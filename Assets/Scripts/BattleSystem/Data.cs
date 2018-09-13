using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data{

	public static List<AttackAction> AllAttacks = new List<AttackAction>();
	public static List<DefendAction> AllDefends = new List<DefendAction>();
	public static Dictionary<ActionType, List<DefendAction>> AllDefendsByType = new Dictionary<ActionType, List<DefendAction>>();
}
