using UnityEngine;

public class Points : MonoBehaviour
{
    
    public int score;
   
    private void Start()
    {
        score = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Point Check"))
        {
            score++;
            AkSoundEngine.PostEvent("Point", gameObject);
            Debug.Log("Score!");
        }

    }






}
