using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Item : ScriptableObject {

    public string itemName;
    public Sprite sprite;

    public virtual void Use()
    {
        
    }
}
