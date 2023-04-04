using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    Sprite redReticle;
    [SerializeField]
    Sprite yellowReticle;
    [SerializeField]
    Sprite blueReticle;
    [SerializeField]
    Image reticle;

    // coding the UI
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text armorText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text pickupText;
    [SerializeField]
    private Text waveText;
    [SerializeField]
    private Text enemyText;
    [SerializeField]
    private Text waveClearText;
    [SerializeField]
    private Text newWaveText;
    [SerializeField]
    Player player;

    /*
1. Initializes the player health and ammo text.
2. This and the rest of the methods are simply setters that set the related text values.
     * */
    // Start is called before the first frame update
    // 1
    void Start()
    {
        SetArmorText(player.armor);
        SetHealthText(player.health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateReticle()
    {
        switch (GunEquipper.activeWeaponType)
        {
            case Constants.Pistol:
                reticle.sprite = redReticle;
                break;
            case Constants.Shotgun:
                reticle.sprite = yellowReticle;
                break;
            case Constants.AssaultRifle:
                reticle.sprite = blueReticle;
                break;
            default:
                return;
        }
    }

    // ch11. coding the UI (P.304)
    // 2
    public void SetArmorText(int armor)
    {
        armorText.text = "Armor: " + armor;
    }
    public void SetHealthText(int health)
    {
        healthText.text = "Health: " + health;
    }
    public void SetAmmoText(int ammo)
    {
        ammoText.text = "Ammo: " + ammo;
    }
    public void SetScoreText(int score)
    {
        scoreText.text = "" + score;
    }
    public void SetWaveText(int time)
    {
        waveText.text = "Next Wave: " + time;
    }
    public void SetEnemyText(int enemies)
    {
        enemyText.text = "Enemies: " + enemies;
    }

    /*
1. Show the wave clear bonus text by setting its enabled state to true, 
    then immediately call a coroutine that will hide the text again. 
    You do it this way because you can use the coroutine to pause itself 
    before it actually hides the text.
2. Wait for 4 seconds before setting the enabled state to false - therefore hiding the text.
3. Enable and set the text for the pickup alert and restart the hidePickup() oroutine. 
    This lets the player to pick up two or more pickups in quick succession, 
    without the second pickup¡¯s text label displaying 
    before the first pickup¡¯s text times out.
4. Wait for 4 seconds before removing the pickup text.
5. Show the new wave text.
6. Wait for 4 seconds before removing the new wave text.
     * */

    // coding the UI (p.305)
    // 1
    public void ShowWaveClearBonus()
    {
        waveClearText.GetComponent<Text>().enabled = true;
        StartCoroutine("hideWaveClearBonus");
    }
    // 2
    IEnumerator hideWaveClearBonus()
    {
        yield return new WaitForSeconds(4);
        waveClearText.GetComponent<Text>().enabled = false;
    }
    // 3
    public void SetPickUpText(string text)
    {
        pickupText.GetComponent<Text>().enabled = true;
        pickupText.text = text;
        // Restart the Coroutine so it doesn¡¯t end early
        StopCoroutine("hidePickupText");
        StartCoroutine("hidePickupText");
    }
    // 4
    IEnumerator hidePickupText()
    {
        yield return new WaitForSeconds(4);
        pickupText.GetComponent<Text>().enabled = false;
    }
    // 5
    public void ShowNewWaveText()
    {
        StartCoroutine("hideNewWaveText");
        newWaveText.GetComponent<Text>().enabled = true;
    }
    // 6
    IEnumerator hideNewWaveText()
    {
        yield return new WaitForSeconds(4);
        newWaveText.GetComponent<Text>().enabled = false;
    }
}
