using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringLogic : MonoBehaviour
{
    public Collider P1SideDetector;
    public Collider Net;
    public Text score;
    public bool ballInP1Half = false;
    private int bounceCountP1 = 0;
    private int bounceCountP2 = 0;
    public bool ballHasCollided = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            ballInP1Half = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            ballInP1Half = false;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (ballHasCollided)
        {
            if (ballInP1Half)
            {
                bounceCountP1++;
                ballHasCollided = false;
            }
            else{
                bounceCountP2++;
                ballHasCollided = false;
            }
        }

        //if(bounceCountP1 > )

    }

}
