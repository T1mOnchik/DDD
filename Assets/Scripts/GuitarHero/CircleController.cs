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
        if(mRigidbody.velocity.y > -4)
        mRigidbody.AddForce(-transform.up * speed *Time.deltaTime);
    }
}