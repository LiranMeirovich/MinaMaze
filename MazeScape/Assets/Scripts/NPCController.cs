using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{

    public Animator animator;
    public GameObject target;
    public int hits = 0;
    public float deadTime = 10f;
    public float timer = 0;
    public int health = 25;
    public TMP_Text respawnMessage;
    public bool dead = false;
    public NavMeshAgent agent;

    public Transform player;

    // Flag to indicate whether to follow the player
    private bool followPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            respawnMessage.text = "Remmy died!";
            dead = true;
            health = 25;
            agent.Warp(new Vector3(-3.7f, -4.1f, 16));
        }
        else
        {
            if (dead)
            {
                if (timer > deadTime)
                {
                    timer = 0;
                    dead = false;
                    agent.Warp(player.transform.position);
                    respawnMessage.text = "Remmy respawned!";
                }
                else
                    timer += Time.deltaTime;
            }
            else
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                if (agent.remainingDistance == 0)
                {
                    animator.SetInteger("NPCState", 0);
                    Debug.Log("Set to standing");
                }
                else
                {
                    animator.SetInteger("NPCState", 1);
                }
                //Debug.Log(agent.remainingDistance);
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Vector3 camPos = Camera.main.transform.forward * 100;
                    Ray ray = Camera.main.ScreenPointToRay(camPos);
                    RaycastHit hit;

                    if (Physics.Raycast(Camera.main.transform.position, camPos, out hit, Mathf.Infinity))
                    {
                        running(hit.point);
                        followPlayer = false;




                    }
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    followPlayer = true;
                }

                if (followPlayer)
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, player.position);


                    if (distanceToPlayer > 2f)
                        running(player.position);
                    else
                        agent.ResetPath();



                }
            }
        }
    }

    void running(Vector3 position)
    {
        respawnMessage.text = "";
        agent.SetDestination(position);
        animator.SetInteger("NPCState", 1);
        Debug.Log("Set to walking");
    }
    public void command(Vector3 position)
    {
        followPlayer = false;
        running(position);
    }
    public void minHit()
    {
        health -= 25;
    }
}