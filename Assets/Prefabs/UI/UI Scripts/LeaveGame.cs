using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void QuitToMainMenu()
    {
        SceneManager.LoadScene(1); //1 is lobby scene
        //SceneManager.LoadScene("LobbyScene"); //accessing scene by name change the name if yall change the scene name
    }
}
