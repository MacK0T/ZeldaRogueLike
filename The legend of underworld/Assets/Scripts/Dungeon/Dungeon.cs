using UnityEngine;
using System.Collections.Generic;

public enum RoomTypes: byte
{
    normal,
    treasure,
    boss
}


public class Dungeon : SceneSingleton<Dungeon> 
{
    // размер мира в котором могут быть комнаты в количествах комнат
    [SerializeField]
	private int worldSizeX = 20;
    [SerializeField]
    private int worldSizeY = 20;

    public int worldX
    {
        get
        {
            return worldSizeX;
        }
    }

    public int worldY
    {
        get
        {
            return worldSizeX;
        }
    }

    // Size of 2D Model Prefab in World Space
    public const float RoomSizeX = 7.4375f;
    public const float RoomSizeY = 4.9375f;
	
	// Demo Room Prefab
	public GameObject RoomBasicPrefab; 
	
	// Room structure
	public Room[,] rooms;
    private Dictionary<RoomTypes, int> specialRooms;

	
	// Pointer to Boss Room "Demo" GameObject
	private GameObject startRoom;
	
	public override void Init () 
	{
		GenerateDungeon();
        GenerateGameRooms();

        // Camera looking at Boss Room for the demo
        GetComponentInParent<GameManager>().StartGame(startRoom.transform.position);
	}
	
	public void GenerateDungeon()
	{
		// Create room structure
		rooms = new Room[worldSizeX, worldSizeY];
		
		// Create our first room at a random position
		int roomX = Random.Range (0, worldSizeX);
		int roomY = Random.Range (0, worldSizeY);
		
		Room firstRoom = AddRoom(null, roomX,roomY); // null parent because it's the first node
		
		// Generate childrens
		firstRoom.GenerateChildren();
	}
	
	private void GenerateGameRooms()
	{
		// For each room in our matrix generate a 3D Model from Prefab
		foreach (Room room in rooms)
		{
			if (room == null) continue;
			
			// Real world position
			float worldX = room.x * RoomSizeX;
			float worldZ = room.y * RoomSizeY;
            GameObject g = GameObject.Instantiate(RoomBasicPrefab,new Vector3(worldX, worldZ, 0),RoomBasicPrefab.transform.rotation) as GameObject;
			
			// Add the room info to the GameObject main script (Demo)
			GameRoom gameRoom = g.GetComponent<GameRoom>();
			gameRoom.room = room;
			
			if (room.IsFirstNode()) 
			{
				startRoom = g;
				g.name = "Start Room";
			}
			else g.name = "Room " + room.x + " " + room.y;
		}
	}
	
	// Helper Methods
	
	
	public Room AddRoom(Room parent, int x, int y)
	{
        //Debug.Log(x + " " + y);
		Room room = new Room(parent, x, y);
		rooms[x,y] = room;
		return room;
	}
}
