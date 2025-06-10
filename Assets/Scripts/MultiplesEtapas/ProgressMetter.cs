using UnityEngine;
using UnityEngine.UI;

public class ProgressMetter : MonoBehaviour
{
    public Image progressBar;
    public float maxProgress;
    float progress;


    void FillProgressBar()
    {
        progress += Time.deltaTime;
        progressBar.fillAmount = progress / maxProgress;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FillProgressBar();
    }
}
