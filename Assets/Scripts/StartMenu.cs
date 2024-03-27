using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SocketIOClient;
using System;

public class StartMenu : MonoBehaviour
{
    private SocketIO socket;

    public static int GenerateRandom4DigitNumber()
    {
        // Create a random number generator
        System.Random rand = new System.Random();

        // Generate a random 4-digit number (between 1000 and 9999)
        int randomNumber = rand.Next(1000, 10000);

        return randomNumber;
    }

    void Awake()
    {
        socket = new SocketIO("https://scoreboard-blhs.onrender.com"); // Corrected typo in "localhost"
        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("connected");
        };
        socket.ConnectAsync();
    }

    void Start()
    {
        socket.On("game-started", (d) =>
        {
            Debug.Log("Game started event received: ");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        });


        socket.On("game-ended", (data) =>
        {
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }


    public void StartGame()
    {
        // Create a dictionary for the data to be emitted
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["id"] = "retrt";//GenerateRandom4DigitNumber().ToString();
        data["playerName"] = "sa";
        data["roomId"] = "123456";

        // Emit the data
        socket.EmitAsync("join-room", data);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
}
