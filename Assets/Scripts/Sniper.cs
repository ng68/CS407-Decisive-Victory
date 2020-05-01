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

    override public void Attack() {
        Vector3 check = target.transform.position;
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
        float xDif = Mathf.Abs(target.transform.position.x - transform.position.x);
        float yDif = Mathf.Abs(target.transform.position.y - transform.position.y);
        float hypotenuse = Mathf.Sqrt((xDif*xDif) + (yDif*yDif));
        float averageDistance = hypotenuse/(range*5);
		adjustedAttack = attack * (1+averageDistance);
        
		targetScript.LoseHealth(gameObject, type, adjustedAttack, magical);
        //StartCoroutine(CatchUp());
    }
}
