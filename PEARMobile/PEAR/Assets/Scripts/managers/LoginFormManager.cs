using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class LoginFormManager : MonoBehaviour {

    // UI objects linked from the inspector
    public InputField emailInput;
    public InputField passwordInput;

    public Button loginButton;

    public Text statusText;

    public AuthManager authManager;

    void Awake()
    {
        ToggleButtonStates(false);
        // Auth delegate subscriptions
        authManager.authCallback += HandleAuthCallback;
    }

    /// <summary>
    /// Validates the email input
    /// </summary>
    public void ValidateEmail()
    {
        string email = emailInput.text;
        var regexPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        if (email != "" && Regex.IsMatch(email, regexPattern))
        {
            ToggleButtonStates(true);
        }
        else
        {
            ToggleButtonStates(false);
        }
    }

    // Firebase methods

    public void LoadSignUpScreen()
    {
        SceneManager.LoadScene("SignupScreen");
    }

    public void OnLogin()
    {
        authManager.LoginExistingUser(emailInput.text, passwordInput.text);
    }

    IEnumerator HandleAuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation)
    {
        if (task.IsCanceled)
        {
            UpdateStatus("Task was canceled");
        }
        else if (task.IsFaulted)
        {
            UpdateStatus("sorry, error! Error: " + task.Exception.ToString().Substring(task.Exception.ToString().IndexOf("Firebase.FirebaseException:") + "Firebase.FirebaseException:".Length));
            //Debug.Log(task.Exception);
        }
        else if (task.IsCompleted)
        {

            Firebase.Auth.FirebaseUser player = task.Result;
            UpdateStatus("User: " + player.Email);
            // UpdateStatus("Loading the game scene");

            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("UserProfile");
        }
    }

    void OnDestroy()
    {
        authManager.authCallback -= HandleAuthCallback;
    }

    // Utilities
    void ToggleButtonStates(bool toState)
    {
        loginButton.interactable = toState;
    }

    void UpdateStatus(string message)
    {
        statusText.text = message;
    }
}
