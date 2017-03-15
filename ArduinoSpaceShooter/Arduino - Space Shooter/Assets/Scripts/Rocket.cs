using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
    
    public float rocketSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * rocketSpeed, Space.World);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Border")
        {
            gameObject.SetActive(false);
        }

        if (col.gameObject.tag == "Enemy")
        {

        }
    }

}
