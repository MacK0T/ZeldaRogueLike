using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(ShootController))]
public class ShootEnemy : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenChangeDirection;
    [SerializeField]
    private Vector2[] directions;

    private Animator _anim;
    private MoveController _moveCntr;
    private ShootController _shootCntr;
    private Health _health;
    private Coroutine _behaviorCor;

    private const string _tagPlayerProperty = "Player";
    private const string _tagTearProperty = "Tear";

    private IEnumerator RandomMove()
    {
        while (true)
        {
            Vector2 newDirertion;
            do
            {
                newDirertion = directions[Random.Range(0, directions.Length)];
            } while (newDirertion == _moveCntr.direction);
            _moveCntr.direction = newDirertion;
            _shootCntr.StartShooting(newDirertion);
            yield return new WaitForSeconds(timeBetweenChangeDirection);
        }
    }

    


    void Awake()
    {
        _health = GetComponent<Health>();
        _anim = GetComponent<Animator>();
        _moveCntr = GetComponent<MoveController>();
        _shootCntr = GetComponent<ShootController>();
    }

    void Start()
    {
        _behaviorCor = StartCoroutine(RandomMove());
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Tear"))
        {

        }
        else
        {
            StopCoroutine(_behaviorCor);
            _behaviorCor = StartCoroutine(RandomMove());
        }
    }

}
