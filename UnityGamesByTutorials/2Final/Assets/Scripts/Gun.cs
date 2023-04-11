using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate;
    protected float lastFireTime;

    public Ammo ammo;
    public AudioClip liveFire;
    public AudioClip dryFire;

    public float zoomFactor;  // controls the zoom level
    public int range;  // how far the gune can effectively hit its target
    public int damage;  // how much damage the gun causes

    private float zoomFOV;  // the field of view based on the zoom factor
    private float zoomSpeed = 6;

    // Start is called before the first frame update
    void Start()
    {
        zoomFOV = Constants.CameraDefaultZoom / zoomFactor;

        lastFireTime = Time.time - 10;
    }

    // Update is called once per frame
    //void Update()
    protected virtual void Update()
    {
        // Right Click (Zoom)
        if (Input.GetMouseButton(1))
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomFOV, zoomSpeed * Time.deltaTime);
        }
        else
        {
            Camera.main.fieldOfView = Constants.CameraDefaultZoom;
        }
    }

    protected void Fire()
    {
        if (ammo.HasAmmo(tag))
        {
            GetComponent<AudioSource>().PlayOneShot(liveFire);
            ammo.ConsumeAmmo(tag);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(dryFire);
        }

        GetComponent<Animator>().Play("Fire");

        // to implement the raycasting
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            processHit(hit.collider.gameObject);
        }
    }

    private void processHit(GameObject hitObject)
    {
        if (hitObject.GetComponent<Player>() != null)
        {
            hitObject.GetComponent<Player>().TakeDamage(damage);
        }

        if (hitObject.GetComponent<Robot>() != null)
        {
            hitObject.GetComponent<Robot>().TakeDamage(damage);
        }
    }
}
