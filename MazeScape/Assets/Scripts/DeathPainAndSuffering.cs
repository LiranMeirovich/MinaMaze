using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathPainAndSuffering : MonoBehaviour
{
    public GameObject mapPanel;
    public TMP_Text txt;
    public GameObject player;

    public Image black;
    public Animator fadeOut;
    public bool transitioned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerControl>().health <= 0&& !transitioned)
        {
            mapPanel.GetComponent<Image>().enabled = true;
            txt.enabled = true;
            player.GetComponent<PlayerControl>().enabled = false;
            fadeOut.SetBool("Fade", true);
            transitioned = true;
            StartCoroutine(waitForTransition());

        }
    }

    private IEnumerator waitForTransition()
    {
        Debug.Log("Transition");
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene("Menu");

    }
}
