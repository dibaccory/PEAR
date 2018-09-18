using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Item : ScriptableObject {
    
    public string itemName;
    public Sprite sprite;
    public List<Question> collectQuestions;
    public List<Question> gatherQuestions;

    public virtual void Use()
    {
       
    }

    public void GetGatherQuestions()
    {
        // collectQuestions = DatabaseManager.getQuestions("astonomy", "solar system", itemName, "gather");
    }

    public void GetCollectQuestions()
    {
        // collectQuestions = DatabaseManager.getQuestions("astonomy", "solar system", itemName, "collect");
    }
}
