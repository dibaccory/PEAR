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
    // TODO: Validate all text fields. Ensure appropriate values are entered 
    // for all fields. 

            public void OnSignUp()
    {
        if(passwordInput.text != confirmPasswordInput.text)
        {
            // TODO: Handle this a bit better eventually
            Debug.Log("Error. Passwords do not match");
        }
        // TODO: Setup the database to handle more than just a username and password
        authManager.SignUpNewUser(emailInput.text, passwordInput.text);
        Debug.Log("Sign Up");
    }

    IEnumerator HandleAuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation)
    {
        if (task.IsCanceled)
        {
            UpdateStatus("Task was canceled");
        }
        else if (task.IsFaulted)
        {
            UpdateStatus("sorry, error! Error: " + task.Exception);
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
                Classroom classroom = new Classroom(classroomInput.text, "true");

                DatabaseManager.sharedInstance.CreateNewUser(user, newUser.UserId, classroom);

            }

            FirebaseUser player = task.Result;
            UpdateStatus("User: " + player.Email);
            // UpdateStatus("Loading the game scene");

            yield return new WaitForSeconds(1.5f);
            // SceneManager.LoadScene("VuforiaTesting");
        }
    }

    void OnDestroy()
    {
        authManager.authCallback -= HandleAuthCallback;
    }

    void UpdateStatus(string message)
    {
        // TODO: Handle this. Need to add pop-ups or a status text text box
        // statusText.text = message;
    }
}
