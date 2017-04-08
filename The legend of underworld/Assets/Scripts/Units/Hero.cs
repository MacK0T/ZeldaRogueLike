using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(ShootController))]
public class Hero : MonoBehaviour
{
    private Animator _anim;
    private MoveController _moveCntr;
    private ShootController _shootCntr;
    private Health _health;
    private HealthBar _healthUI;



    private void ChangeHealthUI(int currHP, int delta)
    {
        _healthUI.UpdateHealth(currHP);
    }

    private void ShootModificed()
    {

    }

    #region


    void Awake()
    {
        _health = GetComponent<Health>();
        _anim = GetComponent<Animator>();
        _moveCntr = GetComponent<MoveController>();
        _shootCntr = GetComponent<ShootController>();
        gameObject.AddComponent<UnityPoolManager>();
    }

    private void Start()
    {
        _healthUI = FindObjectOfType<Canvas>().GetComponent<GameUI>().healthBar;
        _healthUI.SpawnHearts(_health.maxValue, _health.value);
        _health.onChanged += ChangeHealthUI;
    }

    private void OnDestroy()
    {
        _health.onChanged -= ChangeHealthUI;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _anim.SetInteger("Head", 1);
            _shootCntr.StartShooting(new Vector2(0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _anim.SetInteger("Head", 2);
            _shootCntr.StartShooting(new Vector2(-1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _anim.SetInteger("Head", 3);
            _shootCntr.StartShooting(new Vector2(0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _anim.SetInteger("Head", 4);
            _shootCntr.StartShooting(new Vector2(1, 0));
        }
        else if (!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) &&
            !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            _anim.SetInteger("Head", 0);
            _shootCntr.StopShooting();
        }
        // переделать енумы на получение осей и функцию оюработчик
        _moveCntr.SetDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        int bodyDirection = 0;
        if (Input.GetKeyDown(KeyCode.S))
        {
            bodyDirection = 1;
            _anim.SetInteger("Body", bodyDirection);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            bodyDirection = 2;
            _anim.SetInteger("Body", bodyDirection);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            bodyDirection = 3;
            _anim.SetInteger("Body", bodyDirection);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            bodyDirection = 4;
            _anim.SetInteger("Body", bodyDirection);
        }
        else if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            bodyDirection = 0;
            _anim.SetInteger("Body", bodyDirection);
        }
        // прошлый код ломал анимацию движения
    }
    #endregion

}