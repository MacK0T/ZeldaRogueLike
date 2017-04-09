using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField]
    private Tear _tearPrefab;
    private Transform _heroPos;
    [SerializeField]
    private float _shootPower;
    [SerializeField]
    private float _shootDelay;
    [SerializeField]
    private float _shootRange;
    [SerializeField]
    private int _shootDamage;
    private Coroutine _shootCor;
    private Vector2 _nowDirection;
    private float _nextShootTime;
    
    private void Awake()
    {
        _heroPos = transform;
    }

    public void Init(Transform position, float power = 1, float delay = 1, float range = 1, int damage = 10)
    {
        _heroPos = position;
        //_tearPrefab = Resources.Load<Tear>("Tear");
        _shootPower = power;
        _shootDelay = delay;
        _shootRange = range;
        _shootDamage = damage;
    }

    public void StartShooting(Vector2 dir)
    {
        _nowDirection = dir;
        StopShooting();
        _shootCor = StartCoroutine(ShootDelay());
    }

    public void StopShooting()
    {
        if(_shootCor!=null)
            StopCoroutine(_shootCor);
    }

    private IEnumerator ShootDelay()
    {
        while (true)
        {
            yield return new WaitWhile(() => _nextShootTime > Time.time);
            CreateBullet(_nowDirection);
            _nextShootTime = Time.time + _shootDelay;
        }
    }

    private void CreateBullet(Vector2 direction)
    {
        Tear tempTear = UnityPoolManager.Instance.PopOrCreate<Tear>(_tearPrefab, _heroPos.position, Quaternion.identity);
        tempTear.Create();
        tempTear.SetDmg(_shootDamage);
        tempTear.rb.AddForce(direction * _shootPower);
        tempTear.SetLifetime(_shootRange);
    }
}
