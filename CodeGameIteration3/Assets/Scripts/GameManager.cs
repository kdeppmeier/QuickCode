using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //The GameManager will persist between scenes
    public static GameManager instance = null;

    //[System.Serializable]
    public int[] fitbTypes; //The types of fill-in-the-blank questions to use
    public int score = 0;   //The user's score on the run
    public int questionsAnswered = 0;   //The number of questions answered on the run

    //creates a new GameManager if one doesn't already exist
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //Allows the gameObject and script to persist when the scene is changed
        DontDestroyOnLoad(gameObject);
    }

}
