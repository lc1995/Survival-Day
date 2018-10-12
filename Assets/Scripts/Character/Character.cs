/*******************************************
* Description
* This class is base class for all characters(including player, enemy, npc and etc) in the scene.
* There is also a CharProperty struct which describes the property(attribute) of the character.
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character{

	// ------ Public Variables ------
	public bool isPlayer;
	public string name;

	public CharProperty originalProperty;	// Initial property
	public CharProperty finalProperty;	// Final property considering equipments and labels
	public CharProperty currentProperty;	// Current property

	public Equipment weapon;
	public Equipment armor;
	public Equipment accessory;

	public List<Label> labels;

	public Dictionary<Inventory, int> inventories;

	public List<AttackAction> attackPool = new List<AttackAction>();
	public Dictionary<ActionType, List<DefendAction>> defendPool = new Dictionary<ActionType, List<DefendAction>>();
	
	// ------ Shared Variables ------
	
	// ------ Private Variables ------
	
	
	// ------ Required Components ------
	
	
	// ------ Public Functions ------
	/// <summary>
	/// Initialize a character
	/// </summary>
	/// <param name="n">Name</param>
	/// <param name="ip">Is player</param>
	public Character(string n, bool ip=false){
		name = n;
		isPlayer = ip;

		// Initialization
		// Property
		originalProperty = CharProperty.standard;
		UpdateFinalProperty();
		currentProperty = finalProperty;
		// Label
		labels = new List<Label>();
		// Inventory
		inventories = new Dictionary<Inventory, int>();
	}

	/// <summary>
	/// Initialize a character by CharProperty
	/// </summary>
	/// <param name="n">Name</param>
	/// <param name="cp">Character property</param>
	/// <param name="ip">Is player</param>
	public Character(string n, CharProperty cp, bool ip=false){
		name = n;
		isPlayer = ip;

		originalProperty = cp;
		UpdateFinalProperty();
		currentProperty = finalProperty;
	}

	/// <summary>
	/// Character gets damage both physical and magical
	/// </summary>
	/// <param name="pDamage">Physical damage</param>
	/// <param name="mDamage">Magical damage</param>
	public void GetDamage(float pDamage, float mDamage){
		currentProperty.hp -= pDamage * 100f / (100f + currentProperty.pResist);
		currentProperty.hp -= mDamage * 100f / (100f + currentProperty.mResist);
	}

	public void Reset(){
		currentProperty = finalProperty;
	}

	/// <summary>
	/// Update final property calculated by equipments and labels
	/// </summary>
	public void UpdateFinalProperty(){
		// Calculate property by equipments and labels
		// ...
		finalProperty = originalProperty;

		// Calculate 2-level property by 1-level property
		finalProperty.dodge += finalProperty.dexterity * 1f;
		finalProperty.pResist += finalProperty.strength * 1f;
		finalProperty.mResist += finalProperty.intellect * 1f;
	}
}

public struct CharProperty{

	public float hp;
	public float hunger;

	public float strength;
	public float dexterity;
	public float intellect;
	public float technology;

	public float dodge;
	public float pResist;
	public float mResist;
	public float pDamage;
	public float mDamage;

	public CharProperty(float hp, float hunger, float str, float dex, float inte, float tech){
		this.hp = hp;
		this.hunger = hunger;

		this.strength = str;
		this.dexterity = dex;
		this.intellect = inte;
		this.technology = tech;

		this.dodge = 0f;
		this.pResist = 0f;
		this.mResist = 0f;
		this.pDamage = 0f;
		this.mDamage = 0f;
	}

	public static CharProperty zero{
		private set{}
		get{
			return new CharProperty(0f, 0f, 0f, 0f, 0f, 0f);
		}
	}

	public static CharProperty standard{
		private set{}
		get{
			return new CharProperty(200f, 100f, 10f, 10f, 10f, 10f);
		}
	}

}