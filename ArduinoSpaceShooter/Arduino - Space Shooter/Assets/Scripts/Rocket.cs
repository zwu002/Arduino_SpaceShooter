using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
    
    public float rocketSpeed;

    public GameObject uiManager;

	// Use this for initialization
	void Start () {
        uiManager = GameObject.Find("UIManager");
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * rocketSpeed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Border")
        {
            gameObject.SetActive(false); // Clean rockets outside the screen
        }

        if (col.gameObject.tag == "Enemy") // Hit event
        {
            uiManager.GetComponent<UIManager>().scoreUpdate();
            uiManager.GetComponent<UIManager>().bang.Play();
            gameObject.SetActive(false);
            col.gameObject.SetActive(false);
        }
    }

}
