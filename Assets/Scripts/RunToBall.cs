﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToBall : MonoBehaviour
{
    public GameObject Ball;
    public float movementSpeed;
    public BoxCollider net;
    public Vector3 toBall;
    public Camera cam;
    private Vector3 courtPlaneNormal = Vector3.up;
    private Vector3 nextPositionDiff;
    private Vector3 queryPos;

    private bool inBoundary = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //always face camera
        transform.LookAt(cam.transform);


        //move towards ball
        toBall = Ball.transform.position - transform.position;
        nextPositionDiff = Vector3.Normalize(Vector3.ProjectOnPlane(toBall, courtPlaneNormal)) * movementSpeed * Time.deltaTime;

        //if not about to cross over court
        queryPos = transform.position + nextPositionDiff;
        Vector3 closp = net.ClosestPointOnBounds(queryPos);
        if (Vector3.Distance(closp, queryPos) > 20)
        {
            transform.position = queryPos;
        }

        

    }
}
