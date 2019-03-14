using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button Button1, Button2, Button3, Button4;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        Button1.onClick.AddListener(TaskOnClick);
        Button2.onClick.AddListener(TaskOnClick);
        Button3.onClick.AddListener(delegate { TaskWithParameters("Correct"); });
        Button4.onClick.AddListener(TaskOnClick);
        
    }

    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("Incorrect");
    }

    void TaskWithParameters(string message)
    {
        //Output this to console when the Button2 is clicked
        Debug.Log(message);
    }

    void ButtonClicked(int buttonNo)
    {
        //Output this to console when the Button3 is clicked
        Debug.Log("Button clicked = " + buttonNo);
    }
}