using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    [SerializeField]
    private float _maxSpeed;
    private Rigidbody2D _mainRB;
    private Vector2 _direction;

    public Vector2 direction
    {
        get
        {
            return _direction;
        }
    }

    public float maxSpeed
    {
        get
        {
            return _maxSpeed;
        }
    }

    void Awake()
    {
        _mainRB = GetComponent<Rigidbody2D>();
	}

    public void Init(float spd = 1)
    {
        _maxSpeed = spd;
    }

    void FixedUpdate()
    {
        Move(_direction);
	}

    void Move(Vector2 direction)
    {
        _mainRB.velocity = direction*_maxSpeed;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
}
