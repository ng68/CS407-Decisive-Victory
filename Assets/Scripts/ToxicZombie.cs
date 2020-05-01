using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToxicZombie : Units
{

    // Start is called before the first frame update
    override public void Start(){
		haste = true;
		type = "Toxic Zombie";
		specialAbility = "Gets a huge boost of movement speed and attack speed for a limited time at the beginning of the battle.";
		base.Start();
	}

	override public void Update(){
		if (haste) {
			StartCoroutine(BuffTime());
		}
		base.Update();
	}

	IEnumerator BuffTime()
    {
        yield return new WaitForSeconds(5f);
        haste = false;
    }
}
