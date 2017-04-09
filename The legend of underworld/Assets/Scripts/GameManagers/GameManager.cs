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
    public SceneManager sceneManager { get; private set; }
    private GameObject _cameras;
    

    private void PlayerDeath()
    {
        sceneManager.Load(0);
    }

    public void StartGame(Vector3 roomPos)
    {
        MoveIntoRoom(roomPos);
        player.transform.position = new Vector3(roomPos.x + (Dungeon.RoomSizeX / 2), roomPos.y + (Dungeon.RoomSizeY / 2), 0);
    }

    public void MoveIntoRoom(Vector3 roomPos)
    {
        _cameras.transform.position = new Vector3(roomPos.x + (Dungeon.RoomSizeX / 2), roomPos.y + (Dungeon.RoomSizeY / 2), 0);
    }
    
    // должен ли я делать здоровье буличным чтоб гейм менеджер мог подписатся на события от компонента игрока
    // не уверен что игрок сразу должен быть на сцене а инициироватся сразу
    private void Awake()
    {
        mainUI = GetComponentInChildren<GameUI>();
        dungeonManager = GetComponentInChildren<Dungeon>();
        // не знаю должен ли dungeon до сиз пор наследоваться от синглтона или его стоит добавить 
        // сразу на гейм мэнеджер
        player = GetComponentInChildren<Hero>();
        sceneManager = GetComponentInChildren<SceneManager>();
        _cameras = Camera.main.transform.parent.gameObject;
    }

    private void Start()
    {

        player.onDeath += PlayerDeath;
        //player. _health.onChanged += ChangeHealthUI;
    }

    private void OnDestroy()
    {
        player.onDeath -= PlayerDeath;
        // должен ли я в итоге снимать подписку на событие в момент дестроя,
        // происходит ли дестрой этого объекта при смене сцены
        //_health.onChanged -= ChangeHealthUI;
    }

}
