using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    
    public void StartGame()
    {
        //Loads Level1 Scene
        ScoreBoardController.health = 100;
        ScoreBoardController.scoreCounter = 0;
        SceneManager.LoadScene(1);
    }   

    public void ContinueGame()
    {
        //Loads level saved in user prefs
    }

    public void GoToOptionMenu()
    {
        SceneManager.LoadScene(4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
