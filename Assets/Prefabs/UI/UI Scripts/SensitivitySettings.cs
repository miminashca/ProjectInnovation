using UnityEngine;
using UnityEngine.UI;

public class SensitivitySettings : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCameraConroller playerCameraController;
    [SerializeField] private Slider horizontalSlider;
    [SerializeField] private Slider verticalSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Load saved sensitivity values
        if (PlayerPrefs.HasKey("horizontalSensitivity"))
        {
            LoadSensitivity();
        }
        else
        {
            SetHorizontalSensitivity();
            SetVerticalSensitivity();
        }
    }
    public void SetHorizontalSensitivity()
    {
        float hSensitivity = horizontalSlider.value;
        if (playerMovement != null)
        {
            playerMovement.horizontalSensitivity = hSensitivity;
            PlayerPrefs.SetFloat("horizontalSensitivity", hSensitivity);
        }
    }
    public void SetVerticalSensitivity()
    {
        float vSensitivity = verticalSlider.value;
        if (playerCameraController != null)
        {
            playerCameraController.verticalSensitivity = vSensitivity;
            PlayerPrefs.SetFloat("verticalSensitivity", vSensitivity);
        }
    }
    private void LoadSensitivity()
    {
        horizontalSlider.value = PlayerPrefs.GetFloat("horizontalSensitivity");
        verticalSlider.value = PlayerPrefs.GetFloat("verticalSensitivity");

        SetHorizontalSensitivity();
        SetVerticalSensitivity();
    }


}
