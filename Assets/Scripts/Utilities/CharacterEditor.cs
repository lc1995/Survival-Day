/*******************************************
* Description
*
*
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

    private Dictionary<string, Character> characters = new Dictionary<string, Character>();
    private int selected = 0;

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
        EditorGUILayout.TextField("Hp", ch.currentProperty.hp.ToString());
        EditorGUILayout.TextField("Max HP", ch.finalProperty.hp.ToString());
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Hunger", ch.currentProperty.hunger.ToString());
        EditorGUILayout.TextField("Max Hunger", ch.finalProperty.hunger.ToString());
        GUILayout.EndHorizontal();
        EditorGUI.EndDisabledGroup();
    }

    private void Init(){
        characters.Clear();

        characters.Add("Player", Data.player);
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Character")){
            Character ch = go.GetComponent<SmallMapObject>().character;
            characters.Add(go.name, ch);
        }
    }
}
