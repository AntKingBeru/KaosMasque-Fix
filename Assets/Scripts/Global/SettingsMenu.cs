using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu Instance;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // Optional: ESC closes menu
        if (gameObject.activeSelf && InputSystemEscapePressed())
        {
            Close();
        }
    }

    private bool InputSystemEscapePressed()
    {
#if ENABLE_INPUT_SYSTEM
        return UnityEngine.InputSystem.Keyboard.current?.escapeKey.wasPressedThisFrame == true;
#else
        return Input.GetKeyDown(KeyCode.Escape);
#endif
    }
}