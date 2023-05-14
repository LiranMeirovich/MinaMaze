using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorA : MonoBehaviour
{
    public PlayerControl player;
    public string enterAnimation;
    public string closeAnimation;
    public bool requiresKey;
    [SerializeField] private Animator myDoor = null;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BBB");
        if (other.CompareTag("Player"))
        {
            if (!requiresKey) { 
                Debug.Log("AAA");
                if (openTrigger)
                    myDoor.Play(enterAnimation, 0,0.0f);
            }
            else
            {
                if (player.gotKey(0))
                {
                    Debug.Log("AAA");
                    if (openTrigger)
                        myDoor.Play(enterAnimation, 0, 0.0f);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (closeTrigger)
        {
            myDoor.Play(closeAnimation, 0, 0.0f);
        }
    }
}
