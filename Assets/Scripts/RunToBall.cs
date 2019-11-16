using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToBall : MonoBehaviour
{
    public GameObject Ball;
    public int movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Ball.transform);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;

    }
}
