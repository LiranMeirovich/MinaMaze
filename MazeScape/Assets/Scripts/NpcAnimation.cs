using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcAnimation : MonoBehaviour
{
   //private NavMeshAgent agent;
    private Animator animator;
    public GameObject target;
    public int hits = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
       // agent = GetComponent<NavMeshAgent>();
       //agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (agent.enabled)
        //    agent.SetDestination(target.transform.position);
                if(Input.GetKeyDown(KeyCode.Z))
                {
                    animator.SetInteger("NPCState", 1);
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                   
                    animator.SetInteger("NPCState", 0);
                }
                //else if (Input.GetKeyDown(KeyCode.C))
                //{
                //    animator.SetInteger("KnightState", 2);
                //}
        
    }
    //public IEnumerator GetHit()
    //{
    //    agent.enabled = false;
    //    hits++;
    //    animator.SetInteger("KnightState", 2); // fall

    //    yield return new WaitForSeconds(2f);
    //    if (hits < 2) // the enemy gets up
    //    {
    //        animator.SetInteger("KnightState", 3); // get up
    //        // after a short delay
    //        yield return new WaitForSeconds(7.5f);
    //        agent.enabled = true;
    //        animator.SetInteger("KnightState", 1); // walk

    //    }

    //}


}