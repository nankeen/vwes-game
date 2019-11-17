﻿using System.Collections;
using UnityEngine;
using System;
using Newtonsoft.Json;

//Room data
public class Room
{
    public string roomId;
    public int playersConnected;
};

//Player Data
public class Player
{
    public string playerId;
    public string action;
};

public class web : MonoBehaviour
{
    public bool inP1HitBox = false;
    public bool inP2HitBox = false;

    public GameObject player;
    public GameObject Ball;
    public float smallHitForce;
    public float bigHitForce;

    private Player swing;
    private Room room;
    //private Vector3 toBall;
    private Rigidbody ballrb;

    Vector3 GenHitVector()
    {
        Vector3 hitVec = new Vector3();
        hitVec.z = -(transform.position.z + GaussianRandom.generateNormalRandom(0, 3)); //Gaussian noise added to z. Range -23 to +23 depend on pos
        //positive x if player2, neg if player1
        hitVec.x = 47;
        if (swing.playerId == "1")
        {
            hitVec.x *= -1;
        }
        hitVec.y = 10 + GaussianRandom.generateNormalRandom(0, 1);
        return hitVec;
    }

    public void freezeBall()
    {
        ballrb.isKinematic = true;
    }


    IEnumerator Start()
    {
        ballrb = Ball.GetComponent<Rigidbody>();

        ballrb.Sleep();

        WebSocket w = new WebSocket(new Uri("ws://vwes-backend.uksouth.azurecontainer.io/ws/new/"));
        yield return StartCoroutine(w.Connect());
        string roomStr = w.RecvString();
        room = JsonConvert.DeserializeObject<Room>(roomStr);

        //The reply will be the info about the room
        Debug.Log("Welcome to the game room " + room.roomId);
        Debug.Log("Now we have " + room.playersConnected + " player");

        while (true)
        {
            //Creates a variable of the response that the server might have sent to us
            string msgStr = w.RecvString();

            

            if (msgStr != null )//checks if message nonempty then whether swing valid
            {
                //Assign variable for the second string
                swing = JsonConvert.DeserializeObject<Player>(msgStr);
                string action = swing.action;
                Debug.Log("Connection received: Player " + swing.playerId);



                if ((swing.playerId == "1" && inP1HitBox) || (swing.playerId == "0" && inP2HitBox)) //checks swing valid
                {
                    //Check the force and determine the ball's velocity
                    if (action == "soft")
                    {
                        ballrb.WakeUp();
                        ballrb.isKinematic = false;
                        //toBall = (Ball.transform.position - transform.position) / (Ball.transform.position - transform.position).magnitude;
                        Debug.Log("hit soft by " + swing.playerId);
                        ballrb.AddForce(Vector3.Scale(GenHitVector(), ((new Vector3(1,0,1)) * smallHitForce) + new Vector3(0,3,0)));
                    }
                    else
                    {
                        ballrb.WakeUp();
                        ballrb.isKinematic = false;
                        //toBall = toBall = (Ball.transform.position - transform.position) / (Ball.transform.position - transform.position).magnitude;
                        Debug.Log("hit hard by " + swing.playerId);
                        ballrb.AddForce(Vector3.Scale(GenHitVector(), ((new Vector3(1, 0, 1)) * bigHitForce) + new Vector3(0, 2.5f, 0)));
                    }
                }
            }
            if (w.error != null)
            {
                Debug.LogError("Error: " + w.error);
                break;
            }
            yield return 0;
        }
        w.Close();
    }
}
