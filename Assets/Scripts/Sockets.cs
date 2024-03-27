using SocketIOClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WSockets : MonoBehaviour
{
    public static SocketIO socket; // Change to static
    // Start is called before the first frame update
    void Start()
    {
        socket = new SocketIO("https://scoreboard-blhs.onrender.com"); // Corrected typo in "localhost"
        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("connected");
        };
        socket.ConnectAsync();
    }
}
