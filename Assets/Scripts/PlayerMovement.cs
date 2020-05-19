using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public CapsuleCollider col;
    
       
    public int forwardForce;
    public float jumpSpeed;
    public float gravityMultiplier;
    public float jumpTime;
    public float doubleJumpTime;
    public float moveSpeed;
    public bool isGrounded = true;

    private int maxJump = 2;
    private int currentJump = 0;

    void Update()
    {
       //pushes the character forward constantly
        rb.AddForce(forwardForce * Time.deltaTime, 0, 0);

        //Moving Left & Right
        if(Input.GetKey("a"))
        {
            rb.AddForce(Vector3.forward * moveSpeed, ForceMode.Acceleration);
        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(Vector3.back * moveSpeed, ForceMode.Acceleration);
        }

        //Jumping
        if (Input.GetKeyDown("space") && (isGrounded || (maxJump -1) > currentJump))
        {
            animator.SetBool("isJumping", true);
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
            currentJump++;                   
            StartCoroutine(jumpAnim());
            
        }
        else if (Input.GetKeyDown("space") && (isGrounded || (maxJump) > currentJump))
        {
            rb.AddForce(Vector3.up * (jumpSpeed / 1.4f), ForceMode.Impulse);
            gravityMultiplier *= 2.0f;
            isGrounded = false;
            currentJump++;
            //animator.SetBool("isDoubleJumping", true);   
        }

        //Gravity Multiplier
        if(!isGrounded)
        {
            rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
            currentJump = 0;
            
            gravityMultiplier = 6f;

            
        }
        else if(collision.gameObject.tag == "Obstacle")
        {
            AkSoundEngine.PostEvent("Cancel_Point", gameObject);
            FindObjectOfType<GameManager>().EndGame();
        }  
        else if(collision.gameObject.tag == "Point")
        {
            Destroy(collision.gameObject);                    
        }
        else if(collision.gameObject.tag == "Finish")
        {
            animator.SetBool("Win", true);
            forwardForce = 0;
        }
        
    }

    IEnumerator jumpAnim()
    {
        

        yield return new WaitForSeconds(jumpTime);
        animator.SetBool("isJumping", false);
        

    }

   

  
}
