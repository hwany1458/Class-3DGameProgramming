using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawn : MonoBehaviour
{

	[SerializeField]
	private GameObject[] pickups;

	// Start is called before the first frame update
	// 3
	void Start()
    {
		spawnPickup();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	/*
1. Instantiates a random pickup and sets its position to that of the PickupSpawn GameObject, 
	which you will make in a moment.
2. Waits 20 seconds before calling spawnPickup().
3. Spawns a pickup as soon as the game beings.
4. Starts the Coroutine to respawn when the player has picked up.
	 */
	// 1
	void spawnPickup()
	{
		// Instantiate a random pickup
		GameObject pickup = Instantiate(pickups[Random.Range(0, pickups.Length)]);
		pickup.transform.position = transform.position;
		pickup.transform.parent = transform;
	}

	// 2
	IEnumerator respawnPickup()
	{
		yield return new WaitForSeconds(20);
		spawnPickup();
	}

	// 4
	public void PickupWasPickedUp()
	{
		StartCoroutine("respawnPickup");
	}
}
