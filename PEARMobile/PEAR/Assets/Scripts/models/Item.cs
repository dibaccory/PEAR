﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Item : ScriptableObject {
    
    public string itemName;
    public string tag;
    public Sprite sprite;
    public Material material;
}
