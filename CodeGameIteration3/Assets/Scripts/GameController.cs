/*
 * @author: Kaitlyn Deppmeier
 * @class: CPSC 362-07
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    GameObject answerBox;  //The InputField that the user enters the answer into
    GameObject codeQuestion;  //The Text object that contains the question for the user to answer
    GameObject correctPopup; //The panel that serves as a popup when the user gets an answer correct
    GameObject wrongPopup; //The panel that pops up when the user gets an answer wrong

    //[System.Serializable]
    //public int[] questionTypes; //A list of the question types that are going to be used
    int[] questionTypes;

    private int correctAnswer;  //The correct answer for a given question
    private string wrongAnswerFeedback; //The feedback should the user get the question wrong

    private bool wrongPanelUp = false; //flag that signals whether the wrong panel popup is active

    private FillInTheBlankQuestion fbq;

    delegate void QuestionGenMethod(); //List of functions with different names - each generates a different
    List<QuestionGenMethod> questionList = new List<QuestionGenMethod>();  //question type
    //QuestionGenMethod questionList;

    // Start is called before the first frame update.  Only runs once each time the scene loads.
    void Start()
    {
        //Finds the corresponding game objects
        answerBox = GameObject.Find("AnswerField").gameObject;
        codeQuestion = GameObject.Find("CodeQuestion").gameObject;

        correctPopup = GameObject.Find("CorrectPopup").gameObject; //Note: Find only works on active objects
        correctPopup.SetActive(false); //Deactivates the popup panel
        wrongPopup = GameObject.Find("WrongPopup").gameObject;
        wrongPopup.SetActive(false);

        questionTypes = GameManager.instance.fitbTypes;  //Grabs the question types from the game manager

        fbq = gameObject.GetComponent<FillInTheBlankQuestion>(); //Picks a random fill in the blank question

        StartCoroutine(DelayedStart()); //Generates the first question
        answerBox.GetComponent<InputField>().ActivateInputField();  //So the user doesn't need to click inside the input field to start
    }

    // Update is called once per frame
    void Update()
    {
        //If the user presses enter
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (wrongPanelUp)
            {
                //closes the popup and creates a new question
                wrongPopup.SetActive(false);
                wrongPanelUp = false;

                newQuestion();

                answerBox.GetComponent<InputField>().ActivateInputField();  //So the user doesn't have to click in the input field
                answerBox.GetComponent<InputField>().text = "";  //clears the previous answer from the input field
            }
            else
            {
                if (answerBox.GetComponent<InputField>().text.Equals(correctAnswer.ToString()))  //if answer is correct
                {
                    newQuestion();  //Generates new question
                    StartCoroutine(Popup());  //Calls the coroutine for the popup

                    answerBox.GetComponent<InputField>().ActivateInputField();  //So the user doesn't have to click in the input field
                    answerBox.GetComponent<InputField>().text = "";  //clears the previous answer from the input field
                }
                else if (!answerBox.GetComponent<InputField>().text.Equals("")) //if answer is wrong, not just blank
                {
                    wrongPopup.transform.Find("Feedback").GetComponent<Text>().text = wrongAnswerFeedback;  //Changes the text on the popup
                    //to reflect the question
                    wrongPopup.SetActive(true);  //activates the popup
                    wrongPanelUp = true;
                }
            }
        }

        //Closes the application if the user presses the escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //Generates a new question
    void newQuestion()
    {
        //FBQData qData = fbq.pickRandomQuestion(); //The method from the other class that picks a random question
        FBQData qData = fbq.pickRandomQuestion(questionTypes); //The method from the other class that picks a random question
        codeQuestion.GetComponent<Text>().text = qData.code; //gets the code, feedback, and answer from the returned object
        wrongAnswerFeedback = qData.feedback;
        correctAnswer = qData.answer;
    }

    //Called when the user gets the correct answer
    IEnumerator Popup()
    {
        correctPopup.SetActive(true);  //Activates the popup
        yield return new WaitForSeconds(0.7F);  //Waits the inputed number of seconds to close the popup
        correctPopup.SetActive(false);
    }

    //Delays slightly before generating first question.  Gies FillInTheBlankQuestion time to load
    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.02F);
        newQuestion();
    }
}
