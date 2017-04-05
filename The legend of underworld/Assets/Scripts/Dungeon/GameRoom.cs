using UnityEngine;
using System.Collections;

public class GameRoom : MonoBehaviour
{
	[SerializeField]
	private GameObject doorLeft, doorRight, doorTop, doorBottom;
	public Room room;
	
	void Start () 
	{
        // Remove walls if connected
        if (room.IsConnectedTo(room.GetLeft()))
        {
            doorLeft.transform.GetChild(0).gameObject.SetActive(true);
            BoxCollider2D[] walls = doorLeft.GetComponentsInChildren<BoxCollider2D>();
            for(int i = 0; i < walls.Length; i++)
            {
                walls[i].enabled = false;
            }
        }
        else
        {
            doorLeft.transform.GetChild(0).gameObject.SetActive(false);
            BoxCollider2D[] walls = doorLeft.GetComponentsInChildren<BoxCollider2D>();
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].enabled = true;
            }
        }

        if (room.IsConnectedTo(room.GetRight()))
        {
            doorRight.transform.GetChild(0).gameObject.SetActive(true);
            BoxCollider2D[] walls = doorRight.GetComponentsInChildren<BoxCollider2D>();
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].enabled = false;
            }
        }
        else
        {
            doorRight.transform.GetChild(0).gameObject.SetActive(false);
            BoxCollider2D[] walls = doorRight.GetComponentsInChildren<BoxCollider2D>();
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].enabled = true;
            }
        }

        if (room.IsConnectedTo(room.GetTop()))
        {
            doorTop.transform.GetChild(0).gameObject.SetActive(true);
            BoxCollider2D[] walls = doorTop.GetComponentsInChildren<BoxCollider2D>();
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].enabled = false;
            }
        }
        else
        {
            doorTop.transform.GetChild(0).gameObject.SetActive(false);
            BoxCollider2D[] walls = doorTop.GetComponentsInChildren<BoxCollider2D>();
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].enabled = true;
            }
        }

        if (room.IsConnectedTo(room.GetBottom()))
        {
            doorBottom.transform.GetChild(0).gameObject.SetActive(true);
            BoxCollider2D[] walls = doorBottom.GetComponentsInChildren<BoxCollider2D>();
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].enabled = false;
            }
        }
        else
        {
            doorBottom.transform.GetChild(0).gameObject.SetActive(false);
            BoxCollider2D[] walls = doorBottom.GetComponentsInChildren<BoxCollider2D>();
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].enabled = true;
            }
        }
	}
}
