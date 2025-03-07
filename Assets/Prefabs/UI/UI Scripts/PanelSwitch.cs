using UnityEngine;

public class PanelSwitch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject currentPanel;
    [SerializeField] GameObject targetPanel;

    public void SwitchPan()
    {
        currentPanel.SetActive(false);
        targetPanel.SetActive(true);

    }
}
