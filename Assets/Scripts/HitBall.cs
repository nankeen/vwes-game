using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBall : MonoBehaviour
{
    public GameObject Ball;
    private Vector3 toBall;
    private Rigidbody ballrb;
    public bool BallinHitBox = false;
    public bool isPlayer1;
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


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            BallinHitBox = false;
        }
    }

    Vector3 GenHitVector()
    {
        Vector3 hitVec = new Vector3();
        hitVec.z = -(transform.position.z + GaussianRandom.generateNormalRandom(0, 1)); //Gaussian noise added to z. Range -23 to +23 depend on pos
        //positive x if player2, neg if player1
        hitVec.x = 20;
        if (isPlayer1) { hitVec.x *= -1; }
        hitVec.y = 20 + GaussianRandom.generateNormalRandom(0, 3); 
        return hitVec; 
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J) && BallinHitBox)
        {
            toBall = (Ball.transform.position - transform.position) / (Ball.transform.position - transform.position).magnitude;
            
            Debug.Log("hit soft by " + gameObject.name);
            //ballrb.AddForce(toBall * smallHitForce);
            ballrb.AddForce(GenHitVector() * smallHitForce);
        }
        if (Input.GetKeyDown(KeyCode.K) && BallinHitBox)
        {
            toBall = toBall = (Ball.transform.position - transform.position) / (Ball.transform.position - transform.position).magnitude;
            Debug.Log("hit hard by " + gameObject.name);
            //ballrb.AddForce(toBall * bigHitForce);
            ballrb.AddForce(GenHitVector() * smallHitForce);
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
