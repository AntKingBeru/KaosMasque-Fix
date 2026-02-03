using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1f;
    
    [SerializeField] private float minimumLoadTime = 0.5f;
    
    [SerializeField] private GameObject loadingUI;

    public float LoadingProgress { get; private set; }
    public bool IsLoading { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        HideLoadingUI();
        StartCoroutine(Fade(1f, 0f));
    }

    public void FadeToScene(int buildIndex)
    {
        if (!IsLoading)
            StartCoroutine(FadeAndLoadAsync(buildIndex));
    }
    
    private IEnumerator FadeAndLoadAsync(int buildIndex)
    {
        IsLoading = true;
        ShowLoadingUI();
        LoadingProgress = 0f;

        yield return Fade(0f, 1f);

        var op = SceneManager.LoadSceneAsync(buildIndex);
        if (op != null)
        {
            op.allowSceneActivation = false;

            var timer = 0f;

            while (!op.isDone)
            {
                timer += Time.deltaTime;
                LoadingProgress = Mathf.Clamp01(op.progress / 0.9f);

                if (op.progress >= 0.9f && timer >= minimumLoadTime)
                    op.allowSceneActivation = true;

                yield return null;
            }
        }

        IsLoading = false;
        HideLoadingUI();

        yield return Fade(1f, 0f);
    }

    private IEnumerator Fade(float from, float to)
    {
        canvasGroup.blocksRaycasts = true;

        var time = 0f;
        canvasGroup.alpha = from;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = to;
        canvasGroup.blocksRaycasts = to > 0f;
    }
    
    private void ShowLoadingUI()
    {
        if (loadingUI != null)
            loadingUI.SetActive(true);
    }

    private void HideLoadingUI()
    {
        if (loadingUI != null)
            loadingUI.SetActive(false);
    }
}