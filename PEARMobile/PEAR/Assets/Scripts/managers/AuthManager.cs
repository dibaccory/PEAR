using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour
{

    Firebase.Auth.FirebaseAuth auth;

    // Delegates
    public delegate IEnumerator AuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation);
    public event AuthCallback authCallback;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SignUpNewUser(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            StartCoroutine(authCallback(task, "sign_up"));
        });
    }

    public void LoginExistingUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            StartCoroutine(authCallback(task, "login"));
        });
    }
}

//import UIKit
//import Firebase

//@UIApplicationMain
//class AppDelegate : UIResponder, UIApplicationDelegate
//{

//    var window: UIWindow?

//    func application(_ application: UIApplication,
//      didFinishLaunchingWithOptions launchOptions: [UIApplicationLaunchOptionsKey: Any]?)
//    -> Bool {
//    FirebaseApp.configure()
//    return true
//  }
//}