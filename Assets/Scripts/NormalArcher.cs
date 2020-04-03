using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalArcher : Units
{
	override public void Start(){
		type = "Archer";
		base.Start();
	}
}
