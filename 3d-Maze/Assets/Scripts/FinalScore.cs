using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FinalScore : MonoBehaviour
{

    [SerializeField] private Text resultLabel;
    [SerializeField] private Text scoreLabel;
    // Start is called before the first frame update
    void Start()
    {
        if(SaveData.result == 0){
          scoreLabel.text = "";
          resultLabel.text = "You ran out of time. You Lose!";
        } else {
          scoreLabel.text = "Score: " + SaveData.score.ToString();
          resultLabel.text = "You brought the treasure back! Congratulations!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
