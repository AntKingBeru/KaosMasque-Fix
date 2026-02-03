using UnityEngine;
using UnityEngine.UI;

public class TitleColorCycle : MonoBehaviour
{
    [SerializeField] private Image targetImage;

    [SerializeField] private Color[] colors =
    {
        Color.red,
        Color.orange,
        Color.yellow,
        Color.green,
        Color.blue,
        Color.indigo,
        Color.violet
    };

    [SerializeField] private float transitionSpeed = 1f;

    private int _currentColorIndex;
    private int _nextColorIndex = 1;
    private float _time;

    private void Update()
    {
        if (colors.Length < 2) return;

        _time += Time.deltaTime * transitionSpeed;
        targetImage.color = Color.Lerp(
            colors[_currentColorIndex],
            colors[_nextColorIndex],
            _time
        );

        if (!(_time >= 1f)) return;
        _time = 0f;
        _currentColorIndex = _nextColorIndex;
        _nextColorIndex = (_nextColorIndex + 1) % colors.Length;
    }
}