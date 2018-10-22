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
        if(!Application.isEditor)
            return;
        if(characters.Keys.Count == 0)
            return;

        string[] names = characters.Keys.ToArray();
        selected = EditorGUILayout.Popup(selected, names);
        Character ch = characters[names[selected]];

        if(ch == null)
            return;

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.TextField("Name", ch.name);

        GUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Hp", ch.finalProperty.hp.ToString() + " / " + ch.finalProperty.hpMax.ToString());
        EditorGUILayout.TextField("Hunger", ch.finalProperty.hunger.ToString() + " / " + ch.finalProperty.hungerMax.ToString());
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Physical Damage", ch.finalProperty.pDamage.ToString());
        EditorGUILayout.TextField("Magical Damage", ch.finalProperty.mDamage.ToString());
        EditorGUILayout.TextField("Physical Resist", ch.finalProperty.pResist.ToString());
        EditorGUILayout.TextField("Magical Resist", ch.finalProperty.mResist.ToString());
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.TextField("Strength", ch.finalProperty.strength.ToString());
        EditorGUILayout.TextField("Dexterity", ch.finalProperty.aligity.ToString());
        EditorGUILayout.TextField("Intellect", ch.finalProperty.intellect.ToString());
        EditorGUILayout.TextField("Technology", ch.finalProperty.technology.ToString());
        GUILayout.EndHorizontal();

        EditorGUILayout.TextField("Dodge", ch.finalProperty.dodge.ToString());

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
