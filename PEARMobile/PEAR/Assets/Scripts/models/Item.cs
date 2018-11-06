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
    public VideoClip video;
}
