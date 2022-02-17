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
        mRigidbody.AddForce(-transform.up * speed *Time.deltaTime);
    }
}
