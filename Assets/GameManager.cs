using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private PlayerMovement playerMovement;

    public void EndGame()
    {
        SceneManager.LoadScene("samplescene");
    }




}
