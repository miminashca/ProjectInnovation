using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Updatequestlotest : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    private int currentCount = 0;
    private int maxCount = 7;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        counterText = GameObject.Find("Active Quest Attr")?.GetComponent<TextMeshProUGUI>();
        UpdateCounterText();
          
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncrementCounter();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DecrementCounter();
        }
    }

    void IncrementCounter()
    {
        if (currentCount < maxCount)
        {
            currentCount++;
            UpdateCounterText();
        }
    }

    void DecrementCounter()
    {
        if (currentCount > 0)
        {
            currentCount--;
            UpdateCounterText();
        }
    }

    void UpdateCounterText()
    {
        counterText.text = $"{currentCount} / {maxCount}";
    }
}

