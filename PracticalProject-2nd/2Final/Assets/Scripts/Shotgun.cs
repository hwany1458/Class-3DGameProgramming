using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Pistol : MonoBehaviour
public class Shotgun : Gun
{
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

    override protected void Update()
    {
        base.Update();
        // Shotgun & Pistol have semi-auto fire rate
        if (Input.GetMouseButtonDown(0) && (Time.time - lastFireTime) > fireRate)
        {
            lastFireTime = Time.time;
            Fire();
        }
    }
}
