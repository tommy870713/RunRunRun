using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public AK.Wwise.Event footsteps;

    private void Step()
    {
        if(PlayerMovement.isGrounded)
        {
            footsteps.Post(gameObject);
            Debug.Log("step");
        }
    }
}
