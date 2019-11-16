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


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("T");
        }

        if (Input.GetKeyDown(KeyCode.J) && BallinHitBox)
        {
            toBall = Vector3.Normalize(transform.position - Ball.transform.position);

            Ball.GetComponent<Rigidbody>().AddForce(toBall * smallHitForce);
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
