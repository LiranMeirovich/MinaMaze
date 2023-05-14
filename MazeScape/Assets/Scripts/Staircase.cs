using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Staircase : MonoBehaviour
{
    Animator anmtor;
    [SerializeField] GameObject[] parts;
    public GameObject PlayerEye;
    private float waitTime = 4.0f;
    private float timer = 0.0f;
    private bool waiting = false;
    public NPCController npc;
    public PlayerControl player;
    public int number;
    public Vector3Int loc;
    private TMP_Text openStaircase;
    // Start is called before the first frame update
    void Start()
    {
        anmtor = GetComponent<Animator>();
        openStaircase = GameObject.FindWithTag("OpenStairs").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > waitTime && waiting)
        {
            timer = 0;
            waiting = false;
        }
        else
        {
            if (waiting)
                timer += Time.deltaTime;
        }
            // if the player focus is on drawer then change the crosshair
        RaycastHit hit;
        Debug.DrawRay(PlayerEye.transform.position, PlayerEye.transform.forward, Color.green);
        Debug.DrawRay(player.transform.position - new Vector3(0, 0.178f, 0), player.transform.forward, Color.green);
        Debug.DrawRay(player.transform.position - new Vector3(0, 0.178f, 0), -player.transform.forward, Color.blue);
        if (Physics.Raycast(PlayerEye.transform.position, PlayerEye.transform.forward, out hit, 500f))
        {
            //Debug.Log("hit something???");
            bool h = false;
            for(int i = 0; i<parts.Length;i++)
            {
                if (hit.collider.gameObject == parts[i])
                    h = true;
            }
            if (h && Vector3.Distance(gameObject.transform.position, player.transform.position) < 1f)
            {
                if (!player.gotKey(number))
                    openStaircase.text = "Need Key!";
                else
                {
                    if (!anmtor.GetBool("Open"))
                        openStaircase.text = "Press E to open staircase";
                    else
                        openStaircase.text = "Press E to close staircase";
                }
                
                //Debug.Log("HIT!"+number);
                if (Input.GetKey(KeyCode.E) && !anmtor.GetBool("Open")&&player.gotKey(number)&&!waiting)
                {
                    //openStaircase.text = "";
                    anmtor.SetBool("Open", true);
                    Debug.Log("opening!");
                    waiting = true;
                }
                else
                {
                    //Debug.Log(player.gotKey(number));
                    //Debug.Log(anmtor.GetBool("Open"));


                }
                if (Input.GetKey(KeyCode.E) && anmtor.GetBool("Open") && player.gotKey(number)&&!waiting)
                {
                    //openStaircase.text = "";
                    anmtor.SetBool("Open", false);
                    Debug.Log("Closing!!");
                    waiting = true;
                    // Check if we have reached beyond 2 seconds.
                    // Subtracting two is more accurate over time than resetting to zero.
                    
                }
            }
            else
            {
                if (Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) < 0.7f && Vector3.Distance(gameObject.transform.position, player.transform.position)<1.3f)
                {
                    openStaircase.text = "";
                    //Debug.Log("AAAAA"+number);
                }
                //Debug.Log(hit.collider.gameObject.name);
            }
        }
        RaycastHit hit1;
        RaycastHit hit2;
        RaycastHit hit3;
        RaycastHit hit4;
        if (Physics.Raycast(player.transform.position - new Vector3(0, 0.178f, 0), player.transform.forward, out hit1))
        {
            bool h = false;
            for (int i = 0; i < parts.Length; i++)
            {
                if (hit1.collider.gameObject == parts[i])
                {
                    Debug.Log("Adding to known!");
                    h = true;
                }
                else
                    Debug.Log(hit1.collider.name);
            }
            //Debug.Log("hit something???");
            if(h)
                player.updateMap(loc);
        }
        if (Physics.Raycast(player.transform.position - new Vector3(0, 0.178f, 0), -player.transform.forward, out hit2))
        {
            bool h = false;
            for (int i = 0; i < parts.Length; i++)
            {
                if (hit2.collider.gameObject == parts[i])
                    h = true;
            }
            //Debug.Log("hit something???");
            if (h)
                player.updateMap(loc);
        }
        if (Physics.Raycast(npc.transform.position - new Vector3(0, 0.178f, 0), player.transform.forward, out hit3))
        {
            bool h = false;
            for (int i = 0; i < parts.Length; i++)
            {
                if (hit3.collider.gameObject == parts[i])
                    h = true;
            }
            //Debug.Log("hit something???");
            if (h)
                player.updateMap(loc);
        }
        Debug.DrawRay(npc.transform.position, PlayerEye.transform.forward, Color.green);
        if (Physics.Raycast(npc.transform.position - new Vector3(0, 0.178f, 0), -player.transform.forward, out hit4))
        {
            bool h = false;
            for (int i = 0; i < parts.Length; i++)
            {
                if (hit4.collider.gameObject == parts[i])
                    h = true;
            }
            //Debug.Log("hit something???");
            if (h)
                player.updateMap(loc);
        }
    }
    IEnumerator sleepForBit()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(10);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
    public void setPlayer(GameObject oPlayerEye, PlayerControl oplayer, NPCController onpc)
    {
        npc = onpc;
        PlayerEye = oPlayerEye;
        player = oplayer;
    }
    public void setNum(int num)
    {
        number = num;
    }
    public void setLoc(Vector3Int location)
    {
        loc = location;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(number==2&& player.gotKey(number)&&other.tag=="Player"&& anmtor.GetBool("Open"))
            SceneManager.LoadScene("WinRoom");
    }
}
