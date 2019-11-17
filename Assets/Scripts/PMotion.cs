using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMotion : MonoBehaviour
{
    public GameObject Ball;
    public GameObject HitLogic;
    public bool isPlayer1 = false;
    public float movementSpeed;
    public BoxCollider net;
    public Vector3 toBall;
    public Camera cam;
    public float stoppingDistance = 4;
    private Vector3 courtPlaneNormal = Vector3.up;
    private Vector3 nextPositionDiff;
    private Vector3 queryPos;

    private bool inBoundary = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            if (isPlayer1)
            {
                HitLogic.GetComponent<web>().inP1HitBox = true;
            }
            else
            {
                HitLogic.GetComponent<web>().inP2HitBox = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            if (isPlayer1)
            {
                HitLogic.GetComponent<web>().inP1HitBox = false;
            }
            else
            {
                HitLogic.GetComponent<web>().inP2HitBox = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //always face camera
        transform.LookAt(cam.transform);


        //move towards ball
        toBall = Ball.transform.position - transform.position;
        toBall = toBall - Vector3.Normalize(toBall) * stoppingDistance;
        nextPositionDiff = Vector3.Normalize(Vector3.ProjectOnPlane(toBall, courtPlaneNormal)) * movementSpeed * Time.deltaTime;

        //if not about to cross over court
        queryPos = transform.position + nextPositionDiff;
        Vector3 closp = net.ClosestPointOnBounds(queryPos);
        if (Vector3.Distance(closp, queryPos) > 23 && Vector3.Distance(closp, queryPos) < 34)
        {
            transform.position = queryPos;
        }
        else { transform.position += Vector3.Project(nextPositionDiff, Vector3.forward); }



    }
}
