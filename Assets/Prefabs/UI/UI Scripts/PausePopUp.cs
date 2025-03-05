using UnityEngine;

public class PausePopUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject PausePanel;

    
    // Update is called once per frame
    void Update()
    {
           
    }
    public void Pause()
    {
        PausePanel.SetActive(true);
    } 
    public void Continue()
    {
        PausePanel.SetActive(false);
    }
}
