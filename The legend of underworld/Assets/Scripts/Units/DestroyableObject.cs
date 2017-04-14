using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct SpriteHealth
{
    public Sprite stateSprite;
    public int stateHealth;
}

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpriteRenderer))]
public class DestroyableObject : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Health _health;
    [SerializeField]
    private SpriteHealth[] _states;


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
            if (currentHealth >= _states[i].stateHealth)
            {
                _renderer.sprite = _states[i].stateSprite;
                break;
            }
        }
    }

    private void OnDestroy()
    {
        _health.onChanged -= ChangeSprite;
    }
}
