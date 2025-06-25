using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CanvasFadeInAndOut : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private string nextSceneName;

    [Header("Referencias")]
    [SerializeField] private Button transitionButton;
    [SerializeField] private GameObject[] imageHolders;

    private int currentIndex = 0;
    private bool isTransitioning = false;

    private void Start()
    {
        for (int i = 0; i < imageHolders.Length; i++)
        {
            SetImagesAlpha(imageHolders[i], i == 0 ? 1f : 0f);
            imageHolders[i].SetActive(i == 0);
        }

        transitionButton.onClick.AddListener(StartTransition);
    }

    private void StartTransition()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionImages());
        }
    }

    private IEnumerator TransitionImages()
    {
        isTransitioning = true;

        GameObject currentHolder = imageHolders[currentIndex];
        yield return StartCoroutine(FadeImages(currentHolder, 1f, 0f));
        currentHolder.SetActive(false);

        currentIndex++;
        if (currentIndex >= imageHolders.Length)
        {
            SceneManager.LoadScene(nextSceneName);
            yield break;
        }

        GameObject nextHolder = imageHolders[currentIndex];
        nextHolder.SetActive(true);
        yield return StartCoroutine(FadeImages(nextHolder, 0f, 1f));

        isTransitioning = false;
    }

    private IEnumerator FadeImages(GameObject holder, float startAlpha, float endAlpha)
    {
        Image[] images = holder.GetComponentsInChildren<Image>(true);
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);

            foreach (var img in images)
            {
                Color c = img.color;
                c.a = Mathf.Lerp(startAlpha, endAlpha, t);
                img.color = c;
            }

            yield return null;
        }

        foreach (var img in images)
        {
            Color c = img.color;
            c.a = endAlpha;
            img.color = c;
        }
    }

    private void SetImagesAlpha(GameObject holder, float alpha)
    {
        Image[] images = holder.GetComponentsInChildren<Image>(true);
        foreach (var img in images)
        {
            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }
    }
}
