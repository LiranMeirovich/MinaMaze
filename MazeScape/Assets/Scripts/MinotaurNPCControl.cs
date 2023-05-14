using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MinotaurNPCControl : MonoBehaviour
{

    public Animator animator;
    public GameObject target;
    public int hits = 0;
    private float waitTime = 4.0f;
    private float stunTime = 10.0f;
    public float timer = 0;
    public bool followNPC = false;
    public bool attacking = false;
    public bool stunnedbomb = false;
    public bool stunnedArrow = false;
    public GameObject cs;
    public Transform[,] stairs;
    public Vector3 Dest;
    public NPCController npc;

    public NavMeshAgent agent;
    public bool hit_p = false;
    public PlayerControl player;

    // Flag to indicate whether to follow the player
    private bool followPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        running(new Vector3(1, transform.position.y, 1));

    }

    // Update is called once per frame
    void Update()
    {
        if (!stunnedArrow && !stunnedbomb)
        {
            if (!agent.isActiveAndEnabled)
                agent.enabled = true;
            //Debug.Log(agent.remainingDistance);
            Vector3[] dirs = new Vector3[] { cs.transform.forward, cs.transform.forward - transform.right, cs.transform.forward + transform.right };
            RaycastHit hit;
            followNPC = false;
            for (int i = 0; i < 3; i++)
            {
                if (Physics.Raycast(cs.transform.position, dirs[i], out hit, Mathf.Infinity))
                {

                    if (hit.collider.gameObject.tag == "NPC")
                    {
                        Debug.Log("NEAR NPC");
                        followNPC = true;
                        if (Vector3.Distance(hit.collider.gameObject.transform.position, transform.position) < 1f)
                        {
                            hit_p = true;
                            npc.minHit();
                            stunnedArrow = true;
                            animator.Play("Attack");
                            Debug.Log("Attacking");
                        }
                        else
                        {
                            //Debug.Log(hit.collider.transform.position);
                            running(npc.transform.position);
                            Debug.Log("SeeingNPC");
                        }
                    }
                    else
                        Debug.Log(hit.collider.name);
                }
            }
            followPlayer = false;
            for (int i = 0; i < 3; i++)
            {
                if (Physics.Raycast(cs.transform.position, dirs[i], out hit, Mathf.Infinity))
                {
                    
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        followPlayer = true;
                        if (Vector3.Distance(hit.collider.gameObject.transform.position, transform.position) < 1f)
                        {
                            animator.Play("Attack");
                            hit_p = true;
                            stunnedArrow = true;
                            player.minHit();
                        }
                        else
                        {
                            //Debug.Log(hit.collider.transform.position);
                            running(player.transform.position);
                        }
                    }
                    else
                        Debug.Log(hit.collider.name);
                }
            }
            if (!followPlayer && !followNPC&&agent.remainingDistance == 0f)
            {
                Random.seed = System.DateTime.Now.Millisecond;
                int rndx = Random.Range(-9, 8);
                Random.seed = System.DateTime.Now.Millisecond;
                int rndz = Random.Range(-9, 8);
                running(new Vector3(rndx, transform.position.y, rndz));
                //Debug.Log(rndx+","+ rndz);
            }
            else
            {
                Debug.Log(followPlayer);
                //Debug.Log(agent.remainingDistance);
            }
        }
        else
        {
            gameObject.GetComponent<AudioSource>().enabled = false;
            if (agent.isActiveAndEnabled)
                agent.enabled = false;
            if ((timer > stunTime && stunnedArrow) || (timer > waitTime && stunnedbomb))
            {
                timer = 0;
                stunnedArrow = false;
                stunnedbomb = false;
                hit_p = false;
            }
            else
            {
                if (stunnedArrow || stunnedbomb)
                {
                    timer += Time.deltaTime;
                    if (!hit_p)
                        animator.Play("Idle");
                }

            }
        }
    }

    void running(Vector3 position)
    {
        if(Mathf.Abs(gameObject.transform.position.y-player.transform.position.y)<0.5f)
            gameObject.GetComponent<AudioSource>().enabled = true;
        else
            gameObject.GetComponent<AudioSource>().enabled = false;
        agent.SetDestination(position);
        Debug.Log("WALKING!");
        animator.Play("Run");
    }
    public void command(Vector3 position)
    {
        followPlayer = false;
        running(position);
    }
    public void hit_arrow()
    {
        stunnedArrow = true ;
        Debug.Log("Arrow Hit");
    }
    public void explodeNPC()
    {
        stunnedbomb = true;
    }
}