using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfRunCode : MonoBehaviour
{

    //This script is for the scene that appears after the user has answered all 10 questions
    void Start()
    {
        GameObject.Find("Score").gameObject.GetComponent<Text>().text = GameManager.instance.score + "/10";
        if(GameManager.instance.score >= 9)
        {
            GameObject.Find("Message").gameObject.GetComponent<Text>().text = "Good job!";
        }
        else if(GameManager.instance.score >= 7)
        {
            GameObject.Find("Message").gameObject.GetComponent<Text>().text = "Not bad, but could use a little work.";
        }
        else
        {
            GameObject.Find("Message").gameObject.GetComponent<Text>().text = "Reviewing the material would be a good idea.";
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.instance.score = 0; //Resets the score and number of questions answered upon return to main menu
            GameManager.instance.questionsAnswered = 0;
            SceneManager.LoadScene(0); //Goes to the main menu
        }
    }
}
