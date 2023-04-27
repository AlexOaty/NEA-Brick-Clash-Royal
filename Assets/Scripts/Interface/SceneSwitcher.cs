using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public void Quit()
    {
      
    }
    public void LoadScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
