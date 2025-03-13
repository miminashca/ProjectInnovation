using UnityEngine;

public class TutorialScreneDisable : MonoBehaviour
{
    [SerializeField] GameObject TutorialPanel;
    [SerializeField] float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("HidePanel", timer);
    }

    // Update is called once per frame
    void HidePanel()
    {
        TutorialPanel.SetActive(false);
    }
}
