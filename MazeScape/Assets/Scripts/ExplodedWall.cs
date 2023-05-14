using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodedWall : MonoBehaviour
{
    [SerializeField] GameObject[] bricks;
    // Start is called before the first frame update
    float countdown = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            Destroy(gameObject);
        }
    }
    public void ExplodeShatter(float power, Vector3 expoisionPos, float radius)
    {
        for(int i = 0; i < bricks.Length; i++)
        {
            bricks[i].GetComponent<Rigidbody>().AddExplosionForce(power, expoisionPos, radius);
        }

    }
}
