using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject[] robots;  // array of robot GameObjects to instantiate

    private int timesSpawned;  // tracks the spawn cycle of the robots
    private int healthBonus = 0;  //how much health each robot gains each wave 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRobot()
    {
        timesSpawned++;
        healthBonus += 1 * timesSpawned;
        GameObject robot = Instantiate(robots[Random.Range(0, robots.Length)]);
        robot.transform.position = transform.position;
        robot.GetComponent<Robot>().health += healthBonus;
    }
}
