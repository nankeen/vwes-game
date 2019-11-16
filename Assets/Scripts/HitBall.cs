using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBall : MonoBehaviour
{
    public GameObject Ball;
    private Vector3 toBall;
    private Rigidbody ballrb;
    public bool BallinHitBox = false;
    public float smallHitForce;


    // Start is called before the first frame update
    void Start()
    {
        ballrb = Ball.GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("test");
        if (other.gameObject.name == "Ball") { }
        BallinHitBox = true;
  
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("test");
        BallinHitBox = true;

    }

    void OnTriggerLeave(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            BallinHitBox = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("T");
        }
        if (BallinHitBox)
        {
            Debug.Log("yey");
        }

        if (Input.GetKeyDown(KeyCode.J) && BallinHitBox)
        {
            toBall = Vector3.Normalize(transform.position - Ball.transform.position);

            Ball.GetComponent<Rigidbody>().AddForce(toBall * smallHitForce);
        }
    }
}
