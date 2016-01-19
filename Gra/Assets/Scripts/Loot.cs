using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour
{
    public GameObject loot;
	public GameObject buildStone;

    void Start()
    {

    }

	public void generateBones(int bonesValue)
    {
        for (int i = 0; i < bonesValue; i++)
        {
            float x = transform.position.x + Random.Range(0.1f, 1.0f);
            float y = transform.position.y + Random.Range(0.1f, 1.0f);
            Vector2 newPosition = new Vector2(x, y);
            Instantiate(loot, newPosition, transform.rotation);
			x += 0.5f;
			y -= 0.5f;
			Vector2 newPosition2 = new Vector2(x, y);
			Instantiate(buildStone, newPosition2, transform.rotation);
        }
    }
}