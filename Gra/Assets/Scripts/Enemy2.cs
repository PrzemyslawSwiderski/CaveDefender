using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour
{
    [HideInInspector]
    public float moveSpeed, moveSpeedY;		// The speed the enemy moves at.
    [HideInInspector]
    public int HP = 1;                  // How many times the enemy can be hit before it dies.
    int bonesValue = 5;
    public bool dead = false;           // Whether or not the enemy is dead.
    public Sprite deadEnemy;            // A sprite of the enemy when it's dead.
    public Sprite damagedEnemy;         // An optional sprite of the enemy when it's damaged.
    [HideInInspector]
    public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
    [HideInInspector]
    public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
    public float xMin, xMax, yMin, yMax, movey, howManyY, howTimes;

    private SpriteRenderer ren;         // Reference to the sprite renderer.
    private Transform frontCheck;       // Reference to the position of the gameobject used for checking if something is in front.
    private Transform Cave;
    void Awake()
    {
        // Setting up the references.
        ren = transform.Find("body").GetComponent<SpriteRenderer>();
        frontCheck = transform.Find("frontCheck").transform;
        moveSpeed = (Random.Range(1f, 3f) * 2);
        moveSpeedY = (Random.Range(-1f, 1f) * 2);
        xMin = -30f;
        xMax = 30f;
        yMin = -9.7f;
        yMax = 9.5f;
        movey = 1.5f;
        howManyY = (Random.Range(0f, 1f) * 100);
        howTimes = 0f;
        GameObject helpVariable = GameObject.FindGameObjectWithTag("Cave");
        Cave = helpVariable.transform;
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1, new Vector2(1, 0), 0, 1 << 15);
        if (((GetComponent<Rigidbody2D>().position.y + movey) <= yMax) & ((GetComponent<Rigidbody2D>().position.y + movey) >= yMin) & (howTimes <= howManyY))
        {
            howTimes = howTimes + 1;
        }
        else
        {
            howTimes = 0;
            moveSpeedY = -moveSpeedY;
        }
        if (GetComponent<Rigidbody2D>().position.x < 15.0f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, transform.localScale.y * moveSpeedY);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = (new Vector2(((Cave.position.x - transform.position.x) * moveSpeed) / 15, (((Cave.position.y - transform.position.y) * moveSpeedY) / 15)+0.3f));

        }

        // If the enemy has one hit point left and has a damagedEnemy sprite...
        if (HP == 1 && damagedEnemy != null)
            // ... set the sprite renderer's sprite to be the damagedEnemy sprite.
            ren.sprite = damagedEnemy;

        // If the enemy has zero or fewer hit points and isn't dead yet...
        if (HP <= 0 && !dead)
        {
            // ... call the death function.
            moveSpeed = 0;
            moveSpeedY = 0;
            Death();
        }

        GetComponent<Rigidbody2D>().position = new Vector2(Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, xMin, xMax), Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, yMin, yMax));
    }

    public void Hurt(int attackPoints)
    {
        // Reduce the number of hit points by one.
        HP -= attackPoints;
        movey *= Random.Range(0, 1) * 2 - 1;
        if (((GetComponent<Rigidbody2D>().position.y + movey) <= yMax) & ((GetComponent<Rigidbody2D>().position.y + movey) >= yMin))
            transform.Translate(0, movey, 0);
        else
            transform.Translate(0, -movey, 0);
    }

    void Death()
    {
        // Find all of the sprite renderers on this object and it's children.
        SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Disable all of them sprite renderers.
        foreach (SpriteRenderer s in otherRenderers)
        {
            s.enabled = false;
        }

        // Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
        ren.enabled = true;
        ren.sprite = deadEnemy;

        // Set dead to true.
        dead = true;

        GameManager.instance.deadEnemies++;
        GameManager.instance.points = GameManager.instance.points + GameManager.instance.multiplier;

        gameObject.GetComponent<Loot>().generateBones(bonesValue);

        Destroy();

        // Allow the enemy to rotate and spin it by adding a torque.
        //GetComponent<Rigidbody2D>().fixedAngle = false;
        //GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin,deathSpinMax));

        // Play a random audioclip from the deathClips array.
        //int i = Random.Range(0, deathClips.Length);
        //AudioSource.PlayClipAtPoint(deathClips[i], transform.position);
        //Invoke ("Destroy",deathClips[i].length+1.0f);

    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    public void Flip()
    {
        // Multiply the x component of localScale by -1.
        Vector3 enemyScale = transform.localScale;
        enemyScale.y *= -1;
        transform.localScale = enemyScale;
    }
}
