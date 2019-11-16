using System.Collections;
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
    public GameObject player;
    private Player swing;
    private Room room;

    IEnumerator Start()
    {
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
            if (msgStr != null)
            {
                //Assign variable for the second string
                swing = JsonConvert.DeserializeObject<Player>(msgStr);
                string action = swing.action;
                Debug.Log("Connection received: " + swing.action + " from " + swing.playerId);

                //Check the force and determine the ball's velocity
                if (action == "soft") { }
                else { }
                
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
