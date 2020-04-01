using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombie : Units
{
    private bool died = false;

    override public void LoseHealth(GameObject attacker, float damage, bool typeMagical)
    {
        float dmgTaken;
        if (typeMagical) {
            dmgTaken = damage * (1 - magicalArmorPercent);
            health = health - dmgTaken;
        }else {
            dmgTaken = damage * (1 - physicalArmorPercent);
            health = health - dmgTaken;   
        }
        
        healthBar.fillAmount = health/maxHealth;
        //Debug.Log(attacker.name + " is attacking " + gameObject.name + " for " + dmgTaken + " damage!");
        if (health <= 0)
        {
            if (died) {
                //transform.gameObject.tag = "Dead";
                //Play death animation
                transform.gameObject.SetActive(false);
            }else {
                health = maxHealth / 2;
                magicalArmorPercent = 0;
                physicalArmorPercent = 0;
                attack = attack / 2;
                healthBar.fillAmount = health/maxHealth;
                died = true;
            }
        }
    }
}
