/*******************************************
* Description
* This class is base class for all characters(including player, enemy, npc and etc) in the scene.
* There is also a CharProperty struct which describes the property(attribute) of the character.
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {

	// ------ Public Variables ------
	public bool isPlayer;
	public string name;
	public CharProperty originalProperty;
	public CharProperty currentProperty;
	public List<AttackAction> attackPool = new List<AttackAction>();
	public Dictionary<ActionType, List<DefendAction>> defendPool = new Dictionary<ActionType, List<DefendAction>>();
	
	// ------ Shared Variables ------
	
	
	// ------ Private Variables ------
	
	
	// ------ Required Components ------
	
	
	// ------ Public Functions ------

	public Character(string n, bool ip=false){
		name = n;
		originalProperty = CharProperty.standard;
		currentProperty = originalProperty;
		isPlayer = ip;
	}

	public Character(string n, CharProperty cp, bool ip=false){
		name = n;
		originalProperty = cp;
		currentProperty = originalProperty;
		isPlayer = ip;
	}

	public void GetDamage(float damage){
		currentProperty.hp -= damage;
	}

	public void Reset(){
		currentProperty = originalProperty;
	}
}

public struct CharProperty{

	public float hp;
	public float hunger;
	public float strength;
	public float speed;

	public CharProperty(float hp, float strength, float speed, float hunger){
		this.hp = hp;
		this.strength = strength;
		this.speed = speed;
		this.hunger = hunger;
	}

	public static CharProperty zero{
		private set{}
		get{
			return new CharProperty(0f, 0f, 0f, 0f);
		}
	}

	public static CharProperty standard{
		private set{}
		get{
			return new CharProperty(200f, 10f, 10f, 100f);
		}
	}

}