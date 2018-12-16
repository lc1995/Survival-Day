/*******************************************
* Description
* This script is responsible for parsing inequality string.
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class InequalityParse{

    // ------ Public Variables ------

    // ------ Shared Variables ------

    // ------ Private Variables ------

    // ------ Required Components ------


    // ------ Public Functions ------
    public static bool ConditionParse(string str){
        bool result = true;

        string[] separatingChars = {"&&"};
        string[] subStrs = str.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);

        foreach(string subStr in subStrs){
            result &= BaseCParse(subStr);
        }

        return result;
    }

    public static CharProperty EffectParse(string str, CharProperty cp){
        string pattern = @"E(\d+)([\+\-\*/])([0-9]*(?:\.[0-9]*)?)";
        foreach(Match m in Regex.Matches(str, pattern)){
            int effectType = int.Parse(m.Groups[1].Value);
            string operStr = m.Groups[2].Value;
            float effectValue = float.Parse(m.Groups[3].Value);

            switch(effectType){
                case 1:
                    cp.technology = BaseEParse(operStr, cp.technology, effectValue);
                    break;
                case 2:
                    cp.intellect = BaseEParse(operStr, cp.intellect, effectValue);
                    break;
                case 3:
                    cp.strength = BaseEParse(operStr, cp.strength, effectValue);
                    break;
                case 4:
                    cp.agility = BaseEParse(operStr, cp.agility, effectValue);
                    break;
                case 5:
                    cp.pResist = BaseEParse(operStr, cp.pResist, effectValue);
                    break;
                case 6:
                    cp.mResist = BaseEParse(operStr, cp.mResist, effectValue);
                    break;
            }
        }

        return cp;
    }    

    // ------ Private Functions ------
    private static bool BaseCParse(string str){
        bool result = false;

        string[] separatingChars = {" "};
        string[] values = str.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
        int leftValue = int.Parse(values[0]);
        int rightValue = int.Parse(values[2]);

        switch(values[1]){
            case "<":
                result = leftValue < rightValue;
                break;
            case "=":
                result = leftValue == rightValue;
                break;
            case ">":
                result = leftValue > rightValue;
                break;
            case "<=":
                result = leftValue <= rightValue;
                break;
            case ">=":
                result = leftValue >= rightValue;
                break;
        }

        return result;
    }

    private static float BaseEParse(string operStr, float v1, float v2){
        float result = 0f;
        switch(operStr){
            case "+":
                result = v1 + v2;
                break;
            case "-":
                result = v1 - v2;
                break;
            case "*":
                result = v1 * v2;
                break;
            case "/":
                result = v1 / v2;
                break;
        }

        return result;
    }

    
}
