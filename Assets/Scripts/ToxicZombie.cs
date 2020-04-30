using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToxicZombie : Units
{
    // Start is called before the first frame update
    override public void Start(){
		type = "Magic Zombie";
		specialAbility = "Gets a huge boost of movement speed and attack speed for a limited time at the beginning of the battle.";
		base.Start();
	}
}
