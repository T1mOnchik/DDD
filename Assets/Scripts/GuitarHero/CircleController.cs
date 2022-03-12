using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{   
    [SerializeField]private float speed = 50f;

    Rigidbody2D mRigidbody;

    void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(mRigidbody.velocity.y > -4)
        mRigidbody.AddForce(-transform.up * speed *Time.deltaTime);
    }
}
