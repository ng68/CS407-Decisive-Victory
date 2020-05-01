using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalArcher : Units
{
	private int attackCounter = 0;

	override public void Start(){
		type = "Archer";
		specialAbility = "Does extra damage every 4th attack";
		base.Start();
	}

	override public void Attack() {
		Vector3 check = target.transform.position;
		AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.clip = attackSound;
        audio.enabled = true;
        audio.Play();
        if (check.x < transform.position.x && facingRight) {
            mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
            facingRight = false;
        }

        if (check.x > transform.position.x && !facingRight) {
            mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
            facingRight = true;
        }

		float adjustedAttack;

        animator.SetInteger("state", 2);
		if (attackCounter >= 3) {
			adjustedAttack = attack*3;
			attackCounter = 0;
		}else {
			adjustedAttack = attack;
			attackCounter++;
		}
        
		targetScript.LoseHealth(gameObject, type, adjustedAttack, magical);
		//StartCoroutine(CatchUp());
	}
}
