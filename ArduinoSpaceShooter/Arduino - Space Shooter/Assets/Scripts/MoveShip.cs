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
    public GameObject ship;

    float previousTime;
    public float shootingTimeGap;

    SerialPort sp = new SerialPort("COM4", 9600);

    Rigidbody2D rb;

    void Start () {
        sp.Open();
        sp.ReadTimeout = 1;
        previousTime = Time.time + 1;

        rb = ship.gameObject.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        amountToMove = speed * Time.deltaTime;

        if (sp.IsOpen)
        {
            try
            {
                NextMove(sp.ReadByte());
            }
            catch (System.Exception)
            {

            }
        }
        position = transform.position;
        position.y = Mathf.Clamp(position.y, minPos, maxPos);
        transform.position = position;
    }

    void Fire()
    {
        position = transform.position;
        Instantiate(rocketo, new Vector2(position.x + 1f, position.y), Quaternion.identity);
    }

    void NextMove(int direction)
    {
        if (direction == 2)
        {
            rb.velocity = new Vector2(0, speed * Time.deltaTime);
            position = transform.position;
            position.y = Mathf.Clamp(position.y, minPos, maxPos);
            transform.position = position;
        }

        if (direction == 3 && (Time.time - previousTime) > shootingTimeGap)
        {
            Fire();
            previousTime = Time.time;
        }
        
        if (direction == 3 && Time.timeScale == 0 && uiManager.GetComponent<UIManager>().gameOver == true)
        {
            uiManager.GetComponent<UIManager>().Replay();
        }
        else if (direction == 3 && Time.timeScale == 0 && uiManager.GetComponent<UIManager>().gameStart == true)
        {
            uiManager.GetComponent<UIManager>().startText.gameObject.SetActive(false);
            uiManager.GetComponent<UIManager>().startTextII.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            uiManager.GetComponent<UIManager>().gameOver = true;
            uiManager.GetComponent<UIManager>().bang.Play();
        }
    }

}
