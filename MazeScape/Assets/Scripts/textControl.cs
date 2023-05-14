using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textControl : MonoBehaviour
{
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
    }

    // Update is called once per frame
    public void clean()
    {
        text.text = "";
    }
    public void setText(string incoming)
    {
        text.text = incoming;
    }
}
