using System; // For EventArgs
using Firebase.Auth; // For Firebase Authentication
using UnityEngine;
using UnityEngine.SceneManagement; // For SceneManager

public class LoadSceneWhenUserAuthenticated : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad; 
    private FirebaseAuth auth;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize Firebase Authentication instance
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += HandleAuthStateChanged;
        
        // Check if the user is already authenticated when the scene loads
        CheckUser();
    }
    private void update()
    {
        // Initialize Firebase Authentication instance
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += HandleAuthStateChanged;
        
        // Check if the user is already authenticated when the scene loads
        CheckUser();
    }
    // Ensure you clean up the event handler when the object is destroyed
    private void OnDestroy()
    {
        if (auth != null)
        {
            auth.StateChanged -= HandleAuthStateChanged;
        }
    }

    // Handle the authentication state change event
    private void HandleAuthStateChanged(object sender, EventArgs e)
    {
        CheckUser(); // Recheck the user state when there's a change
    }

    // Check if the user is authenticated and load the scene if they are
    private void CheckUser()
    {
        if (auth.CurrentUser != null) // If the user is signed in
        {
            SceneManager.LoadScene(_sceneToLoad); // Load the specified scene
        }
        else
        {
            Debug.LogWarning("User is not authenticated.");
        }
    }
}
