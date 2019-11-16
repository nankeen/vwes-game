using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToBall : MonoBehaviour
{
    public GameObject Ball;
    public float movementSpeed;
    public Vector3 toBall;
    public Camera cam;
    private Vector3 courtPlaneNormal = Vector3.up;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform);
        toBall = Ball.transform.position - transform.position;
        transform.position += Vector3.Normalize(Vector3.ProjectOnPlane(toBall, courtPlaneNormal)) * movementSpeed * Time.deltaTime;

    }
}
