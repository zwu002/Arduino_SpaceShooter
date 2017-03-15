using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class MoveShip : MonoBehaviour {

    public float speed = 1;
    float amountToMove;

    public float minPos;
    public float maxPos;

    Vector2 position;

    public GameObject rocketo;
    public GameObject uiManager;

    SerialPort sp = new SerialPort("COM4", 9600);

    void Start () {
        sp.Open();
        sp.ReadTimeout = 1;
	}
	
	void Update () {
        amountToMove = speed * Time.deltaTime;

        if (sp.IsOpen)
        {
            try
            {
                MoveObject(sp.ReadByte());
                print(sp.ReadByte());
            }
            catch (System.Exception)
            {

            }
        }

        // Shooting
        if (Input.GetButtonDown("Fire1") && (Time.timeScale == 1))
        {
            Fire();
        }

    }

    void Fire()
    {
        position = transform.position;
        Instantiate(rocketo, new Vector2(position.x + 1f, position.y), Quaternion.identity);
    }

    void MoveObject(int direction)
    {
        if (direction == 2)
        {
            transform.Translate(Vector2.up * amountToMove, Space.World);
            position = transform.position;
            position.y = Mathf.Clamp(position.y, minPos, maxPos);
            transform.position = position;
        }
        else if (direction == 1)
        {
            transform.Translate(Vector2.down * amountToMove, Space.World);
            position = transform.position;
            position.y = Mathf.Clamp(position.y, minPos, maxPos);
            transform.position = position;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            uiManager.GetComponent<UIManager>().gameOver = true;
        }
    }

}
