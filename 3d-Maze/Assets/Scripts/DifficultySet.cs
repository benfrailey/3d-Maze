using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void setDifficulty(int difficulty){
      DifficultySave.difficulty = difficulty;
      
      
      Debug.Log(DifficultySave.difficulty);
      
      if(difficulty == 5){
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
      } else {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }
    }
}
