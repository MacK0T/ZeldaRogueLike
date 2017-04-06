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
    private IEnumerator _shootCor;
    private bool _canShoot = true;
    private bool _wantShoot = false;
    private Vector2 _nowDirection;

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

    public void StartShooting(Directions dir)
    {
        switch (dir)
        {
            case Directions.down:
                _nowDirection = new Vector2(0, -1);
                break;
            case Directions.right:
                _nowDirection = new Vector2(1, 0);
                break;
            case Directions.left:
                _nowDirection = new Vector2(-1, 0);
                break;
            case Directions.up:
                _nowDirection = new Vector2(0, 1);
                break;
        }
        _shootCor = ShootDelay();
        _wantShoot = true;
        StartCoroutine(_shootCor);
    }

    public void StartShooting(Vector2 dir)
    {
        _nowDirection = dir;
        _shootCor = ShootDelay();
        _wantShoot = true;
        StartCoroutine(_shootCor);
    }

    public void StopShooting()
    {
        _wantShoot = false;
    }

    private IEnumerator ShootDelay()
    {
        while (_canShoot && _wantShoot)
        {
            _canShoot = false;
            CreateBullet(_nowDirection);
            yield return new WaitForSeconds(_shootDelay);
            _canShoot = true;
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
