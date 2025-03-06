using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject targetPanel;

    
    // Update is called once per frame
    void Update()
    {
           
    }
    public void On()
    {
        targetPanel.SetActive(true);
    } 
    public void Off()
    {
        targetPanel.SetActive(false);
    }
}
