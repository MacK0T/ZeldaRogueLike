using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShootController))]
public class TearTurret : MonoBehaviour
{
    private ShootController _shootCntr;
    //private SpriteRenderer _renderer;
    
    private void Awake()
    {
        _shootCntr = GetComponent<ShootController>();
        //_renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(StartCircleShoot(0.05f, 0.05f));
    }

    private IEnumerator StartCircleShoot(float rotate, float time)
    {
        float x, y;
        float allRotate = 0;
        while (true)
        {
            allRotate += rotate;
            yield return new WaitForSeconds(time);
            x = Mathf.PingPong(allRotate - 1, 2) - 1;
            y = Mathf.PingPong(allRotate, 2) - 1;
            _shootCntr.StartShooting(new Vector2(x, y));
        }
    }

}
