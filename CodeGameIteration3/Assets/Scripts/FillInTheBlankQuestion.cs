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
        questionList.Add(makeForLoopQuestion);
        questionList.Add(makeWhileLoopQuestion);
        questionList.Add(makeArrayQuestion);
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
        newCode += "\ncout << foo << endl;";

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
        if (randomOp == '/')
        {
            wrongAnswerFeedback += "\nNote: With integer division, decimals get cut off.  (ie. 1.85 will become 1)";
        }

        //New question
        newCode += "int foo;";
        newCode += "\nfoo = ";
        newCode += num1 + " " + randomOp + " " + num2 + ";";
        newCode += "\ncout << foo << endl;";

        return new FBQData(newCode, wrongAnswerFeedback, correctAnswer);
    }

    private FBQData makeVarOpVarQuestion()
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
        if (randomOp == '/')
        {
            wrongAnswerFeedback += "\nNote: With integer division, decimals get cut off.  (ie. 1.85 will become 1)";
        }

        //New question
        newCode += "int foo = " + num1 + ";";
        newCode += "\nint bar = " + num2 + ";";
        newCode += "\nint x = foo " + randomOp + " bar;";
        newCode += "\ncout << x << endl;";

        return new FBQData(newCode, wrongAnswerFeedback, correctAnswer);
    }

    private FBQData makeForLoopQuestion()
    {
        int num1 = Random.Range(0, 10);
        int num2 = Mathf.Min(10, num1 + Random.Range(0, 10));

        int correctAnswer = 0;
        for (int i = num1; i < num2; i++)
        {
            correctAnswer += 1;
        }

        string newCode = "";
        newCode += "int x = 0;\n";
        newCode += "\nfor (int i = " + num1 + "; i < " + num2 + "; i++)";
        newCode += "\n{";
        newCode += "\n  x += 1;";
        newCode += "\n}";
        newCode += "\n\ncout << x << endl;";

        string wrongAnswerFeedback = "The correct answer was " + correctAnswer + ".\n";
        wrongAnswerFeedback += "The for loop would have run " + num2 + " - " + num1 + " = " + correctAnswer + " times.";

        return new FBQData(newCode, wrongAnswerFeedback, correctAnswer);
    }

    private FBQData makeWhileLoopQuestion()
    {
        int num1 = Random.Range(0, 10);
        int num2 = Mathf.Min(10, num1 + Random.Range(0, 10));

        int correctAnswer = 0;
        int i = num1;
        while (i < num2)
        {
            correctAnswer += 1;
            i++;
        }

        string newCode = "";
        newCode += "int x = 0;";
        newCode += "\nint i = " + num1 + "\n";
        newCode += "\nwhile (i < " + num2 + ")";
        newCode += "\n{";
        newCode += "\n  x += 1;";
        newCode += "\n  i++;";
        newCode += "\n}";
        newCode += "\n\ncout << x << endl;";

        string wrongAnswerFeedback = "The correct answer was " + correctAnswer + ".\n";
        wrongAnswerFeedback += "The while loop would have run " + num2 + " - " + num1 + " = " + correctAnswer + " times.";

        return new FBQData(newCode, wrongAnswerFeedback, correctAnswer);
    }

    private FBQData makeArrayQuestion()
    {
        //Generates a random length for the array and a random index within that length
        int randArrayLength = Random.Range(1, 5);
        int randArrayIndex = Random.Range(0, randArrayLength);

        //Randomly generates elements for the array
        int[] elements = new int[randArrayLength];
        for (int i = 0; i < randArrayLength; i++)
        {
            elements[i] = Random.Range(0, 9);
        }

        int correctAnswer = elements[randArrayIndex];

        string newCode = "";
        newCode += "int x[" + randArrayLength + "] = {";
        newCode += string.Join(", ", elements) + "};";
        newCode += "\nint index = " + randArrayIndex + ";";
        newCode += "\ncout << x[index] << endl;";

        string wrongAnswerFeedback = "x[" + randArrayIndex + "] is element " + randArrayIndex + " of array x, which is " + correctAnswer + ".";
        wrongAnswerFeedback += "\nNote: The first element in an array is at index 0 rather than 1.";

        return new FBQData(newCode, wrongAnswerFeedback, correctAnswer);
    }


}
