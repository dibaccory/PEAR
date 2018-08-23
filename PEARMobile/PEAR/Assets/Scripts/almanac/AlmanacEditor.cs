using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(Almanac))]
public class AlmanacEditor : Editor {

    private SerializedProperty itemImagesProperty;
    private SerializedProperty itemsProperty;

    private bool[] showItemSlots = new bool[Almanac.numItemSlots];

    private const string almanacPropItemImagesName = "itemImages";
    private const string almanacPropItemsName = "items";

    private void OnEnable()
    {
        itemImagesProperty = serializedObject.FindProperty(almanacPropItemImagesName);
        itemsProperty = serializedObject.FindProperty(almanacPropItemsName);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        for (int i = 0; i < Almanac.numItemSlots; i++)
        {
            ItemSlotGUI(i);
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void ItemSlotGUI(int index)
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        showItemSlots[index] = EditorGUILayout.Foldout(showItemSlots[index], "Item slot " + index);

        if(showItemSlots[index])
        {
            EditorGUILayout.PropertyField(itemImagesProperty.GetArrayElementAtIndex(index));
            EditorGUILayout.PropertyField(itemsProperty.GetArrayElementAtIndex(index));

        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }
}
