using UnityEngine;
using System.Collections;
using System.Threading;
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

        if ((Input.GetKeyDown("space") || Input.GetKey("space")) && isGrounded)
        {
            isJumping = true;
            isGrounded = false;            
            animator.SetBool("Jump", true);
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            StartCoroutine(animWait());          
        }
        else if ((Input.GetKeyDown("f") || Input.GetKey("f")) && isGrounded)
        {
           
            isGrounded = false;
            animator.SetBool("Flip", true);
            rb.useGravity = false;
            StartCoroutine(delay());
        }
        else if ((Input.GetKeyDown("s") || Input.GetKey("s")) && isGrounded)
        {
            
            animator.SetBool("Slide", true);
            
            StartCoroutine(delayAgain());
            Debug.Log("Slide");
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

       
                       
    }

    private void OnCollisionEnter(Collision collision)
    {
       
     
        if(collision.gameObject.tag == "Finish")
        {
            StartCoroutine(winWait());
        }

        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
            isJumping = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
        
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("You Lose");
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
          
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fall Check")
        {
            isGrounded = false;
            Debug.Log("Falling");
        }
    }



    IEnumerator animWait()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Jump", false);
        
        
        
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("Flip", false);
        rb.useGravity = true;
    }

    IEnumerator winWait()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Victory", true);
        moveSpeed = 0;
    }
   
    IEnumerator delayAgain()
    {
        yield return new WaitForSeconds(0.5f);
        
        animator.SetBool("Slide", false);
    }    

    void callFootstep()
    {
        //AkSoundEngine.PostEvent("Footsteps", gameObject);
        //Debug.Log("Step");
    }

  





}
