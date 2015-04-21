using UnityEngine;
using System.Collections;

public class ApplicationQuit : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game"); //Debug
    }
}
