using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LoginRegister : MonoBehaviour
{
    string Username;
    string Password;
    string path = "C:\\Users\\Alex\\source\\repos\\NEA-Lego-Clash-Royale\\Assets\\Scripts\\UserInfo.txt";
    public TextAsset text;

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
        string[] info = File.ReadAllText(path).Split("\n");
        foreach (string line in info)
        {
            string[] strings = line.Split(',');
            if (Username == strings[0])
            {
                Debug.Log("Username Taken");
                return;
            }
        }
        File.AppendAllText(path, Username + "," + Password + "\n");
        Debug.Log($"Username: {Username}\nPassword: {Password}");
    }

    public void Login()
    {
        string[] info = File.ReadAllText(path).Split("\n");
        foreach (string line in info)
        {
            string[] strings = line.Split(',');
            if (Username == strings[0] && Password == strings[1])
            {
                Debug.Log("Login successful");
                return;
            }
        }
        Debug.Log("Username or password incorrect");
    }
}
