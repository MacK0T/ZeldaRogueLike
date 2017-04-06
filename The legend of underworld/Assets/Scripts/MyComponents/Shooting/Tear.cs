using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Tear : UnityPoolObject
{
    public Rigidbody2D rb { get; private set; }
    private float _lifetime;
    private int _damage;
    [SerializeField]
    private string _friendlyTag;
    [SerializeField]
    private string _enemyesTag;

    public override void Create()
    {
        base.Create();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetLifetime(float time)
    {
        _lifetime = time;
        StartCoroutine(DestroyTear());
    }

    public void SetDmg(int dmg)
    {
        _damage = dmg;
    }

    private IEnumerator DestroyTear()
    {
        yield return new WaitForSeconds(_lifetime);
        OnPush();
        Push();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == _enemyesTag)
        {
            try
            {
                collision.GetComponent<Health>().ChangeValue(_damage);
            }
            catch { }
        }
        if (collision.tag == "Tear" || collision.tag == _friendlyTag)
        {

        }
        else
        {
            OnPush();
            Push();
        }
    }

}
