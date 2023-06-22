using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginSignupPlayFabManager : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject loginPanel;

    public TMP_InputField inputUserID;
    public TMP_InputField inputPassword;
    public TMP_InputField inputEmail;
    public TMP_Text displayMessage;

    private string username;
    private string password;
    private string email;

    private GameObject foundGameManager;

    // Start is called before the first frame update
    void Start()
    {
        PlayFabSettings.TitleId = "F2F4E";
        foundGameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //--------------------

    public void UsernameValueChanged()
    {
        username = inputUserID.text.ToString();
    }

    public void PasswordValueChanged()
    {
        password = inputPassword.text.ToString();
    }

    public void EmailValueChanged()
    {
        email = inputEmail.text.ToString();
    }

    // -------------- Login
    public void Login()
    {
        Debug.Log(username + "/" + password);
        var request = new LoginWithPlayFabRequest { Username = username, Password = password };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login successfully");
        displayMessage.text = "Login successfully [" + result.PlayFabId + "]";

        foundGameManager.GetComponent<GameManager>().myGlobalPlayFabId = result.PlayFabId;
        foundGameManager.GetComponent<GameManager>().userName = username;

        menuUI.SetActive(true);
        loginPanel.SetActive(false);

        StartGame();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Login FAILED");
        Debug.LogWarning(error.GenerateErrorReport());
        displayMessage.text = error.GenerateErrorReport();
    }

    //------------- Register (Signup)
    public void Register()
    {
        Debug.Log(username + "/" + password + "/" + email);
        var request = new RegisterPlayFabUserRequest { Username = username, Password = password, Email = email };
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Signup successfully");
        displayMessage.text = "Signup successfully";

        menuUI.SetActive(true);
        loginPanel.SetActive(false);
    }

    private void RegisterFailure(PlayFabError error)
    {
        Debug.LogWarning("Signup FAILED");
        Debug.LogWarning(error.GenerateErrorReport());
        displayMessage.text = error.GenerateErrorReport();
    }

    void StartGame()
    {
        Debug.Log("Now, start the game, enjoy it");
        GetUserData(foundGameManager.GetComponent<GameManager>().myGlobalPlayFabId);
    }

    public void GetUserData(string myPlayFabId)
    {
        if (myPlayFabId == null) { myPlayFabId = foundGameManager.GetComponent<GameManager>().myGlobalPlayFabId; }

        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabId,
            Keys = null
        },
        result => {
                Debug.Log("Got user data:");
                //if (result.Data == null || !result.Data.ContainsKey("Department")) { Debug.Log("NO Department"); }
                if (result.Data == null ) { Debug.Log("NO Data"); }
                else {
                    //Debug.Log("Department is " + result.Data["Department"].Value); 

                    foundGameManager.GetComponent<GameManager>().storedAmmo = int.Parse(result.Data["Ammo"].Value);
                    foundGameManager.GetComponent<GameManager>().storedArmor = int.Parse(result.Data["Armor"].Value);
                    foundGameManager.GetComponent<GameManager>().storedGunItem = int.Parse(result.Data["GunItem"].Value);
                    foundGameManager.GetComponent<GameManager>().storedPlayingTime = int.Parse(result.Data["PlayingTime"].Value);
                }
        }, 
        error => {
                Debug.Log("Got error retrieving user data:");
                Debug.Log(error.GenerateErrorReport());
         });
    }

    /*
    private void GetUserDataSuccess()
    {

    }

    private void GetUserDataFailure()
    {

    }
    */


}
