using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoardManager : MonoBehaviour {

    public List<User> userList = new List<User>();

    private void Awake()
    {
        userList.Clear();
        DatabaseManager.sharedInstance.GetUsers(result =>
        {
            userList = result;
            Debug.Log(userList[0].email);
        });
    }
}
