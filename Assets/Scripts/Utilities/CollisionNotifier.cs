using System;
using UnityEngine;

public class CollisionNotifier : MonoBehaviour
{
    public void OnCollisionEnter(Collision other)
    {
        EventManager.Instance.Invoke("OnCollisionEnter", gameObject, other);
    }

    public void OnTriggerEnter(Collider other)
    {
        EventManager.Instance.Invoke("OnTriggerEnter", gameObject, other);
    }

    public void OnTriggerExit(Collider other)
    {
        EventManager.Instance.Invoke("OnTriggerExit", gameObject, other);
    }
}