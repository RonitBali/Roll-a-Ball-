using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using SocketIOClient;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed;
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private SocketIO socket;
    public AudioSource coinaudio;
    public float sceneLoadDelay = 3f;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
       
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        if (count >= 10 )
        {
            winTextObject.SetActive(true);
            StartCoroutine(LoadNextSceneWithDelay());

        }

        IEnumerator LoadNextSceneWithDelay()
        {
            Debug.Log("LoadNextSceneWithDelay coroutine started.");
            yield return new WaitForSeconds(sceneLoadDelay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY );
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {   if (other.gameObject.CompareTag("Collectible"))
            { 
            
        other.gameObject.SetActive(false);
            count= count + 1;
            if (coinaudio!= null)
            {
                coinaudio.Play();
            }
           



           /* Dictionary<string, string> data = new Dictionary<string, string>();
            data["player_id"] = "retrt";//GenerateRandom4DigitNumber().ToString();
            data["score"] = count.ToString();
            data["room_id"] = "123456";

            // Emit the data
            socket.EmitAsync("goal-score", data);*/
            SetCountText();

        }
    }
}
