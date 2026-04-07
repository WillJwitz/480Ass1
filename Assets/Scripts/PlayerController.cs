using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    private bool grounded = true;
    private bool dJump = true;

    public float speed = 0;
    public float jumpForce = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;   
        winTextObject.SetActive(false);
        SetCountText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y;
    }
    //Implements jump and double jump
    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {   
            if(grounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                grounded = false;
            } else if (dJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                dJump = false;
            }
            
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
        
    }

    
    // Double jump reset
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            grounded = true;
            dJump = true;
        }
    }

    void SetCountText() 
   {
        countText.text =  "Count: " + count.ToString();
        if (count >= 12)
       {
           winTextObject.SetActive(true);
       }
   }
}
