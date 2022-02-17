using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerController : MonoBehaviour
{
    [SerializeField]private Vector3 acceleration;
    public float speed;
    private float random;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DelayRandom");
    }

    // Update is called once per frame
    void Update()
    {   if(transform.localPosition.x < 3.5f && transform.localPosition.x > -3.5f)
        {
            acceleration = Input.acceleration;
            GetComponent<Rigidbody>().velocity = new Vector3(acceleration.x * speed + random, 0f, 0f);
        }
        else if(transform.localPosition.x > 3.5f)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-1f, 0f, 0f);
        }
        else if(transform.localPosition.x < -3.5f)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(1f, 0f, 0f);
        }
    }

    private IEnumerator DelayRandom()
    {
        while(true)
        {
            random = Random.Range(-2f, 2f);
            yield return new WaitForSeconds(1f);
            yield return null;
        }
    }

}
