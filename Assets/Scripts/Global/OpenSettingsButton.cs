using UnityEngine;

public class OpenSettingsButton : MonoBehaviour
{
    public void OpenSettings()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.OpenSettings();
        }
        else
        {
            Debug.LogWarning("UIManager not found.");
        }
    }
}