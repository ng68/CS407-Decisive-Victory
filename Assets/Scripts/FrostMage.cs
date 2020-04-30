using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrostMage : Units
{
    // Start is called before the first frame update
    override public void Start(){
		type = "Frost Mage";
		specialAbility = "Slows target's movement speed.";
		base.Start();
	}

    override public void Attack() {
        targetScript.slowed = true;
        base.Attack();
    }
}
