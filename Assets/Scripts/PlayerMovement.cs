using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public CapsuleCollider col;
    public bool isGrounded = true;
    public bool isDoubleJumping = false;

    public int forwardForce;
    public float jumpSpeed;
    public int maxJump = 2;
    public int currentJump = 0;
    public int gravityMultiplier;
    public Animator animator;

    

    public float jumpTime;
    public float doubleJumpTime;

   

 

    void Update()
    {
       //pushes the character forward constantly
        rb.AddForce(forwardForce * Time.deltaTime, 0, 0);

        //Jumping
        if(Input.GetKeyDown("space") && (isGrounded || (maxJump -1) > currentJump))
        {
            animator.SetBool("isJumping", true);
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
            currentJump++;
            col.center = new Vector3(0f, 1.8f, 0f);
            col.height = 1.5f;

            
            StartCoroutine(jumpAnim());
            


        }
        else if (Input.GetKeyDown("space") && (isGrounded || (maxJump) > currentJump))
        {
            rb.AddForce(Vector3.up * (jumpSpeed / 1.4f), ForceMode.Impulse);
            isGrounded = false;
            currentJump++;
            animator.SetBool("isDoubleJumping", true);
            StartCoroutine(doubleJumpAnim());
            isDoubleJumping = true;
            col.center = new Vector3(0f, 2f, 0f);
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
            isDoubleJumping = false;

            Debug.Log("On Floor!");
        }
        else if(collision.gameObject.tag == "Obstacle")
        {
            SceneManager.LoadScene("Intro"); 
        }

        
    }

    IEnumerator jumpAnim()
    {
        

        yield return new WaitForSeconds(jumpTime);
        animator.SetBool("isJumping", false);
        if(!isDoubleJumping)
        {
            col.center = new Vector3(0f, 0.86f, 0f);
            col.height = 1.72f;
        }

        Debug.Log("wait");


    }

    IEnumerator doubleJumpAnim()
    {


        yield return new WaitForSeconds(doubleJumpTime);
        animator.SetBool("isDoubleJumping", false);
       
        col.center = new Vector3(0f, 0.86f, 0f);
        col.height = 1.72f;

        Debug.Log("wait");


    }




}
