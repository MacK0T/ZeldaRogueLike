using UnityEngine;
using System.Collections;

public enum Directions
{
    down,
    left,
    right,
    up
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(ShootController))]
public class Hero : MonoBehaviour
{
    private Animator _anim;
    private MoveController _moveCntr;
    private ShootController _shootCntr;
    [SerializeField]
    private float _shotSpeed;
    [SerializeField]
    private float _tearDelay;

    private Health _health = null;

    void Awake ()
    {
        _health = GetComponent<Health>();
        _anim = GetComponent<Animator>();
        _moveCntr = GetComponent<MoveController>();
        _shootCntr = GetComponent<ShootController>();
        _shootCntr.Init(transform, _shotSpeed, _tearDelay);
        gameObject.AddComponent<UnityPoolManager>();
	}
	

	private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _anim.SetInteger("Head", 1);
            _shootCntr.StartShooting(Directions.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _anim.SetInteger("Head", 2);
            _shootCntr.StartShooting(Directions.left);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _anim.SetInteger("Head", 3);
            _shootCntr.StartShooting(Directions.up);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _anim.SetInteger("Head", 4);
            _shootCntr.StartShooting(Directions.right);
        }
        else if (!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) &&
            !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            _anim.SetInteger("Head", 0);
            _shootCntr.StopShooting();
        }

        _moveCntr.SetDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        if (Input.GetKeyDown(KeyCode.S))
        {
            _anim.SetInteger("Body", 1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _anim.SetInteger("Body", 2);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            _anim.SetInteger("Body", 3);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _anim.SetInteger("Body", 4);
        }
        else if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            _anim.SetInteger("Body", 0);
        }
    }

}
