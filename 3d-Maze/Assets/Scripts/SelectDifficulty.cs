using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Need to find a way to pass the difficulty variable to the StartNewGame method of game controller. I tried and i cant figure it out
public class SelectDifficulty : MonoBehaviour
{
    private GameController gm;

    private void Awake() {
        gm = GetComponent<GameController>();
    }
    
    public void StartGame(int difficulty){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gm.StartNewGame(difficulty);
    }
}
