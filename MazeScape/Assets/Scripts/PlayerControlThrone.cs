using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlThrone : MonoBehaviour
{

    public float jumpForce;

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
    private bool mapOpen = false;
    public int current_floor = 0;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

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
        keys[k] = true;
    }
    public bool gotKey(int k)
    {
        return keys[k];
    }
   

}
