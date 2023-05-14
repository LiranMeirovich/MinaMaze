using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TriggerVictory : MonoBehaviour
{
    public GameObject mapPanel;
    public TMP_Text txt;
    public GameObject player;

    public Image black;
    public Animator fadeOut;

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (player)
        {
            mapPanel.GetComponent<Image>().enabled = true;
            txt.enabled = true;
            player.GetComponent<PlayerControlThrone>().enabled = false;

            fadeOut.SetBool("Fade", true);
            yield return new WaitUntil(() => black.color.a == 1);
            SceneManager.LoadScene("Menu");
        }
    }
}
