using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class EventController : MonoBehaviour
{
    private Collider2D mainCollider = null;
    [SerializeField]
    private Vector2 direction;
    public CollisionColliderUnityEvent onTriggerEnter = new CollisionColliderUnityEvent();

    private void Awake()
    {
        mainCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter.Invoke(collision, mainCollider, direction);
    }

}

[System.Serializable]
public class CollisionColliderUnityEvent : UnityEvent<Collider2D, Collider2D, Vector2>
{

}
