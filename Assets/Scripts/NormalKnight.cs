using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalKnight : Units
{
	private bool enraged = false;

	override public void Start(){
		type = "Knight";
		specialAbility = "When this unit gets to 1/3 health, it enrages tripling its attack speed and move speed";
		base.Start();
	}  

	override public void LoseHealth(GameObject attacker, string attackerType, float damage, bool typeMagical){
		if (!enraged && health <= maxHealth/3) {
			enraged = true;
			moveTime = moveTime/3;
			attackSpeed = attackSpeed/3;
			LogText.GetComponent<Text>().text += '\n' + gameObject.name + " has become enraged";
		}
		base.LoseHealth(attacker, attackerType, damage, typeMagical);
	}
}
