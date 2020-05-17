using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public bool isGrounded = true;

    public int forwardForce;
    public int jumpSpeed;
    public int maxJump = 2;
    public int currentJump = 0;
    public float gravityMultiplier;

    void Start()
    {
        rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
    }

    void Update()
    {
       //pushes the character forward constantly
        rb.AddForce(forwardForce * Time.deltaTime, 0, 0);

        //Jumping
        if(Input.GetKeyDown("space") && (isGrounded || maxJump > currentJump))
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
            currentJump++;
        }
                     
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
            currentJump = 0;
            Debug.Log("On Floor!");
        }
        else if(collision.gameObject.tag == "Obstacle")
        {
            SceneManager.LoadScene("Intro"); 
        }
    }

    
}
