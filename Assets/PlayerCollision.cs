using UnityEngine;
using System.Collections;


public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Rigidbody rb;
    public Animator animator;


    private void OnCollisionEnter(Collision collision)
    {
     
        if (collision.gameObject.tag == "Floor")
        {

            playerMovement.isGrounded = true;
            playerMovement.isJumping = false;

            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("You Lose");
            FindObjectOfType<GameManager>().EndGame();
        }
   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Victory"))
        {
            StartCoroutine(winWait());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Inner"))
        {
            playerMovement.isGrounded = false;
            Debug.Log("Left Inner");
        }

        
    }

    IEnumerator winWait()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Victory", true);
        playerMovement.moveSpeed = 0;
    }
}
