using UnityEngine;
using System.Collections;

public class GameRoom : MonoBehaviour
{
	[SerializeField]
	private GameObject doorLeft, doorRight, doorTop, doorBottom;

	public Room room;

    public void PlayerChangeRoom(Collider2D enter, Vector2 direction)
    {
        if (enter.tag == "Player")
        {
            StartCoroutine(GameManager.Instance.MoveIntoRoomAnim(2, direction.x, direction.y));
        }
    }

    private void Start() 
	{
        // Remove walls if connected
        if (room.IsConnectedTo(room.GetLeft()))
        {
            doorLeft.transform.GetChild(0).gameObject.SetActive(true);
            BoxCollider2D[] walls = doorLeft.GetComponentsInChildren<BoxCollider2D>();
            walls[0].enabled = true;
            for (int i = 1; i < walls.Length; i++)
            {
                walls[i].enabled = false;
            }
        }
        else
        {
            doorLeft.transform.GetChild(0).gameObject.SetActive(false);
            BoxCollider2D[] walls = doorLeft.GetComponentsInChildren<BoxCollider2D>();
            walls[0].enabled = false;
            for (int i = 1; i < walls.Length; i++)
            {
                walls[i].enabled = true;
            }
        }

        if (room.IsConnectedTo(room.GetRight()))
        {
            doorRight.transform.GetChild(0).gameObject.SetActive(true);
            BoxCollider2D[] walls = doorRight.GetComponentsInChildren<BoxCollider2D>();
            walls[0].enabled = true;
            for (int i = 1; i < walls.Length; i++)
            {
                walls[i].enabled = false;
            }
        }
        else
        {
            doorRight.transform.GetChild(0).gameObject.SetActive(false);
            BoxCollider2D[] walls = doorRight.GetComponentsInChildren<BoxCollider2D>();
            walls[0].enabled = false;
            for (int i = 1; i < walls.Length; i++)
            {
                walls[i].enabled = true;
            }
        }

        if (room.IsConnectedTo(room.GetTop()))
        {
            doorTop.transform.GetChild(0).gameObject.SetActive(true);
            BoxCollider2D[] walls = doorTop.GetComponentsInChildren<BoxCollider2D>();
            walls[0].enabled = true;
            for (int i = 1; i < walls.Length; i++)
            {
                walls[i].enabled = false;
            }
        }
        else
        {
            doorTop.transform.GetChild(0).gameObject.SetActive(false);
            BoxCollider2D[] walls = doorTop.GetComponentsInChildren<BoxCollider2D>();
            walls[0].enabled = false;
            for (int i = 1; i < walls.Length; i++)
            {
                walls[i].enabled = true;
            }
        }

        if (room.IsConnectedTo(room.GetBottom()))
        {
            doorBottom.transform.GetChild(0).gameObject.SetActive(true);
            BoxCollider2D[] walls = doorBottom.GetComponentsInChildren<BoxCollider2D>();
            walls[0].enabled = true;
            for (int i = 1; i < walls.Length; i++)
            {
                walls[i].enabled = false;
            }
        }
        else
        {
            doorBottom.transform.GetChild(0).gameObject.SetActive(false);
            BoxCollider2D[] walls = doorBottom.GetComponentsInChildren<BoxCollider2D>();
            walls[0].enabled = false;
            for (int i = 1; i < walls.Length; i++)
            {
                walls[i].enabled = true;
            }
        }
	}
}
