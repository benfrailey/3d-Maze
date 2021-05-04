using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame(){
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void StartGame(int difficulty){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void PlayAgain(){
        SceneManager.LoadScene(1);
    }
}
