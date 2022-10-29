using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoginRegister : MonoBehaviour
{
    string Username;
    string Password;

    public void ReadUsername(string U)
    {
        Username = U;
        Debug.Log(Username);
    }

    public void ReadPassword(string P)
    {
        Password = P;
        Debug.Log(Password);
    }

    public void Register()
    {
        Debug.Log($"Username: {Username}\nPassword: {Password}");
    }
}
