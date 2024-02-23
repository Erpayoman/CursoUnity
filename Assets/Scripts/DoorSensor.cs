using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    bool _doorOpening;
    float _doorMoveDistance;

    [SerializeField] float doorSpeed;
    [SerializeField] Transform door;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("Player")) return;
        Debug.Log("From Door: Someone is on the door!");
        _doorOpening = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.tag.Equals("Player")) return;

        _doorOpening = false;
    }
    private void Update()
    {
        if (_doorOpening)
        {
            Vector3 movementDir = new Vector3(0, 0, 1f) * doorSpeed * Time.deltaTime;
            _doorMoveDistance += movementDir.z;
            if (_doorMoveDistance >= door.localScale.z)
            {
                movementDir = Vector3.zero;
                _doorMoveDistance = door.localScale.z;
            }

            door.Translate(movementDir);
            
            
        }
        else
        {
            Vector3 movementDir = new Vector3(0, 0, -1f) * doorSpeed * Time.deltaTime;
            _doorMoveDistance += movementDir.z;

            if (_doorMoveDistance <= 0)
            {
                movementDir = Vector3.zero;
                _doorMoveDistance = 0;
            }

            door.Translate(movementDir);

        }
        
    }
}
