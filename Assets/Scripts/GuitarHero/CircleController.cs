using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{   
    Rigidbody2D mRigidbody;
    public float speed;

    void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mRigidbody.velocity = new Vector2(0, -speed);
    }
}
