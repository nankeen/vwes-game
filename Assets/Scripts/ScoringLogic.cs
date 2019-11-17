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

    private bool GameOver = false;
    private int winner = -1;

    public bool ballOutBounds = false;
    public bool P1lasthitter = false; //if false, p2 definitely was last hitter

    public bool ballHasCollided = false;
    public bool ballInP1Half = false;

    public int bounceCountP1 = 0; //these 4 public for debugging but should be priv
    public int bounceCountP2 = 0;
    public int P1Score;
    public int P2Score;


    // Start is called before the first frame update
    void Start()
    {
        score.text =  toTennisScore(P1Score, P2Score);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            bounceCountP2 = 0;
            ballInP1Half = false;
        }
    }

    private string toTennisScore(int score1, int score2) //Assumes a player hasn't already won
    {
        if (GameOver)
        {
            if (winner == 1)
            {
                return "P1 Wins!";
            }
            else
            {
                return "P2 Wins!";
            }

        }
        if ((score1 == score2) && (score2 == 0))
        {
            return "Join code: " + HitLogic.GetComponent<web>().roomIdPublic;
        }
        if (score1 > 3 || score2 > 3)//means we have gone to deuce and advantage
        {
            if (score1 > score2)
            {
                return "P1: Advantage | P2: Deuce";
            }
            else if (score2 > score1){
                return "P1: Deuce | P2: Advantage";
            } else {return "P1: Deuce | P2: Deuce"; }
        }
        return "P1: " + tennisMult(score1) + "  | P2: " + tennisMult(score2);


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

    public void resetCourt()
    {
        if (P1Score <= 3 && P2Score <= 3)
        {
            //do nothing, nobody has won
        }
        else if (P1Score > 3 && P2Score < 3)
        {
            GameOver = true;
            winner = 1;
        }
        else if (P2Score > 3 && P1Score < 3)
        {
            GameOver = true;
            winner = 2;
        }
        else if (P2Score - P1Score > 1)
        {
            GameOver = true;
            winner = 2;
        }
        else {
            GameOver = true;
            winner = 1;
        }

        bounceCountP1 = 0;
        bounceCountP2 = 0;
        score.text = toTennisScore(P1Score, P2Score);
        GameObject ball = GameObject.Find("Ball");
        ball.transform.position = new Vector3(-21.2f, 11.89f, 9.87f); // uh oh
        HitLogic.GetComponent<web>().freezeBall();
        //TODO
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            bounceCountP1 = 0;
            ballInP1Half = true;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (ballOutBounds)
        {
            ballHasCollided = false;
            if (P1lasthitter)
            {
                ballOutBounds = false;
                if (P1lasthitter && bounceCountP1 == 1)
                {
                    P1Score++;
                }
                else { P2Score++; }
                resetCourt();

            }
            else
            {
                ballOutBounds = false;
                if ((!P1lasthitter) && bounceCountP2 == 1)
                {
                    P2Score++;
                }
                else { P1Score++; }
                
                resetCourt();
            }

        }


        if (ballHasCollided && (!ballOutBounds))
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
