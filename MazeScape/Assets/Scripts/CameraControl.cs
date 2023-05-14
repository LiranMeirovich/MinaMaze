using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
