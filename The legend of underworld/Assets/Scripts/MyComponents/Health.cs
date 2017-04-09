using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _currentValue;

    //public delegate void OnGetDamage();
    //public event OnGetDamage onGetDamage;
    //public delegate void OnGetHeal();
    //public event OnGetHeal onGetHeal;

    //public System.Action onGetDamage = delegate() { };
    //public System.Action onGetHeal = delegate () { };
    public event System.Action<int, int> onChanged = delegate { };

    public int value
    {
        get
        {
            return _currentValue;
        }
        private set
        {
            // вынести death
            // как мне это сделать ведь меняем мы значение 
            // _currentValue а если мы на него повесим сет то он перестанет быть видимым в инспекторе
        }
    }
    
    public int maxValue
    {
        get
        {
            return _maxHealth;
        }
    }

    public void Init(int maxHealth)
    {
        Init(maxHealth, maxHealth);
    }

    public void Init(int maxHealth, int nowHealth)
    {
        _maxHealth = maxHealth;
        _currentValue = nowHealth;
    }

    public int ChangeValue(int delta)
    {
        _currentValue += delta;
        onChanged(_currentValue, delta);
        /*
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
        */
        return _currentValue;
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
