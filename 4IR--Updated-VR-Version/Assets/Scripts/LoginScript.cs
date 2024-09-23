using System.Collections;
using System.Collections.Generic;
using Firebase;
using System;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginScript : MonoBehaviour
{
    public TMP_InputField emailText;
     public TMP_InputField passwordText;
    public TMP_Text statusText;
    public Button logOutBtn;
    
    private DatabaseReference databaseRef;
    private FirebaseUser user;
    private bool firebaseInitialized = false;


    private bool ValidateEmail(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@]+@\w+(\.\w+)+\w$");
    }

    private bool ValidatePassword(string password)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]") &&
               System.Text.RegularExpressions.Regex.IsMatch(password, @"[a-z]") &&
               System.Text.RegularExpressions.Regex.IsMatch(password, @"[0-9]") &&
               System.Text.RegularExpressions.Regex.IsMatch(password, @"[^A-Za-z0-9]") &&
               password.Length > 5;
    }

    void Start()
    {
        logOutBtn.gameObject.SetActive(false);
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            if (task.IsCompleted && task.Result == DependencyStatus.Available)
            {
                
                var auth = FirebaseAuth.DefaultInstance;

                auth.StateChanged += AuthStateChanged;
                AuthStateChanged(this, null);
                databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
                firebaseInitialized = true;
                Debug.Log("Firebase has been initialized!");
            }
            else
            {
                statusText.text = "Failed to initialize Firebase.";
            }
        });
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        if (auth.CurrentUser != null)
        {
            bool signIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signIn && user != null)
            {
                Debug.Log("Signed out " + user.Email);
            }
            user = auth.CurrentUser;

            if (signIn)
            {
                Debug.Log("Signed in " + user.Email);
                logOutBtn.gameObject.SetActive(true);
            }
        }
    }

    public void Login()
    {
        if (!firebaseInitialized)
        {
            statusText.text = "Firebase is not initialized. Please try again later.";
            return;
        }
        string email = $"{emailText.text}";
        string password = $"{passwordText.text}";
        if (!ValidateEmail(emailText.text) || !ValidatePassword(passwordText.text))
        {
            statusText.text = "Invalid email or password format.";
            return;
        }
        StartCoroutine(LoginAsync(email, password));
    }

     private IEnumerator LoginAsync(string email, string password)
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            // Firebase-specific exception handling
            FirebaseException firebaseEx = loginTask.Exception.GetBaseException() as FirebaseException;
            string errorMessage = "";

            if (firebaseEx != null)
            {
                AuthError authError = (AuthError)firebaseEx.ErrorCode;
                switch (authError)
                {
                    case AuthError.MissingEmail:
                        errorMessage += "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        errorMessage += "Missing Password";
                        break;
                    case AuthError.WrongPassword:
                        errorMessage += "Wrong Password";
                        break;
                    case AuthError.InvalidEmail:
                        errorMessage += "Invalid Email";
                        break;
                    case AuthError.UserMismatch:
                        errorMessage += "User mismatch";
                        break;
                    case AuthError.UserNotFound:
                        errorMessage += "User Not Found";
                        break;
                    case AuthError.OperationNotAllowed:
                        errorMessage += "This operation is not allowed. You must enable this service in the console.";
                        break;
                    default:
                        errorMessage = "Login Failed";
                        break;
                }
            }
            else
            {
                errorMessage = "An unknown error occurred.";
            }

            Debug.LogError("Login failed: " + errorMessage);
            statusText.text = errorMessage;
        }
        else
        {
            Firebase.Auth.AuthResult authResult = loginTask.Result;
            user = authResult.User;
            string userId = user.UserId;
            DatabaseReference userRef = databaseRef.Child("users").Child(userId);
            Debug.Log("User logged in successfully: " + user.Email);
            statusText.text = "Login Successful! \n"+ user.Email;
        }
    }
    
}

