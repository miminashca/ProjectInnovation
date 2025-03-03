using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    private bool isMenuOpen = false;

    void Update()
    {
        // Example: Press Esc to toggle the menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);

        // Unlock cursor if menu is open
        if (isMenuOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Lock cursor again when menu closes
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
