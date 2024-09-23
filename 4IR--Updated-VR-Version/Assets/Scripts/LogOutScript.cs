using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth; // Firebase authentication namespace
using UnityEngine.UI; // Unity's UI system

public class LogOutSript : MonoBehaviour
{
    // Function called when the button is clicked
    public void OnPointClick()
    {
        // Check if the FirebaseAuth instance is initialized
        if (FirebaseAuth.DefaultInstance != null)
        {
            // Log the user out
            FirebaseAuth.DefaultInstance.SignOut();
            Debug.Log("User logged out successfully.");
        }
        else
        {
            Debug.LogError("Firebase Auth is not initialized.");
        }
    }
}
