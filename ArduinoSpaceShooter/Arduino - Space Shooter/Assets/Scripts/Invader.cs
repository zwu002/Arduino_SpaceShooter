using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {

    public float invaderSpeed;

	void Start () {
	}
	
	void Update () {
        transform.Translate(Vector2.left * invaderSpeed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "Border")
        {
            gameObject.SetActive(false);
        }

        if (col.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}

