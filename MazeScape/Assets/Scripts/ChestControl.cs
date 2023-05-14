using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestControl : MonoBehaviour
{
    public textControl tc;
    public Animator anmtor;
    [SerializeField] GameObject[] parts;
    public GameObject PlayerEye;
    public PlayerControl player;
    bool opened = false;
    bool looked = false;
    public bool near = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(PlayerEye.transform.position, PlayerEye.transform.forward, out hit))
        {
            //Debug.Log("hit something???");
            bool h = false;
            for (int i = 0; i < parts.Length; i++)
            {
                if (hit.collider.gameObject == parts[i])
                    h = true;
            }
            if (h&&near)
            {
                looked = true;
                //Debug.Log("HIT!");
                if (Input.GetKey(KeyCode.Space))
                {

                    anmtor.SetBool("Open", true);
                    Debug.Log("opening!");
                    opened = true;
                    tc.clean();
                    looked = false;
                }
                else
                {
                    if (!opened)
                        tc.setText("press SPACE to open");
                }
            }
            else
            {
                if (looked)
                {
                    looked = false;
                    tc.clean();
                }
            }
        }
    }
    public void updateNear(bool con)
    {
        near = con;
    }
}
