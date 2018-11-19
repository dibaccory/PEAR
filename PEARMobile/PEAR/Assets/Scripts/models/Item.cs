using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[CreateAssetMenu]
public class Item : ScriptableObject {

    public string itemName;
    public string tag;
    public Sprite sprite;
    public Material material;
    public bool isPlaced = false;
    public bool isCollected = false;
<<<<<<< HEAD
    public VideoClip video;
=======
>>>>>>> 59dd34b3cc50553569616b8a2ce918f0a432b6e7
}
