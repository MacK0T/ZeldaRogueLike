using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class DestroyableObject : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Health _health;
    [SerializeField]
    private Sprite[] _states;
    [SerializeField]
    private int[] _statesHealth;


    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _health = GetComponent<Health>();
        _health.onChanged += ChangeSprite;
    }

    private void ChangeSprite(int currentHealth, int delta)
    {
        for (int i = 0; _states.Length > i; i++)
        {
            if (currentHealth >= _statesHealth[i])
            {
                _renderer.sprite = _states[i];
                break;
            }
        }
    }

    private void OnDestroy()
    {
        _health.onChanged -= ChangeSprite;
    }
}
