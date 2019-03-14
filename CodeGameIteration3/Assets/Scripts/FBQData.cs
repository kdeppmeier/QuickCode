/*
 * @author: Kaitlyn Deppmeier
 * @class: CPSC 362-07
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FBQData
{
    //This class contains the data for fill-in-the-blank style questions
    public string code; //The question
    public string feedback; //The feedback given for a wrong answer
    public int answer; //The answer to the question

    public FBQData(string c, string f, int a)
    {
        code = c;
        feedback = f;
        answer = a;
    }
}
