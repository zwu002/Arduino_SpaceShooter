using UnityEngine;
using System.Collections;

public class BGMove : MonoBehaviour {

    public float speed;
    Vector2 offset;

	void Start () {
	
	}
	
	void Update () {
        offset = new Vector2(Time.time * speed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
