using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject PlayerEye;
    public PlayerControl player;
    public Vector3Int loc;
    public int number;
    public TMP_Text pickupKey;
    // Start is called before the first frame update
    void Start()
    {
        pickupKey = GameObject.FindWithTag("PickupTxt").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the player focus is on drawer then change the crosshair
        RaycastHit hit;
        Debug.DrawRay(PlayerEye.transform.position, PlayerEye.transform.forward, Color.green);
        if (Physics.Raycast(PlayerEye.transform.position,PlayerEye.transform.forward, out hit))
        {
            //Debug.Log("hit something???");
            if (hit.collider.gameObject == this.gameObject)
            {
                pickupKey.text = "Press E to pickup key";
                //Debug.Log("HIT!");
                if (Input.GetKey(KeyCode.E))
                {
                    pickupKey.text = "";
                    gameObject.SetActive(false);
                    player.getKey(number);
                }
            }
            else
            {
                if(Mathf.Abs(gameObject.transform.position.y-player.transform.position.y)<0.5f)
                    pickupKey.text = "";
                //Debug.Log(hit.collider.gameObject.name);
            }
        }
        RaycastHit hit1;
        RaycastHit hit2;
        if (Physics.Raycast(player.transform.position - new Vector3(0, 0.178f, 0), player.transform.forward, out hit1))
        {
            //Debug.Log("hit something???");
            if (hit1.collider.gameObject == this.gameObject)
            {
                player.updateMap(loc);
            }
        }
        if (Physics.Raycast(player.transform.position - new Vector3(0, 0.178f, 0), -player.transform.forward, out hit2))
        {
            //Debug.Log("hit something???");
            if (hit2.collider.gameObject == this.gameObject)
            {
                player.updateMap(loc);
            }
        }
    }
    public void setNum(int num)
    {
        number = num;
    }
    public void setPlayer(GameObject oPlayerEye, PlayerControl oplayer)
    {
        PlayerEye = oPlayerEye;
        player = oplayer;
    }
    public void setLoc(Vector3Int location)
    {
        loc = location;
    }
}
