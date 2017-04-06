using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShootController))]
public class TearTurret : MonoBehaviour
{
    private ShootController _shootCntr;
    //private SpriteRenderer _renderer;
    
    private float x, y;

    private void Awake()
    {
        _shootCntr = GetComponent<ShootController>();
        //_renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(StartCircleShoot());
    }

    private IEnumerator StartCircleShoot()
    {
        x = -1; y = 0;
        bool xplus=true, yplus=false;
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (xplus)
            {
                x += 0.05f;
                if (x >= 1)
                    xplus = false;
            }
            else
            {
                x -= 0.05f;
                if (x <= -1)
                    xplus = true;
            }
            if (yplus)
            {
                y += 0.05f;
                if (y >= 1)
                    yplus = false;
            }
            else
            {
                y -= 0.05f;
                if (y <= -1)
                    yplus = true;
            }
            _shootCntr.StopShooting();
            _shootCntr.StartShooting(new Vector2(x, y));
        }
    }

}
