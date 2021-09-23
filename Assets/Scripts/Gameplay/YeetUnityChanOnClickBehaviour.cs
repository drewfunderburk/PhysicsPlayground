using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YeetUnityChanOnClickBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 _relativeForce;
    [SerializeField] private LayerMask _layerMask = ~0;

    private void Update()
    {
        // On left mouse down
        if (Input.GetMouseButtonDown(0))
        {
            // Cache camera
            Camera cam = Camera.main;

            // Raycast to mouse pointer
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, _layerMask))
            {
                AIRandomMovementBehaviour moveScript = hit.collider.GetComponent<AIRandomMovementBehaviour>();
                if (moveScript)
                    moveScript.Ragdoll();


                // Get all rigidbodies
                Rigidbody[] rigidbodies = hit.collider.GetComponentsInChildren<Rigidbody>();
                if (rigidbodies.Length > 0)
                {
                    // Yeet them
                    foreach (Rigidbody rigidbody in rigidbodies)
                    {
                        Vector3 right = cam.transform.right * _relativeForce.x;
                        Vector3 up = hit.collider.transform.up * _relativeForce.y;
                        Vector3 forward = cam.transform.forward * _relativeForce.z;
                        Vector3 movement = right + up + forward;

                        rigidbody.AddForce(movement);
                    }
                }

                OnTriggerBehaviour triggerScript = hit.collider.GetComponent<OnTriggerBehaviour>();
                if (triggerScript)
                    triggerScript.OnEnter.Invoke();
            }
        }
    }
}
