using UnityEngine;

public class CloseGame : MonoBehaviour
{

    
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#else
            Application.Quit(); // Close the game in a built application
#endif
        }
    }

