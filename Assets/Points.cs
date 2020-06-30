using UnityEngine;

public class Points : MonoBehaviour
{
    
    public int score = 0;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Point Check"))
        {
            score++;
            //AkSoundEngine.PostEvent("Point", gameObject);
            Debug.Log("Score!");
        }       
    }






}
