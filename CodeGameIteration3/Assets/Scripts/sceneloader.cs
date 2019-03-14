using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneloader : MonoBehaviour
{
    public void sceneload(int sceneindex)
    {
        SceneManager.LoadScene(sceneindex);


    }


}
