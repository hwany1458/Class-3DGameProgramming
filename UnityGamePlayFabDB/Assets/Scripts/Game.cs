using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// adding UI (p.318)
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static Game singleton;
    
    [SerializeField]
    RobotSpawn[] spawns;  //array of teleporters that spawn robots each wave
    
    public int enemiesLeft;  // counter which tracks how many robots are still alive

    // chapter 11. coding the UI (p.308)
    public GameUI gameUI;
    public GameObject player;
    public int score;
    public int waveCountdown;
    public bool isGameOver;

    // game over (p.318)
    public GameObject gameOverPanel;

    /*
1. Initialize the singleton and call SpawnRobots().
2. Go through each RobotSpawn in the array and call SpawnRobot() to actually spawn a robot. 
    You will see a warning that the singleton is 'initialized but not used'. 
    Ignore this for now; it will be used shortly in the next chapter.
     */

    // Start is called before the first frame update
    // 1
    void Start()
    {
        singleton = this;

        // coding the UI (p.310)
        StartCoroutine("increaseScoreEachSecond");
        isGameOver = false;
        Time.timeScale = 1;
        waveCountdown = 30;
        enemiesLeft = 0;
        StartCoroutine("updateWaveTimer");

        SpawnRobots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 2
    private void SpawnRobots()
    {
        foreach (RobotSpawn spawn in spawns)
        {
            spawn.SpawnRobot();
            enemiesLeft++;
        }

        // adding UI (p.310)
        gameUI.SetEnemyText(enemiesLeft);
    }

    // adding UI (p.309)
    private IEnumerator updateWaveTimer()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1f);
            waveCountdown--;
            gameUI.SetWaveText(waveCountdown);
            // Spawn next wave and restart count down
            if (waveCountdown == 0)
            {
                SpawnRobots();
                waveCountdown = 30;
                gameUI.ShowNewWaveText();
            }
        }
    }

    // adding UI (p.309~310)
    public static void RemoveEnemy()
    {
        singleton.enemiesLeft--;
        singleton.gameUI.SetEnemyText(singleton.enemiesLeft);
        // Give player bonus for clearing the wave before timer is done
        if (singleton.enemiesLeft == 0)
        {
            singleton.score += 50;
            singleton.gameUI.ShowWaveClearBonus();
        }
    }

    // adding UI (p.310)
    public void AddRobotKillToScore()
    {
        score += 10;
        gameUI.SetScoreText(score);
    }

    // adding UI (p.310)
    IEnumerator increaseScoreEachSecond()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1);
            score += 1;
            gameUI.SetScoreText(score);
        }
    }


    // chapter 11. game over (p.318)
    /*
1. This method frees the mouse cursor when the game is over 
    so the user can select something from the menu.
2. This will be called when the game is over. 
    It sets the timeScale to 0 so the robots stop moving. 
    It also disables the controls and displays the Game Over panel you just created.
3. When the user wants to restart the game, 
    this method will be used to reload the scene to start the game again.
4. This will be called when the user selects the Exit button. 
    It quits the application, but only if its being run from a build.
5. You also need a method to go back to the main menu 
    when the user selects the Menu button. 
    This just loads the Menu scene.
     * */

    // 1
    public void OnGUI()
    {
        if (isGameOver && Cursor.visible == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    // 2
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        gameOverPanel.SetActive(true);
    }
    // 3
    public void RestartGame()
    {
        //Debug.Log("Button pressed .. Restart Game");
        SceneManager.LoadScene(Constants.SceneBattle);
        gameOverPanel.SetActive(true);
    }
    // 4
    public void Exit()
    {
        //Debug.Log("Button pressed .. Exit");
        Application.Quit();
    }
    // 5
    public void MainMenu()
    {
        //Debug.Log("Button pressed .. Menu");
        SceneManager.LoadScene(Constants.SceneMenu);
    }
}
