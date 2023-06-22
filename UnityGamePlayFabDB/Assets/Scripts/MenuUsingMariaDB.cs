using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUsingMariaDB : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject loginPanel;

    // Start is called before the first frame update
    void Start()
    {
        menuUI.SetActive(true);
        loginPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // adding a Login button
    public void ShowLoginPanel()
    {
        Debug.Log("ShowLoginPanel() is called ..");
        menuUI.SetActive(false);
        loginPanel.SetActive(true);
    }

    // adding a main menu (p.314)
    // 1
    public void StartGame()
    {
        Debug.Log("Game START !");
        //SceneManager.LoadScene("Battle");
    }
}
