using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    GameObject missilePrefab;  // prefab for the missle

    [SerializeField]
    private string robotType;   // type of robot (R, B, Y robot)

    public int health;  // how much damage this robot can take before dying
    public int range;   // distance the gun can fire
    public float fireRate;  // how fast it can fire

    public Transform missileFireSpot;   // coordinate on the robot model from which the missiles fire
    UnityEngine.AI.NavMeshAgent agent;  // reference to the NavMesh Agent component

    private Transform player;   // what the rohot should track
    private float timeLastFired;

    private bool isDead;        // tracks whether the robot is alive or dead

    public Animator robot;

    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip fireSound;
    [SerializeField]
    private AudioClip weakHitSound;

    /*
     * Start()¿Í Update() ¼³¸í
     * 1. By default, all robots are alive. You then set the agent and player values to the
     * NavMesh Agent and Player components respectively.
     * 2. Check if the robot is dead before continuing. There are no zombie robots in this game!
     * 3. Make the robot face the player.
     * 4. Tell the robot to use the NavMesh to find the player.
     * 5. Check to see if the robot is within firing range and there¡¯s been enough time 
     *     between shots to fire again.
     * 6. Update timeLastFired to the current time and call Fire(), 
     *     which simply logs a message to the console for the time being
     */

    // Start is called before the first frame update
    void Start()
    {
        // 1
        isDead = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // 2
        if (isDead)
        {
            return;
        }
        // 3
        transform.LookAt(player);
        // 4
        agent.SetDestination(player.position);
        // 5
        if (Vector3.Distance(transform.position, player.position) < range && Time.time - timeLastFired > fireRate)
        {
            // 6
            timeLastFired = Time.time;
            fire();
        }
    }

    private void fire()
    {
        GameObject missile = Instantiate(missilePrefab);
        missile.transform.position = missileFireSpot.transform.position;
        missile.transform.rotation = missileFireSpot.transform.rotation;

        Debug.Log("Fire");
        robot.Play("Fire");

        GetComponent<AudioSource>().PlayOneShot(fireSound);
    }

    /*
1. This has roughly the same logic as the player TakeDamage() method, 
    except when health hits 0 it plays a death animation before calling DestroyRobot().
2. This adds a delay before destroying the robot. This lets the Die animation finish.
     * */

    // 1
    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }

        health -= amount;

        if (health <= 0)
        {
            isDead = true;
            robot.Play("Die");
            StartCoroutine("DestroyRobot");

            // adding UI (p.311)
            Game.RemoveEnemy();

            GetComponent<AudioSource>().PlayOneShot(deathSound);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(weakHitSound);
        }
    }

    // 2
    IEnumerator DestroyRobot()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
