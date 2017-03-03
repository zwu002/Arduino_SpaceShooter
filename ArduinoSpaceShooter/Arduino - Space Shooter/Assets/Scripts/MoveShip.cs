using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class MoveShip : MonoBehaviour {

    public float speed = 1;
    float amountToMove;

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
	}

    void MoveObject(int direction)
    {
        if (direction == 1)
        {
            transform.Translate(Vector2.up * amountToMove, Space.World);
        }
        else if (direction == 2)
        {
            transform.Translate(Vector2.down * amountToMove, Space.World);
        }
    }
}
