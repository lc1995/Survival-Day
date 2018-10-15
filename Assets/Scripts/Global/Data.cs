using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data{

	public static Character player;
	public static bool inBigMap;

	public static Dictionary<int, Attack> AllAttacks = new Dictionary<int, Attack>();
	public static Dictionary<int, Defend> AllDefends = new Dictionary<int, Defend>();
	public static Dictionary<int, Result> AllResults = new Dictionary<int, Result>();

	public static Dictionary<ActionType, List<int>> AllDefendsByType = new Dictionary<ActionType, List<int>>();

}
