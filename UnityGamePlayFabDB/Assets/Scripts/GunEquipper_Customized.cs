using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEquipper_Customized : MonoBehaviour
{
    public static string activeWeaponType;

    public GameObject pistol;
    public GameObject assaultRifle;
    public GameObject shotgun;

    GameObject activeGun;

    [SerializeField]
    //GameUI gameUI;
    GameObject gameUI;

    // adding UI (p.307)
    [SerializeField]
    Ammo ammo;

    // Start is called before the first frame update
    void Start()
    {
        activeWeaponType = Constants.Pistol;
        activeGun = pistol;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            loadWeapon(pistol);
            activeWeaponType = Constants.Pistol;

            //gameUI.UpdateReticle();
        }
        else if (Input.GetKeyDown("2"))
        {
            loadWeapon(assaultRifle);
            activeWeaponType = Constants.AssaultRifle;

            //gameUI.UpdateReticle();
        }
        else if (Input.GetKeyDown("3"))
        {
            loadWeapon(shotgun);
            activeWeaponType = Constants.Shotgun;

            //gameUI.UpdateReticle();
        }
    }

    // To load the appropriate gun when the player switches weapons
    private void loadWeapon(GameObject weapon)
    {
        pistol.SetActive(false);
        assaultRifle.SetActive(false);
        shotgun.SetActive(false);

        weapon.SetActive(true);
        activeGun = weapon;

        // adding UI (p.308)
        //gameUI.SetAmmoText(ammo.GetAmmo(activeGun.tag));
    }

    public GameObject GetActiveWeapon()
    {
        return activeGun;
    }
}
