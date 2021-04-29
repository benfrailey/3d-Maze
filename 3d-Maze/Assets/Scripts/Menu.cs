using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame(){
        Application.Quit();
    }

    public void StartGame(int difficulty){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
