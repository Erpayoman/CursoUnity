using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMovementAiming : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] LayerMask _aimLayerMask;
    [SerializeField] GameObject shootPoint;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] bool _movingWithRigidbody;


    float _horizontal;
    float _vertical;
    Rigidbody _rigidbody;
    Vector3 _movement;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _movement = new Vector3(_horizontal, 0f, _vertical);

        if (!_movingWithRigidbody)
        {
            MovingTransform();
        }
       
       
        AimTowardMouse();
        Fire();

    }
    private void FixedUpdate()
    {
        if (_movingWithRigidbody)
        {
            MovingWithRigidbody();
        }
    }

    private void MovingWithRigidbody()
    {
        if (_movement.magnitude > 0)
        {
            _movement.Normalize();
            _movement *= _speed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + _movement);

        }
        
    }

    private void MovingTransform()
    {
        if (_movement.magnitude > 0)
        {
            _movement.Normalize();
            _movement *= _speed * Time.deltaTime;
            transform.Translate(_movement, Space.World);
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
