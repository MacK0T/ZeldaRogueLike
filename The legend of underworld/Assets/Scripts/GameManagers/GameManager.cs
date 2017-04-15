using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TestSingleton<GameManager>
{
    // должен ли я просто вешать компонент для инициализции синглтона
    // не должен ли я делать его из кода например при начале новой игры из меню, а потом делать
    //    донт дестрой он лоад, также вроде вместо донт дестрой он лоад надо подписыватся через делегат
    //    но я не знаю как
    public GameUI mainUI { get; private set; }
    public Dungeon dungeonManager { get; private set; }
    public Hero player { get; private set; }
    public SceneController sceneManager { get; private set; }
    private GameObject _cameras;
    

    private void PlayerDeath()
    {
        sceneManager.Load(0);
    }

    public void StartGame(Vector3 roomPos)
    {
        MoveIntoRoom(roomPos);
        player.transform.position = new Vector3(roomPos.x + (Dungeon.RoomSizeX / 2), roomPos.y - (Dungeon.RoomSizeY / 2), 0);
    }

    public void MoveIntoRoom(Vector3 roomPos)
    {
        _cameras.transform.position = new Vector3(roomPos.x + (Dungeon.RoomSizeX / 2), roomPos.y - (Dungeon.RoomSizeY / 2), 0);
    }
    
    public IEnumerator MoveIntoRoomAnim(float slideTime, float directionX, float directionY)
    {
        Vector3 startingPoint = _cameras.transform.position;
        Vector3 finalPoint = new Vector3(
            _cameras.transform.position.x + directionX * Dungeon.RoomSizeX,
            _cameras.transform.position.y + directionY * Dungeon.RoomSizeY,
            _cameras.transform.position.z);
        float speedX = Mathf.Abs(finalPoint.x - startingPoint.x) / slideTime;
        float speedY = Mathf.Abs(finalPoint.y - startingPoint.y) / slideTime;
        bool vertical = false;
        if (startingPoint.y == finalPoint.y)
            vertical = true;
        if(vertical)
        {
            if (startingPoint.x < finalPoint.x)
            {
                // right
                player.transform.position = new Vector3(finalPoint.x - 2.74375f,
                    finalPoint.y, 0);
                while (_cameras.transform.position.x <= finalPoint.x)
                {
                    _cameras.transform.localPosition = new Vector3(
                    _cameras.transform.localPosition.x + speedX * Time.deltaTime,
                    _cameras.transform.localPosition.y,
                    _cameras.transform.localPosition.z);
                    yield return null;
                }
            }
            else
            {
                // left
                player.transform.position = new Vector3(finalPoint.x + 2.74375f,
                    finalPoint.y, 0);
                while (_cameras.transform.position.x >= finalPoint.x)
                {
                    _cameras.transform.localPosition = new Vector3(
                    _cameras.transform.localPosition.x - speedX * Time.deltaTime,
                    _cameras.transform.localPosition.y,
                    _cameras.transform.localPosition.z);
                    yield return null;
                }
            }
        }
        else
        {
            if (startingPoint.y < finalPoint.y)
            {
                // top
                player.transform.position = new Vector3(finalPoint.x,
                    finalPoint.y - 1.26875f, 0);
                while (_cameras.transform.position.y <= finalPoint.y)
                {
                    _cameras.transform.localPosition = new Vector3(
                    _cameras.transform.localPosition.x,
                    _cameras.transform.localPosition.y + speedY * Time.deltaTime,
                    _cameras.transform.localPosition.z);
                    yield return null;
                }
            }
            else
            {
                // bot
                player.transform.position = new Vector3(finalPoint.x,
                    finalPoint.y + 1.26875f, 0);
                while (_cameras.transform.position.y >= finalPoint.y)
                {
                    _cameras.transform.localPosition = new Vector3(
                    _cameras.transform.localPosition.x,
                    _cameras.transform.localPosition.y - speedY * Time.deltaTime,
                    _cameras.transform.localPosition.z);
                    yield return null;
                }
            }
        }
    }
    
    new private void Awake()
    {
        base.Awake();
        mainUI = GetComponentInChildren<GameUI>();
        dungeonManager = GetComponentInChildren<Dungeon>();
        player = GetComponentInChildren<Hero>();
        sceneManager = GetComponentInChildren<SceneController>();
        _cameras = Camera.main.transform.parent.gameObject;
        gameObject.AddComponent<UnityPoolManager>();
    }

    private void Start()
    {
        player.onDeath += PlayerDeath;
    }

    new void OnDestroy()
    {
        player.onDeath -= PlayerDeath;
        base.OnDestroy();
    }

}
