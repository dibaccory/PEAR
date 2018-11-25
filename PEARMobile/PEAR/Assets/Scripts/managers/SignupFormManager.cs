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

public class SignupFormManager : MonoBehaviour {

    public InputField firstNameInput, lastNameInput, emailInput, passwordInput,
                      confirmPasswordInput;
    public InputField classroomInput;
    public Text loginStatusText;

    public Button signUpButton;
    public AuthManager authManager;

    void Awake()
    {
        signUpButton.interactable = false;

        // Auth delegate subscriptions
        authManager.authCallback += HandleAuthCallback;
    }

    public void ValidateEmail()
    {
        string email = emailInput.text;
        var regexPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        if (email != "" && Regex.IsMatch(email, regexPattern))
        {
            signUpButton.interactable = true;
        }
        else
        {
            signUpButton.interactable = false;
        }
    }
    public void OnSignUp()
    {
        if (passwordInput.text != confirmPasswordInput.text)
        {
            UpdateStatus("Passwords do not match.");
        }
        else if (classroomInput.text != "astronomy")
        {
            UpdateStatus("Please enter a valid classroom");
        }
        else if(firstNameInput.text.Length == 0 || lastNameInput.text.Length == 0)
        {
            UpdateStatus("Please enter a valid first and last name");
        }
        else
        {
            authManager.SignUpNewUser(emailInput.text, passwordInput.text);
        }
        
    }

    public void ReturnToLoginScene()
    {
        FindObjectOfType<SceneController>().FadeAndLoadScene("LoginScreen");
    }

    IEnumerator HandleAuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation)
    {
        if (task.IsCanceled)
        {
            UpdateStatus("Task was canceled");
        }
        else if (task.IsFaulted)
        {
            UpdateStatus("Sorry, there was an error! Please try again");
            Debug.Log(task.Exception);
        }
        else if (task.IsCompleted)
        {
            if (operation == "sign_up")
            {
                string combinedName = firstNameInput.text + " " + lastNameInput.text;
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("welcome to PEAR {0}", firstNameInput.text);
                
                User user = new User(newUser.Email, combinedName);
                Classroom classroom = new Classroom(classroomInput.text);

                DatabaseManager.sharedInstance.CreateNewUser(user, newUser.UserId, classroom);

            }
            FirebaseUser player = task.Result;
            UpdateStatus("User created successfully!");
            yield return new WaitForSeconds(1.5f);
            FindObjectOfType<SceneController>().FadeAndLoadScene("LoginScreen");
        }
    }

    void OnDestroy()
    {
        authManager.authCallback -= HandleAuthCallback;
    }

    void UpdateStatus(string message)
    {
        loginStatusText.text = message;
    }
}
