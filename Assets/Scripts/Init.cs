using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Init : MonoBehaviour
{
    static bool checkFireBaseDone = false;
    static bool fetchRemoteConfigDone = false;
    float interval = 3.0f;

    [SerializeField] Text loadingText;

    public GameObject videoDemo;

    IEnumerator Start()
    {
        loadingText.text = "Initializing";
        yield return null;
        loadingText.text = "Initializing Settings";
        MainThreadDispatcher.Init();

        yield return new WaitForSeconds(1);

        DG.Tweening.DOTween.Init();

        yield return null;

        loadingText.text = "Initializing Firebase";

        yield return new WaitForSeconds(2);
        loadingText.text = "Initializing Database";

        yield return new WaitForSeconds(0.5f);

        loadingText.text = "Initializing Auth";

        yield return new WaitForSeconds(1);

        yield return null;
        loadingText.text = "Initializing Config";
        
        yield return new WaitForSeconds(1);
        
        MainThreadDispatcher.Invoke(() =>
        {
            videoDemo.SetActive(true);
            gameObject.SetActive(false);
        });
    }
    
}