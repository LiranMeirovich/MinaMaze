using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SendMessage("hit_arrow", SendMessageOptions.DontRequireReceiver);
        Debug.Log(collision.gameObject.name);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        collision.gameObject.SendMessage("hit_arrow", SendMessageOptions.DontRequireReceiver);
        Debug.Log(collision.gameObject.name);
        Destroy(gameObject);
    }
    private void aaa(Collider collision)
    {
        collision.gameObject.SendMessage("hit_arrow", SendMessageOptions.DontRequireReceiver);
        Debug.Log(collision.gameObject.name);
        Destroy(gameObject);
    }
}
