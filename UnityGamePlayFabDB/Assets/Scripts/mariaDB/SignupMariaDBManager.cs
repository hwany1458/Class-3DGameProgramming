using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using MySql.Data.MySqlClient;

using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;

public class SignupMariaDBManager : MonoBehaviour
{
    MySqlConnection connection;

    public TMP_InputField inputUserID;
    public TMP_InputField inputPassword;
    public TMP_InputField inputEmail;
    public TMP_Text displayMessage;

    private string username;
    private string password;
    private string email;

    //---------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        SetupSQLConnection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //--------------------------
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
    //--------------------------

    private void SetupSQLConnection()
    {
        if (connection == null)
        {
            string connectionString = "SERVER=61.245.246.242; Port=3306; " +
                "DATABASE=wonkwangdc; " +
                "UID=wonkwangdc; " +
                "PASSWORD=wkdc2017!A;";
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                Debug.Log("[MariaDB Connection] Connected successfully ..");
            }
            catch (MySqlException ex)
            {
                Debug.LogError("[MariaDB Connection Error] " + ex.ToString());
            }
        }
    }
}
