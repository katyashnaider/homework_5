using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmControl : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    public event UnityAction Entered;
    public event UnityAction Leaving;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Player>(out Player player))
        {
            Entered?.Invoke();
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Player>(out Player player))
        {
            Leaving?.Invoke();
        }
    }
}
