using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringLogic : MonoBehaviour
{
    public Collider P1SideDetector;
    public Collider Net;
    public Text score;
    public GameObject HitLogic;

    public bool ballInP1Half = false;
    private int bounceCountP1 = 0;
    private int bounceCountP2 = 0;
    public bool ballHasCollided = false;
    private int P1Score;
    private int P2Score;


    // Start is called before the first frame update
    void Start()
    {
        score.text = "Score: " + toTennisScore(P1Score, P2Score);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            ballInP1Half = true;
        }
    }

    private string toTennisScore(int score1, int score2) //Assumes a player hasn't already won
    {
        if (score1 > 3 || score2 > 3)//means we have gone to deuce and advantage
        {
            if (score1 > score2)
            {
                return "Advantage | Deuce";
            }
            else {
                return "Deuce | Advantage";
            }
        }
        return tennisMult(score1) + "  |  " + tennisMult(score2);


    }
    private string tennisMult(int score)
    {
        switch (score)
        {
            case 0:
                return "Luv";
            case 3:
                return "40";
            default:
                return (score * 15).ToString();
        }
    }

    private void resetCourt()
    {
        score.text = "Score: " + toTennisScore(P1Score, P2Score);
        GameObject ball = GameObject.Find("Ball");
        ball.transform.position = new Vector3(-21.2f, 11.89f, 9.87f); // uh oh
        HitLogic.GetComponent<web>().freezeBall();
        //TODO
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
                bounceCountP2 = 0;
                ballHasCollided = false;
            }
            else{
                bounceCountP2++;
                bounceCountP1 = 0;
                ballHasCollided = false;
            }
        }

        if (bounceCountP1 > 1)
        {
            P1Score++;
            bounceCountP1 = 0;
            resetCourt();
        }
        if (bounceCountP2 > 1)
        {
            P2Score++;
            bounceCountP2 = 0;
            resetCourt();
        }

    }

}
