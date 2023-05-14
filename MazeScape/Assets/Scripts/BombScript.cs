using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    bool hasExploded = false;
    float countdown = 5f;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            hasExploded = true;
            ExplodeExplosion();
        }
    }
    void ExplodeExplosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.name == "Positive X wall"|| hitCollider.gameObject.name == "Negative X wall"|| hitCollider.gameObject.name == "Pos Z wall"|| hitCollider.gameObject.name == "Neg Z wall"||hitCollider.gameObject.name== "Minotaur") {
                Debug.Log("Test hit!");
                hitCollider.gameObject.SendMessage("ExplodeWall", transform.position, SendMessageOptions.DontRequireReceiver);
                hitCollider.gameObject.SendMessage("explodeNPC",SendMessageOptions.DontRequireReceiver);
            }
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
