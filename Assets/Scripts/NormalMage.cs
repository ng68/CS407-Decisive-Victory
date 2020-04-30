using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalMage : Units
{
	override public void Start(){
		type = "Mage";
		specialAbility = "Every successful attack against an enemy unit boosts this units attack permanently.";
		base.Start();
	}

	override public void Attack() {
		base.Attack();
		attack += 5;
	}
}
