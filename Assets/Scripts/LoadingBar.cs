using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class LoadingBar : MonoBehaviour
{
    [SerializeField] Image filledLoadingImage = null;
    public TMPro.TMP_Text loadinText;

    private void OnEnable()
    {
        //loadingBar.sizeDelta = GetPercentToVector2(percent);
        StartCoroutine(Animate());
    }

    private void OnDisable()
    {
        StopCoroutine(Animate());
    }

    public void SetActive(bool yes)
    {
        gameObject.SetActive(yes);
    }

    IEnumerator Animate()
    {
        
        while (true)
        {
            float randFill = Random.Range(0.005f, 0.03f);
            filledLoadingImage.fillAmount += randFill;
            loadinText.text = string.Format("Loading...{0}%", (int)(Mathf.Min(filledLoadingImage.fillAmount*100, 100)));

            float randWait = Random.Range(0.01f, 0.1f);
            yield return new WaitForSecondsRealtime(randWait);

            if (filledLoadingImage.fillAmount >= 1)
            {
                break;
            }
        }
        
        yield return new WaitForSecondsRealtime(1.0f);
        
        MainThreadDispatcher.Invoke(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            gameObject.SetActive(false);
        });
    }
}
