using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void SettingsMenu()
    {
        SceneManager.LoadScene("SeetingsMenu");
    }
   
    public void QuitGame()
    {
        Application.Quit();
    }
}