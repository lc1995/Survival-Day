using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ExtensionMethods{

	public static string ToBattleString(this string str, Character attacker, Character defender){
		return str.Replace("$1", attacker.name).Replace("$2", defender.name);
	}

	public static void AddText(this Text text, string str, ScrollRect sr){
		text.text += str;

		Canvas.ForceUpdateCanvases();
		// sr.verticalScrollbar.value = 0f;
		sr.verticalNormalizedPosition = 0f;
		Canvas.ForceUpdateCanvases();
	}

}
