using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class playerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;
    public Text timeText;

    private float timeLeft = 60.0f;
    private Rigidbody rb;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 7;
        SetCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        timeLeft -= Time.deltaTime;
       
        if (timeLeft < 0)
        {
            timeLeft = 0;
            winText.text = "You Lose!";
            rb.gameObject.SetActive(false);
        }
        timeText.text = "Time:" + timeLeft.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count - 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            if (count == 0)
            {
                winText.text = "You Win!";
                rb.gameObject.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("Treetag"))
        {
            winText.text = "You Lose!";
            rb.gameObject.SetActive(false);
        }
    }

    void SetCountText()
    {
        countText.text = "PickUp Left: " + count.ToString();
    }
    
}