using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// This class came from a UI/Inventory tutorial. Only for making interacting
// with the GUI elements from within the Unity editor easier. 
// TODO: Make it work with the inventory system we're implementing
// [CustomEditor (typeof(Almanac))]
// public class AlmanacEditor : Editor {

    //SerializedProperty itemImagesProperty;
    //SerializedProperty itemsProperty;

    //bool[] showItemSlots = new bool[Almanac.numItemSlots];

    //const string almanacPropItemImagesName = "itemImages";
    //const string almanacPropItemsName = "items";

    //void OnEnable()
    //{
    //    itemImagesProperty = serializedObject.FindProperty(almanacPropItemImagesName);
    //    itemsProperty = serializedObject.FindProperty(almanacPropItemsName);
    //}

    //public override void OnInspectorGUI()
    //{
    //    serializedObject.Update();
    //    for (int i = 0; i < Almanac.numItemSlots; i++)
    //    {
    //        ItemSlotGUI(i);
    //    }
    //    serializedObject.ApplyModifiedProperties();
    //}

    //void ItemSlotGUI(int index)
    //{
    //    EditorGUILayout.BeginVertical(GUI.skin.box);
    //    EditorGUI.indentLevel++;

    //    showItemSlots[index] = EditorGUILayout.Foldout(showItemSlots[index], "Item slot " + index);

    //    if(showItemSlots[index])
    //    {
    //        EditorGUILayout.PropertyField(itemImagesProperty.GetArrayElementAtIndex(index));
    //        EditorGUILayout.PropertyField(itemsProperty.GetArrayElementAtIndex(index));

    //    }

    //    EditorGUI.indentLevel--;
    //    EditorGUILayout.EndVertical();
    //}
// }
