using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    // должен ли я просто вешать компонент для инициализции синглтона
    // не должен ли я делать его из кода например при начале новой игры из меню, а потом делать
    //    донт дестрой он лоад, также вроде вместо донт дестрой он лоад надо подписыватся через делегат
    //    но я не знаю как
    public GameUI mainUI { get; private set; }
    public Dungeon dungeonManager { get; private set; }
    public Hero player { get; private set; }

    private void PlayerDeath(int currHealth, int delta)
    {

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
    }

    private void Start()
    {
        //player. _health.onChanged += ChangeHealthUI;
    }

    private void OnDestroy()
    {
        // должен ли я в итоге снимать подписку на событие в момент дестроя,
        // происходит ли дестрой этого объекта при смене сцены
        //_health.onChanged -= ChangeHealthUI;
    }

}
