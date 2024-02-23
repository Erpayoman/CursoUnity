using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody _rgbd;
    [SerializeField] float bulletSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        _rgbd = GetComponent<Rigidbody>();
        _rgbd.AddForce(this.transform.forward * bulletSpeed );
        Destroy(this.gameObject, 1.5f);
    }

    
}
