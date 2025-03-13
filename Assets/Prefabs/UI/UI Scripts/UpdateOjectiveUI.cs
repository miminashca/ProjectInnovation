using TMPro;
using UnityEngine;

public class UpdateOjectiveUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI countertext;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  

    // Update is called once per frame
    void Update()
    {
        countertext.text = GameManager.Instance.currentAmountOfPickups + "/7";
    }
}
