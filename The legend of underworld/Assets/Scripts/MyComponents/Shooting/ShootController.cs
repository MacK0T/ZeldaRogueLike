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
            CreateBullet(_nowDirection);
            yield return new WaitForSeconds(_shootDelay);
        }
    }
    // изза оптимизация убралась проверка на то чтобы делей сохранялся еслимы отпускаем кнопку и нажимаем снова
	// а коротюна не завершалась резко чтобымы могли знать прошло ли нужное нам время выстрела или нет

    private void CreateBullet(Vector2 direction)
    {
        Tear tempTear = UnityPoolManager.Instance.PopOrCreate<Tear>(_tearPrefab, _heroPos.position, Quaternion.identity);
        tempTear.Create();
        tempTear.SetDmg(_shootDamage);
        tempTear.rb.AddForce(direction * _shootPower);
        tempTear.SetLifetime(_shootRange);
    }
}
