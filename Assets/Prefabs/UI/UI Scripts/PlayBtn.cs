using UnityEngine;

public class PlayBtn : MonoBehaviour
{

    [SerializeField] private GameObject PlayScreen;
    [SerializeField] private GameObject MenuScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Press()
    {
        PlayScreen.SetActive(true);
        MenuScreen.SetActive(false);
    }

}
