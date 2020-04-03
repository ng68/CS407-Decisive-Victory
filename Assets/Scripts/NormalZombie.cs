using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalZombie : Units
{
    private bool died = false;
    private bool zombieSlayer = false;
    
    override public void Start(){
        type = "Zombie";
        base.Start();
    }

    override public void LoseHealth(GameObject attacker, string attackerType, float damage, bool typeMagical)
    {
        float dmgTaken;
        if (attackerType.Equals("Knight")) {
            zombieSlayer = true;
        }
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
            if (died || zombieSlayer) {
                //transform.gameObject.tag = "Dead";
                //Play death animation
                if (zombieSlayer) {
                    LogText.GetComponent<Text>().text += '\n' + "Zombieslayer debuff prevented " + gameObject.name + " from reviving";
                }
                LogText.GetComponent<Text>().text += '\n' + gameObject.name + " was killed by " + attacker.name;
                transform.gameObject.SetActive(false);
            }else {
                health = maxHealth / 2;
                magicalArmorPercent = 0;
                physicalArmorPercent = 0;
                attack = attack / 2;
                healthBar.fillAmount = health/maxHealth;
                died = true;
                LogText.GetComponent<Text>().text += '\n' + gameObject.name + " revived";
            }
        }
    }
}
