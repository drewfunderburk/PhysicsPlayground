using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnYPositionBehaviour : MonoBehaviour
{
    [SerializeField] private float _yPosition = -10f;

    public UnityEvent OnYPosition;

    private bool _eventInvoked = false;

    private void Update()
    {
        if (!_eventInvoked)
            if (transform.position.y < _yPosition)
                OnYPosition.Invoke();
    }
}
