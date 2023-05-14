using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{
    public float maxDistance;
    public bool inSight;
    public bool inDistance;

    public GameObject objectToThrow;
    public TMP_Text pressToShoot;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        inSight = false;
        inDistance = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;


        inSight = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward * 100, out hit, Mathf.Infinity)
            && hit.collider.gameObject == this.gameObject;
            

        inDistance = Vector3.Distance(this.gameObject.transform.position, player.position) < maxDistance;

        if (inDistance && inSight)
            pressToShoot.text = "Press C to shoot cannon";
        else
            pressToShoot.text = "";

        if (Input.GetKeyDown(KeyCode.C) && inDistance && inSight)
        {
            shootCannon();
        }

        
    }

    public void shootCannon()
    {
        GameObject projectile = null;
        Vector3 shootingPoint = gameObject.transform.position;
        shootingPoint.z += -0.715f;
        shootingPoint.y += 0.2f;

        projectile = Instantiate(objectToThrow, shootingPoint, transform.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = this.transform.forward;

        RaycastHit hit;

        

        if (Physics.Raycast(shootingPoint, gameObject.transform.forward, out hit, 500f))
        {
            forceDirection = (hit.point - shootingPoint).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * 2 + transform.up*10;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
    }
}
