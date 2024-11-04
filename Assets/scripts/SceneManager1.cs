using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScenesManagement : MonoBehaviour
{
    public void ButtonStart()
    {
        SceneManager.LoadScene(0);
    }

    public void ButtonExit()
    {
        Debug.Log("Hasta la proxima");
        Application.Quit();
    }
}

