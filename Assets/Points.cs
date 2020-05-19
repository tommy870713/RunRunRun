using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    
    public int score;
    
    private PlayerMovement PlayerMovement;

    private void Start()
    {
        score = 0;
    }
    private void pointChecker()
    {
        if (gameObject == null)
        {
            score++;
            AkSoundEngine.PostEvent("Point", gameObject);
            Debug.Log("Score!");
        }
    }
       
    

    


}
