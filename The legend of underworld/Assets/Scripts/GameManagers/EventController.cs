using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class EventController : MonoBehaviour
{
    [SerializeField]
    private Vector2 direction;
    public CollisionColliderUnityEvent onTriggerEnter = new CollisionColliderUnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter.Invoke(collision, direction);
    }
    // проблема в том что при создание префаба теряется ссылка на функцию из другого класса
    // которую мы выставили в инспекторе, тогда получаются для геймдизайнера они бесполезны
}

[System.Serializable]
public class CollisionColliderUnityEvent : UnityEvent<Collider2D, Vector2>
{

}
