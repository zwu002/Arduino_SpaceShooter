using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour {

    public float invaderSpeed;
    Rigidbody2D rb;

	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        transform.Translate(Vector2.left * invaderSpeed, Space.World);
    }

    void OnCollisionEnter2D (Collision2D col)
    {

    }
}

