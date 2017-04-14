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
    public Health health { get; private set; }

    private const string _animHeadProperty = "Head";
    private const string _animBodyProperty = "Body";
    private int _animHeadHash;
    private int _animBodyHash;

    public event System.Action onDeath = delegate { };

    private void ChangeHealth(int currHealth, int delta)
    {
        if (currHealth <= 0)
        {
            onDeath();
        }
    }

    private void ShootModificed()
    {

    }

    #region


    private void Awake()
    {
        health = GetComponent<Health>();
        _anim = GetComponent<Animator>();
        _moveCntr = GetComponent<MoveController>();
        _shootCntr = GetComponent<ShootController>();
        _animHeadHash = Animator.StringToHash(_animHeadProperty);
        _animBodyHash = Animator.StringToHash(_animBodyProperty);
    }

    private void Start()
    {
        health.onChanged += ChangeHealth;
    }

    private void OnDestroy()
    {
        health.onChanged -= ChangeHealth;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _anim.SetInteger(_animHeadHash, 1);
            _shootCntr.StartShooting(new Vector2(0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _anim.SetInteger(_animHeadHash, 2);
            _shootCntr.StartShooting(new Vector2(-1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _anim.SetInteger(_animHeadHash, 3);
            _shootCntr.StartShooting(new Vector2(0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _anim.SetInteger(_animHeadHash, 4);
            _shootCntr.StartShooting(new Vector2(1, 0));
        }
        else if (!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) &&
            !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            _anim.SetInteger(_animHeadHash, 0);
            _shootCntr.StopShooting();
        }

        _moveCntr.direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        int bodyDirection = 0;
        if (Input.GetKeyDown(KeyCode.S))
        {
            bodyDirection = 1;
            _anim.SetInteger(_animBodyHash, bodyDirection);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            bodyDirection = 2;
            _anim.SetInteger(_animBodyHash, bodyDirection);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            bodyDirection = 3;
            _anim.SetInteger(_animBodyHash, bodyDirection);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            bodyDirection = 4;
            _anim.SetInteger(_animBodyHash, bodyDirection);
        }
        else if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            bodyDirection = 0;
            _anim.SetInteger(_animBodyHash, bodyDirection);
        }

    }
    #endregion

}