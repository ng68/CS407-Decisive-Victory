using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sniper : Units
{
    // Start is called before the first frame update
    override public void Start(){
		type = "Sniper";
		specialAbility = "Damage scales off of how far away its target is.";
		base.Start();
	}
}
