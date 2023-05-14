using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject[] objectToThrow;

    [Header("Settings")]
    public int[] totalAmmo;
    public float throwCooldown;
    public float jumpForce;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float[] throwForce;
    public float[] throwUpwardForce;
    public GameObject[] throwables;
    bool readyToThrow;
    CharacterController controller;
    float speed = 3;
    float angularSpeed = 100;
    [Header("Items")]
    public bool[] keys = new bool[3];
    [Header("Camera")]
    public GameObject aCamera; // must be connected to real camera in Unity
    // Start is called before the first frame update
    private int currentItem = 0;
    private bool jmp = false;
    private float yf = -0.006f;
    int key_c = 0;
    private bool mapOpen = false;
    public int current_floor = 0;
    public TMP_Text health_txt;
    public TMP_Text ammo_txt;
    public TMP_Text keys_text;
    public MapController mcv;
    public NPCController ally;
    public int health = 100;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        keys[0] = false;
        keys[1] = false;
        keys[2] = false;
        readyToThrow = true;
        throwables[0] = GameObject.FindGameObjectsWithTag("BOMB")[0];
        throwables[1] = GameObject.FindGameObjectsWithTag("BOW")[0];
        throwables[2] = GameObject.FindGameObjectsWithTag("CANDLE")[0];
        throwables[0].SetActive(true);
        throwables[1].SetActive(false);
        throwables[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0.72f)
        {
            if (current_floor != 0)
            {
                current_floor = 0;
                ally.agent.Warp(transform.position);
            }
        }
        if (transform.position.y > 0.72f && transform.position.y < 1.69f)
        {
            if (current_floor != 1)
            {
                current_floor = 1;
                ally.agent.Warp(transform.position);
            }
        }
        if (transform.position.y > 1.68f)
        {
            if (current_floor != 2)
            {
                current_floor = 2;
                ally.agent.Warp(transform.position);
            }
        }
        Debug.DrawRay(transform.position-new Vector3(0, 0.178f, 0),transform.forward, Color.green);
        Debug.DrawRay(transform.position - new Vector3(0, 0.178f, 0), -transform.forward, Color.blue);
        if (Input.GetKeyDown(KeyCode.Space) && GetComponent<CharacterController>().isGrounded)
        {
            jmp = true;
            yf = jumpForce;
            Debug.Log("JUMP!");
        }
        else
        {
            if (GetComponent<CharacterController>().isGrounded)
            {
                jmp = false;
                yf = -0.006f;
                Debug.Log("Ground!");
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentItem = 0;
            throwables[1].SetActive(false);
            throwables[2].SetActive(false);
            ammo_txt.text = "Ammo : "+totalAmmo[currentItem];
            if (totalAmmo[currentItem] > 0)
                throwables[0].SetActive(true);
            else
                throwables[0].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            
            currentItem = 1;
            ammo_txt.text = "Ammo : " + totalAmmo[currentItem];
            throwables[0].SetActive(false);
            throwables[1].SetActive(true);
            throwables[2].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            
            currentItem = 2;
            ammo_txt.text = "Ammo : " + totalAmmo[currentItem];
            throwables[0].SetActive(false);
            throwables[1].SetActive(false);
            if (totalAmmo[currentItem] > 0)
                throwables[2].SetActive(true);
            else
                throwables[2].SetActive(false);
        }

        if (Input.GetKeyDown(throwKey) && readyToThrow && totalAmmo[currentItem] > 0&&!mapOpen)
        {
            Throw();
        }
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().enabled = false;
        }
        float dx, dz;
        float rotationAboutY;
        float rotationAboutX;

        // rotate a camera about X-Axis
        if (!mapOpen)
        {
            rotationAboutX = -1 * angularSpeed * Time.deltaTime * Input.GetAxis("Mouse Y");
            aCamera.transform.Rotate(rotationAboutX, 0, 0);

            // rotate player about Y-Axis
            rotationAboutY = angularSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
            transform.Rotate(0, rotationAboutY, 0);
        }


        dz = speed * Time.deltaTime * Input.GetAxis("Vertical"); // can be -1, 0 or 1
        dx = speed * Time.deltaTime * Input.GetAxis("Horizontal");
        // simple motion forward
        //        this.transform.Translate(new Vector3(0,0,0.06f));
        if (jmp)
            yf -= 0.006f;
        Vector3 motion = new Vector3(dx, 0, dz);
        motion = transform.TransformDirection(motion); // transform to local coordinates

        controller.Move(motion+new Vector3(0,yf,0)); // in global coordinates
    }
    public void getKey(int k)
    {
        key_c += 1;
        keys_text.text = "Keys : " + key_c;
        keys[k] = true;
    }
    public bool gotKey(int k)
    {
        return keys[k];
    }
    private void Throw()
    {
        if (totalAmmo[currentItem] == 1 && currentItem != 1)
            throwables[currentItem].SetActive(false);
        readyToThrow = false;
        Debug.Log("Test?");
        // instantiate object to throw
        GameObject projectile = null;
        if (currentItem==2)
            projectile = Instantiate(objectToThrow[currentItem], transform.position, Quaternion.identity);
        else
            projectile = Instantiate(objectToThrow[currentItem], attackPoint.position, transform.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce[currentItem] + transform.up * throwUpwardForce[currentItem];

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalAmmo[currentItem]--;
        ammo_txt.text = "Ammo : " + totalAmmo[currentItem];
        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
    public void mapControl(bool isopen)
    {
        mapOpen = isopen;
    }
    public void updateMap(Vector3Int tu)
    {
        Debug.Log(tu);
        mcv.updateKnown(tu);
    }
    public void minHit()
    {
        health -= 25;
        health_txt.text = "HP : " + health;
    }

    
}
