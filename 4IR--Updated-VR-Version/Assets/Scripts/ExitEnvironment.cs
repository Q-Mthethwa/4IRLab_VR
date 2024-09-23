using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitEnvironment : MonoBehaviour
{
    public void Exit()
    {
        //If the game is running on hardrive
        #if UNITY_STANDALONE
        //Quit the application
        Application.Quit();
        #endif

        //If the game is running in the editor
        #if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying= false;
        #endif
    }
    
}
