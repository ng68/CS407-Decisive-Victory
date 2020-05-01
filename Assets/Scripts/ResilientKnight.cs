using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResilientKnight : Units
{
	private bool stun = true;

    // Start is called before the first frame update
    override public void Start(){
		type = "Resilient Knight";
		specialAbility = "Stuns first unit struck for 5 seconds.";
		base.Start();
	}

	override public void Attack() {
		if (stun) {
			StartCoroutine(targetScript.Stun(5f));
			stun = false;
		}
		base.Attack();
	}

}
