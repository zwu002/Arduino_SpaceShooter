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
            uiManager.GetComponent<UIManager>().scoreUpdate();
            gameObject.SetActive(false);
            col.gameObject.SetActive(false);
        }
    }

}
