using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GGOVideoPlay : MonoBehaviour {
    [SerializeField] VideoPlayer player = null;
    [SerializeField] GameObject toBeEnabled = null;

    private void OnEnable()
    {
        player.loopPointReached += EndPlayer;
    }

    private void OnDisable()
    {
        player.loopPointReached -= EndPlayer;
    }

    void EndPlayer(VideoPlayer p)
    {
        toBeEnabled.SetActive(true);
        Destroy(gameObject);
    }
}
