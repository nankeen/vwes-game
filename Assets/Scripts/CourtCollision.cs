using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtCollision : MonoBehaviour
{
    public GameObject CourtSideDetector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball") //if ball hit the court
        {
            CourtSideDetector.GetComponent<ScoringLogic>().ballHasCollided = true;
        }
    }
}
