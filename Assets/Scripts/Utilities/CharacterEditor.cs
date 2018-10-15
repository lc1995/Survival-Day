/*******************************************
* Description
* This class is responsible for Character window in editor
* It will simutaneously update the data of specific character
*******************************************/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class CharacterEditor : EditorWindow {

    [MenuItem("Window/Character")]
    public static void openWindow(){
        CharacterEditor window = (CharacterEditor)EditorWindow.GetWindow(typeof(CharacterEditor));
        window.titleContent = new GUIContent("Character");
        window.Show();
    }

    private Dictionary<string, Character> characters = new Dictionary<string, Character>(); // characters list
    private int selected = 0;   // index of the selected character

    void OnEnable(){
        Init();
    }

    void OnFocus(){
        Init();
    }

    void OnInspectorUpdate(){
        Repaint();
    }

    void OnGUI(){
        string[] names = characters.Keys.ToArray();
        selected = EditorGUILayout.Popup(selected, names);
        Character ch = characters[names[selected]];

        if(ch == null)
            return;

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField("Name", ch.name);

        GUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Hp", ch.currentProperty.hp.ToString() + " / " + ch.finalProperty.hp.ToString());
        EditorGUILayout.TextField("Hunger", ch.currentProperty.hunger.ToString() + " / " + ch.finalProperty.hunger.ToString());
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Physical Damage", ch.currentProperty.pDamage.ToString());
        EditorGUILayout.TextField("Magical Damage", ch.currentProperty.mDamage.ToString());
        EditorGUILayout.TextField("Physical Resist", ch.currentProperty.pResist.ToString());
        EditorGUILayout.TextField("Magical Resist", ch.currentProperty.mResist.ToString());
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Strength", ch.currentProperty.strength.ToString());
        EditorGUILayout.TextField("Dexterity", ch.currentProperty.aligity.ToString());
        EditorGUILayout.TextField("Intellect", ch.currentProperty.intellect.ToString());
        EditorGUILayout.TextField("Technology", ch.currentProperty.technology.ToString());
        GUILayout.EndHorizontal();

        EditorGUILayout.TextField("Dodge", ch.currentProperty.dodge.ToString());

        EditorGUI.EndDisabledGroup();
    }

    /// <summary>
    /// Initialize editor window
    /// </summary>
    private void Init(){
        characters.Clear();

        if(Data.player != null)
            characters.Add("Player", Data.player);
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Character")){
            Character ch = go.GetComponent<SmallMapObject>().character;
            characters.Add(go.name, ch);
        }
    }
}
