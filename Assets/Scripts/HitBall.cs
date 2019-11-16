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
    public float bigHitForce;


    // Start is called before the first frame update
    void Start()
    {
        ballrb = Ball.GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "Ball")
        {
            BallinHitBox = true;
        }
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

        if (Input.GetKeyDown(KeyCode.J) && BallinHitBox)
        {
            toBall = Vector3.Normalize(Ball.transform.position - transform.position);
            Debug.Log("hit soft by " + gameObject.name);
            Ball.GetComponent<Rigidbody>().AddForce(toBall * smallHitForce);
        }
        if (Input.GetKeyDown(KeyCode.K) && BallinHitBox)
        {
            toBall = Vector3.Normalize(Ball.transform.position - transform.position);
            Debug.Log("hit hard by " + gameObject.name);
            Ball.GetComponent<Rigidbody>().AddForce(toBall * bigHitForce);
        }
    }

    private void onTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            BallinHitBox = true;
        }
    }

    private void onTriggerLeave(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            BallinHitBox = false;
        }
    }
}
