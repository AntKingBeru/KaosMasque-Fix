using UnityEngine;
using TMPro;

public class TextBreathing : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    [Header("Fade Settings")]
    [SerializeField] private float fadeSpeed = 5.75f;
    [Range(0f, 1f)] [SerializeField] private float minAlpha = 0.1f;
    [Range(0f, 1f)] [SerializeField] private float maxAlpha = 1f;

    [Header("Font Size Breathing")]
    [Tooltip("Maximum size multiplier (e.g. 1.05 = +5%)")]
    [SerializeField] private float sizeMultiplier = 1f;
    
    [Tooltip("Relative speed vs fade (0.5 = half as fast)")]
    [Range(0.1f, 1f)] [SerializeField] private float sizeSpeedMultiplier = 0.5f;

    private float _baseFontSize;
    private float _time;

    private void Awake()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();

        _baseFontSize = text.fontSize;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        // ---- Fade (faster rhythm) ----
        var fadeWave = Mathf.Sin(_time * fadeSpeed) * 0.5f + 0.5f;
        var alpha = Mathf.Lerp(minAlpha, maxAlpha, fadeWave);

        var color = text.color;
        color.a = alpha;
        text.color = color;

        // ---- Font size breathing (slower rhythm) ----
        var sizeWave = Mathf.Sin(_time * fadeSpeed * sizeSpeedMultiplier) * 0.5f + 0.5f;
        var size = Mathf.Lerp(_baseFontSize, _baseFontSize * sizeMultiplier, sizeWave);

        text.fontSize = size;
    }
}