using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wall : MonoBehaviour
{
    [SerializeField] ExplodedWall wallPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ExplodeWall( Vector3 expoisionPos)
    {
        GetComponent<NavMeshObstacle>().carving = false;
        ExplodedWall newWall = Instantiate(wallPrefab, this.transform.position, Quaternion.identity);
        newWall.ExplodeShatter(0.3f, expoisionPos, 0.5f);
        Destroy(gameObject);
    }
}
