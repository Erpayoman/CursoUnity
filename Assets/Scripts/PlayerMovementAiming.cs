using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAiming : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] LayerMask _aimLayerMask;
    [SerializeField] GameObject shootPoint;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] bool _movingRigidbody;


    float _horizontal;
    float _vertical;


    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");        

        Moving();
        AimTowardMouse();
        Fire();

    }

    private void Moving()
    {
        Vector3 movement = new Vector3(_horizontal, 0f, _vertical);

        if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= _speed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }
    }

    private void AimTowardMouse()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _aimLayerMask))
        {
            var _direction = hitInfo.point - transform.position;
            _direction.y = 0f;
            _direction.Normalize();
            transform.forward = _direction;
        }
    }
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bulletPrefab,shootPoint.transform.position,shootPoint.transform.rotation);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("From Player: knock,knock");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("From Player: "+collision.gameObject.name+" is colliding with player!");
    }
}
