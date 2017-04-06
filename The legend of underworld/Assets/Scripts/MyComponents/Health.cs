using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _currHealth;

    public delegate void OnGetDamage();
    public event OnGetDamage onGetDamage;
    public delegate void OnGetHeal();
    public event OnGetHeal onGetHeal;

    public int health
    {
        get
        {
            return _currHealth;
        }
    }

    public int maxHealth
    {
        get
        {
            return _maxHealth;
        }
    }

    public void Init(int maxHealth, int nowHealth=-1)
    {
        _maxHealth = maxHealth;
        if (nowHealth == -1)
        {
            _currHealth = maxHealth;
        }
        else
        {
            _currHealth = nowHealth;
        }
        if (_currHealth <= 0)
            Death();
    }

    public int ChangeValue(int delta)
    {
        _currHealth += delta;
        if (delta < 0)
        {
            if (onGetDamage != null)
                onGetDamage();
        }
        else if(delta > 0)
        {
            if (onGetHeal != null)
                onGetHeal();
        }
        if (_currHealth <= 0)
            Death();
        return _currHealth;
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
