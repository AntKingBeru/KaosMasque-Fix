using UnityEngine;
using TMPro;

public class LoadingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI percentText;

    private void Update()
    {
        if (!SceneFader.Instance || !SceneFader.Instance.IsLoading)
            return;

        var percent = Mathf.RoundToInt(SceneFader.Instance.LoadingProgress * 100f);
        this.percentText.text = percent + "%";
    }
}