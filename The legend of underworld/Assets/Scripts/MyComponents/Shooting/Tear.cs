using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Tear : UnityPoolObject
{
    public Rigidbody2D rb { get; private set; }
    private float lifetime;
    
    public override void Create()
    {
        base.Create();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetLifetime(float time)
    {
        lifetime = time; 
        StartCoroutine(DestroyTear());
    }

    private IEnumerator DestroyTear()
    {
        yield return new WaitForSeconds(lifetime);
        OnPush();
        Push();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Enemy")
        {
            //collision.GetComponent<Enemy>()
        }
        if (collision.tag == "Tear" || collision.tag == "Player")
        {

        }
        else
        {
            OnPush();
            Push();
        }
    }

}
