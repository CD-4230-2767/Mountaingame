using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerControler : MonoBehaviour
{

    public float jumpForce = 5f;
    private bool isGrounded;
    private Rigidbody rb;

    private float movementX;
    private float movementY;

    private int cotJum;
    private int cotSpe;
    private int count;

    public float speed = 5f;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI resultText;

    public AudioClip jumpSound;
    public AudioClip speedSound;
    public AudioClip winSound;

    private AudioSource audioSource;

    public GameObject panel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();

        cotJum = 0;
        cotSpe = 0;
        count = 0;

        SetCountText();

        resultText.gameObject.SetActive(false);

        panel.SetActive(true);
        StartCoroutine(ClosePanelAfterTime());
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    IEnumerator ClosePanelAfterTime()
    {
        yield return new WaitForSeconds(5f);
        panel.SetActive(false);
    }

    void SetCountText()
    {
        countText.text = "Velocidad: " + cotSpe + "\nSalto: " + cotJum;

        if (count >= 1)
        {
            resultText.text = "YOU WIN!";
            resultText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * (jumpForce + cotJum), ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ);

        rb.AddForce(movement * (speed + cotSpe));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            audioSource.PlayOneShot(winSound);

            SetCountText();
        }

        else if (other.CompareTag("pickup_Spe"))
        {
            other.gameObject.SetActive(false);
            cotSpe++;
            audioSource.PlayOneShot(speedSound);
            SetCountText();
        }

        else if (other.CompareTag("pickup_Jum"))
        {
            other.gameObject.SetActive(false);
            cotJum++;
            audioSource.PlayOneShot(jumpSound);
            SetCountText();
        }

        else if (other.CompareTag("Trampas"))
        {
            resultText.text = "YOU LOSE!";
            resultText.gameObject.SetActive(true);

            rb.isKinematic = true;
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Trapas"))
        {
            resultText.text = "YOU LOSE!";
            resultText.gameObject.SetActive(true);

            rb.isKinematic = true;
            gameObject.SetActive(false);
        }
    }
}