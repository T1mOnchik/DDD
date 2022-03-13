using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaterObjController : MonoBehaviour
{
    public Vector2 destination = new Vector2(0, -1.2f);
    private Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.position = Vector2.SmoothDamp(transform.position, destination, ref velocity, 0.2f, Mathf.Infinity, Time.deltaTime);
    }
}
