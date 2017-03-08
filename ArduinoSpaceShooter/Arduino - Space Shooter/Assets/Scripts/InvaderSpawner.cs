using UnityEngine;
using System.Collections;

public class InvaderSpawner : MonoBehaviour {

    public GameObject spawnSprite;
    public float minPos = -2.6f;
    public float maxPos = 2.6f;
    public float xPos = 4.5f;
    float timer;

    // Use this for initialization
    void Start()
    {
        timer = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Instantiate(spawnSprite, new Vector2(xPos, Random.Range(minPos, maxPos)), Quaternion.identity);
            timer = Random.Range(0.2f, 2f);
        }
    }
}
