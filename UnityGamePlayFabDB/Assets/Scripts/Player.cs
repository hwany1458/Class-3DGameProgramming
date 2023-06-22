using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;  // remaining life of the player
    public int armor;  // special health layer for the player 
    public GameUI gameUI;

    private GunEquipper gunEquipper;
    private Ammo ammo;  // reference to the Ammo class you created earlier to track weapon ammo

    // adding game over (p.320)
    public Game game;
    public AudioClip playerDead;

    // Start is called before the first frame update
    void Start()
    {
        ammo = GetComponent<Ammo>();
        gunEquipper = GetComponent<GunEquipper>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        int healthDamage = amount;

        if (armor > 0)
        {
            int effectiveArmor = armor * 2;
            effectiveArmor -= healthDamage;

            // If there is still armor, don't need to process health damage
            if (effectiveArmor > 0)
            {
                armor = effectiveArmor / 2;
                // adding UI (p.306)
                gameUI.SetArmorText(armor);
                return;
            }

            armor = 0;
            // adding UI (p.306)
            gameUI.SetArmorText(armor);
        }

        health -= healthDamage;
        // adding UI (p.306) -- replace it
        Debug.Log("Health is " + health);
        gameUI.SetHealthText(health);

        if (health <= 0)
        {
            // replace 
            Debug.Log("GameOver");
            
            // adding game over (p.320)
            GetComponent<AudioSource>().PlayOneShot(playerDead);
            game.GameOver();
        }
    }

    /*
1. This adds to the players health and armor respectively.
2. This adds ammunition for that gun type.
     * */
    // 1
    private void pickupHealth()
    {
        health += 50;
        if (health > 200)
        {
            health = 200;
        }

        // adding UI (p.306)
        gameUI.SetPickUpText("Health picked up + 50 Health");
        gameUI.SetHealthText(health);
    }

    private void pickupArmor()
    {
        armor += 15;

        // adding UI (p.307)
        gameUI.SetPickUpText("Armor picked up + 15 armor");
        gameUI.SetArmorText(armor);
    }

    // 2
    private void pickupAssaultRifleAmmo()
    {
        ammo.AddAmmo(Constants.AssaultRifle, 50);

        // adding UI (p.307)
        gameUI.SetPickUpText("Assault rifle ammo picked up + 50 ammo");
        if (gunEquipper.GetActiveWeapon().tag == Constants.AssaultRifle)
        {
            gameUI.SetAmmoText(ammo.GetAmmo(Constants.AssaultRifle));
        }
    }

    private void pickupPisolAmmo()
    {
        ammo.AddAmmo(Constants.Pistol, 20);

        // adding UI (p.307)
        gameUI.SetPickUpText("Pistol ammo picked up + 20 ammo");
        if (gunEquipper.GetActiveWeapon().tag == Constants.Pistol)
        {
            gameUI.SetAmmoText(ammo.GetAmmo(Constants.Pistol));
        }
    }

    private void pickupShotgunAmmo()
    {
        ammo.AddAmmo(Constants.Shotgun, 10);

        // adding UI (p.307)
        gameUI.SetPickUpText("Shotgun ammo picked up + 10 ammo");
        if (gunEquipper.GetActiveWeapon().tag == Constants.Shotgun)
        {
            gameUI.SetAmmoText(ammo.GetAmmo(Constants.Shotgun));
        }
    }

    // takes an int that represents the type of item being picked up
    public void PickUpItem(int pickupType)
    {
        switch (pickupType)
        {
            case Constants.PickUpArmor:
                pickupArmor();
                break;
            case Constants.PickUpHealth:
                pickupHealth();
                break;
            case Constants.PickUpAssaultRifleAmmo:
                pickupAssaultRifleAmmo();
                break;
            case Constants.PickUpPistolAmmo:
                pickupPisolAmmo();
                break;
            case Constants.PickUpShotgunAmmo:
                pickupShotgunAmmo();
                break;
            default:
                Debug.LogError("Bad pickup type passed" + pickupType);
                break;
        }
    }
}