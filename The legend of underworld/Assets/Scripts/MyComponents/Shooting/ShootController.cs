using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    private Tear _tearPrefab;
    private Transform _heroPos;
    private float _shootPower;
    private float _shootDelay;
    private IEnumerator _shootCor;
    private bool _canShoot = true;
    private bool _wantShoot = false;
    private Directions _nowDirection;

    public void Init(Transform position, float power = 1, float delay = 1)
    {
        _heroPos = position;
        _tearPrefab = Resources.Load<Tear>("Tear");
        _shootPower = power;
        _shootDelay = delay;
    }
    
    public void StartShooting(Directions dir)
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
            Tear tempTear = UnityPoolManager.Instance.PopOrCreate<Tear>(_tearPrefab, _heroPos.position, Quaternion.identity);
            tempTear.Create();
            switch (_nowDirection)
            {
                case Directions.down:
                    tempTear.rb.AddForce(new Vector2(0, -1) * _shootPower);
                    break;
                case Directions.right:
                    tempTear.rb.AddForce(new Vector2(1, 0) * _shootPower);
                    break;
                case Directions.left:
                    tempTear.rb.AddForce(new Vector2(-1, 0) * _shootPower);
                    break;
                case Directions.up:
                    tempTear.rb.AddForce(new Vector2(0, 1) * _shootPower);
                    break;
            }
            tempTear.SetLifetime(1);
            yield return new WaitForSeconds(_shootDelay);
            _canShoot = true;
        }
    }
}
