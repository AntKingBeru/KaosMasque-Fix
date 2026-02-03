using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PressAnyKey : MonoBehaviour
{
    private bool _pressed;

    private void Update()
    {
        if (_pressed) return;

        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            Continue();
        }
    }
    
    private void Continue()
    {
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        var nextIndex = currentIndex + 1;

        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("No next scene in Build Settings.");
            return;
        }

        _pressed = true;
        SceneFader.Instance.FadeToScene(nextIndex);
    }
}