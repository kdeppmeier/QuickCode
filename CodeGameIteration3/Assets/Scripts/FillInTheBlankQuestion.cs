/*
 * @author: Kaitlyn Deppmeier
 * @class: CPSC 362-07
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillInTheBlankQuestion:MonoBehaviour
{
    string ops = "+-*/";

    delegate FBQData QuestionGenMethod(); //List of functions with different names - each generates a different
    List<QuestionGenMethod> questionList = new List<QuestionGenMethod>();  //question type

    private void Start()
    {
        //Adds the methods to the list
        questionList.Add(makeNumQuestion);
        questionList.Add(makeNumOpNumQuestion);
        questionList.Add(makeVarOpVarQuestion);
    }

    public FBQData pickRandomQuestion()
    {
        int questionType = Random.Range(0, questionList.Count);  //picks a random question type from the list
        FBQData returnData = questionList[questionType]();  //executes the function corresponding to the new question type

        return returnData;
    }

    //Allows the user to restrict random questions to certain types
    public FBQData pickRandomQuestion(int[] questionTypes)
    {
        //Checks if the indicies are in range.  If not, aborts
        foreach (int x in questionTypes)
        {
            if(x >= questionList.Count || x < 0)
            {
                Debug.Log("Numbers in questionTypes are out of Range.  Adjust the values placed into the inspector.");
                Debug.Log(questionList.Count + " " + x);
                return new FBQData("", "", 0);
            }
        }

        FBQData returnData = questionList[questionTypes[Random.Range(0, questionTypes.Length)]]();  //executes the function corresponding to the new question type

        return returnData;
    }

    private FBQData makeNumQuestion()
    {
        string newCode = "";  //The string containing the code question

        int correctAnswer = Random.Range(1, 10);  //random numbers from 1 to 9, the correct answer
        string wrongAnswerFeedback = "foo was set to " + correctAnswer;  //Displayed if the user gets it wrong

        //new Question
        newCode += "int foo;";
        newCode += "\nfoo = " + correctAnswer;

        return new FBQData(newCode, wrongAnswerFeedback, correctAnswer);
    }

    private FBQData makeNumOpNumQuestion()
    {
        string newCode = "";  //The string containing the code question

        char randomOp = ops[Random.Range(0, 4)];  //chooses a random index in the ops string
        int num1 = Random.Range(1, 10);  //random numbers from 1 to 9
        int num2 = Random.Range(1, 10);

        int correctAnswer = 0;

        //Finds the correct answer
        switch (randomOp)
        {
            case '+':
                correctAnswer = num1 + num2;
                break;
            case '-':
                correctAnswer = num1 - num2;
                break;
            case '*':
                correctAnswer = num1 * num2;
                break;
            case '/':
                correctAnswer = num1 / num2;
                break;
            default:
                break;
        }

        string wrongAnswerFeedback = num1 + " " + randomOp + " " + num2 + " = " + correctAnswer;

        //New question
        newCode += "int foo;";
        newCode += "\nfoo = ";
        newCode += num1 + " " + randomOp + " " + num2 + ";";
        newCode += "\ncout << foo << endl;";

        return new FBQData(newCode, wrongAnswerFeedback, correctAnswer);
    }

    FBQData makeVarOpVarQuestion()
    {
        string newCode = "";  //The string containing the code question

        char randomOp = ops[Random.Range(0, 4)];  //chooses a random index in the ops string
        int num1 = Random.Range(1, 10);  //random numbers from 1 to 9
        int num2 = Random.Range(1, 10);

        int correctAnswer = 0;

        //Finds the correct answer
        switch (randomOp)
        {
            case '+':
                correctAnswer = num1 + num2;
                break;
            case '-':
                correctAnswer = num1 - num2;
                break;
            case '*':
                correctAnswer = num1 * num2;
                break;
            case '/':
                correctAnswer = num1 / num2;
                break;
            default:
                break;
        }

        string wrongAnswerFeedback = "foo " + randomOp + " bar = " + num1 + " " + randomOp + " " + num2 + " = " + correctAnswer;

        //New question
        newCode += "int foo = " + num1 + ";";
        newCode += "\nint bar = " + num2 + ";";
        newCode += "\nint x = foo " + randomOp + " bar;";
        newCode += "\ncout << x << endl;";

        return new FBQData(newCode, wrongAnswerFeedback, correctAnswer);
    }
}
