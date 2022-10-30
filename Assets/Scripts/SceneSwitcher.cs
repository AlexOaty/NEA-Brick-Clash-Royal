using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public void Login()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
      
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Settings()
    {
        SceneManager.LoadScene(2);
    }

    public void Register()
    {
        SceneManager.LoadScene(3);
    }
}
