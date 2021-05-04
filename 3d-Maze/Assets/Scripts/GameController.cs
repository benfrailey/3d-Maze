using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour
{
    //1
    [SerializeField] private FpsMovement player;
    [SerializeField] private Text timeLabel;
    [SerializeField] private Text scoreLabel;

    private MazeConstructor generator;
    

    //2
    private DateTime startTime;
    private int timeLimit;
    private int reduceLimitBy;

    private int score;
    private bool goalReached;      
    
    //3
    void Start() {
        generator = GetComponent<MazeConstructor>();

        if(SaveData.difficulty == 0)
            StartNewGame(2);
        else
            StartNewGame(SaveData.difficulty);

    }

    //4
    public void StartNewGame(int difficulty)
    {
        timeLimit = 60 + (difficulty * 20);
        reduceLimitBy = 5;
        startTime = DateTime.Now;

        score = 0;
        scoreLabel.text = score.ToString();

        StartNewMaze(difficulty);
    }

    //5
    private void StartNewMaze(int difficulty)
    {
        switch(difficulty){
            case 1:
                generator.GenerateNewMaze(13, 15, OnStartTrigger, OnGoalTrigger);
                break;
            case 2:
                generator.GenerateNewMaze(25, 30, OnStartTrigger, OnGoalTrigger);
                break;
            case 3:
                generator.GenerateNewMaze(40, 45, OnStartTrigger, OnGoalTrigger);
                break;
            case 4:
                generator.GenerateNewMaze(60, 75, OnStartTrigger, OnGoalTrigger);
                break;

        }

        float x = generator.startCol * generator.hallWidth;
        float y = 1;
        float z = generator.startRow * generator.hallWidth;
        player.transform.position = new Vector3(x, y, z);

        goalReached = false;
        player.enabled = true;

        // restart timer
        timeLimit -= reduceLimitBy;
        startTime = DateTime.Now;
    }

    //6
    void Update()
    {
        if (!player.enabled)
        {
            return;
        }

        if(player.checkCharMovement() == true){
            int timeUsed = (int)(DateTime.Now - startTime).TotalSeconds;
            int timeLeft = timeLimit - timeUsed;
                    
            if (timeLeft > 0)
            {
                timeLabel.text = timeLeft.ToString();
                
            }
            else
            {
                timeLabel.text = "GAME OVER";
                player.enabled = false;
                
                SaveData.result = 0;
                new WaitForSeconds(1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                
            }
        }
    }

    //7
    private void OnGoalTrigger(GameObject trigger, GameObject other)
    {
        Debug.Log("Goal!");
        goalReached = true;

        score += 1;
        scoreLabel.text = score.ToString();

        Destroy(trigger);
    }

    private void OnStartTrigger(GameObject trigger, GameObject other)
    {
        if (goalReached)
        {
            Debug.Log("Finish!");
            player.enabled = false;
            
            SaveData.result = 1;
            SaveData.score = score;
            
            new WaitForSeconds(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

//            Invoke("StartNewMaze", 4);
        }
    }
}
