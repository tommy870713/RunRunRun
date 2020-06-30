using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
   
    public float jumpSpeed;
    public float jumpTime;
    public float moveSpeed;
    public bool isGrounded = true;
    
    public bool isJumping;
    public bool isSliding;

    private void Start()
    {
        moveSpeed = 15;
        jumpSpeed = 4.15f;
        isJumping = false;

    }

    void FixedUpdate()
    {

        //Constant Velocity moving the character forward (x direction)z
        rb.velocity = new Vector3(moveSpeed, rb.velocity.y, rb.velocity.z);

        if ((Input.GetKeyDown("space") || Input.GetKey("space")) && isGrounded && !isSliding)
        {
            isJumping = true;
            isGrounded = false;            
            animator.SetBool("Jump", true);
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            StartCoroutine(animWait());          
        }
        else if ((Input.GetKeyDown("f") || Input.GetKey("f")) && isGrounded && !isSliding)
        {
           
            isGrounded = false;
            animator.SetBool("Flip", true);
            rb.useGravity = false;
            StartCoroutine(delay());
        }
        else if ((Input.GetKeyDown("s") || Input.GetKey("s")) && isGrounded)
        {
            
            animator.SetBool("Slide", true);
            isSliding = true;         
            StartCoroutine(delayAgain());
            
        }
        else if (Input.GetKeyDown("p"))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        

        if (isJumping)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
        if (!isGrounded)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
                      
    }

    
  
    IEnumerator animWait()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Jump", false);
         
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.7f);
        animator.SetBool("Flip", false);

        yield return new WaitForSeconds(0.3f);
        rb.useGravity = true;
        
    }
   
   
    IEnumerator delayAgain()
    {
        yield return new WaitForSeconds(0.7f);
        
        animator.SetBool("Slide", false);

        yield return new WaitForSeconds(0.5f);

        isSliding = false;

    }    

    void callFootstep()
    {
        //AkSoundEngine.PostEvent("Footsteps", gameObject);
        //Debug.Log("Step");
    }

  





}
