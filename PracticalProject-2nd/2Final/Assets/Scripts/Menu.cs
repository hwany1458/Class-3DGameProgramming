using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
1. This method will be used when the user clicks the Begin button; 
    it simply loads the Battle scene.
2. This method will be used to exit the application. 
    However, note that if you are running the game in Unity 
    (instead of within a standalone build) this won¡¯t do anything.
     * */

    // adding a main menu (p.314)
    // 1
    public void StartGame()
    {
        //SceneManager.LoadScene("Battle");
        SceneManager.LoadScene("Level01");

        /*
        Scene 'Level01' couldn't be loaded 
        because it has not been added to the build settings or the AssetBundle has not been loaded.
        To add a scene to the build settings use the menu File->Build Settings...
         */
    }
    // 2
    public void Quit()
    {
        Application.Quit();
    }
}
