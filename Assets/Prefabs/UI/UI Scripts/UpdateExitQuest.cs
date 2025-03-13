using TMPro;
using UnityEngine;

public class UpdateExitQuest : MonoBehaviour
{
    [SerializeField] public GameObject quest;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        quest.SetActive(false);
    }
    // Update is called once per frame
    public void EnableExit()
    {
        if (GameManager.Instance.currentAmountOfPickups > 0)
        {
            quest.SetActive(true);
        }
        else
        {
            quest.SetActive(false);
        }
    }
    void Update()
    {
       EnableExit();
    }
}
