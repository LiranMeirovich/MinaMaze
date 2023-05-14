using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestCollider : MonoBehaviour
{
    public ChestControl chest;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AAAAAAAAAAAA");
        if (other.CompareTag("Player"))
            chest.updateNear(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            chest.updateNear(false);
    }
}
