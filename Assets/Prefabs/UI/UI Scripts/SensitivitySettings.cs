using UnityEngine;
using UnityEngine.UI;

public class SensitivitySettings : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCameraConroller playerCameraController;
    [SerializeField] private Slider horizontalSlider;
    [SerializeField] private Slider verticalSlider;

    void Start()
    {
        // Check if saved preferences exist, then load; otherwise, set the default slider values.
        if (PlayerPrefs.HasKey("horizontalSensitivity") && PlayerPrefs.HasKey("verticalSensitivity"))
        {
            LoadSensitivity();
        }
        else
        {
            // Use the current slider values as defaults and save them.
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
            PlayerPrefs.Save(); // Save to persist the change
        }
    }

    public void SetVerticalSensitivity()
    {
        float vSensitivity = verticalSlider.value;
        if (playerCameraController != null)
        {
            playerCameraController.verticalSensitivity = vSensitivity;
            PlayerPrefs.SetFloat("verticalSensitivity", vSensitivity);
            PlayerPrefs.Save(); // Save to persist the change
        }
    }

    private void LoadSensitivity()
    {
        horizontalSlider.value = PlayerPrefs.GetFloat("horizontalSensitivity");
        verticalSlider.value = PlayerPrefs.GetFloat("verticalSensitivity");

        // Update the in-game settings using the loaded values
        SetHorizontalSensitivity();
        SetVerticalSensitivity();
    }
}
