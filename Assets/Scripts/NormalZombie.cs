using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalZombie : Units
{
    private bool died = false;
    
    override public void Start(){
        type = "Zombie";
        specialAbility = "If this unit dies, it revives with quarter health, increased attack, and decreased defense";
        base.Start();
    }

    override public void LoseHealth(GameObject attacker, string attackerType, float damage, bool typeMagical)
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
<<<<<<< HEAD
                GameObject effectAudio = GameObject.Find("EffectsSource");
                AudioSource audio = effectAudio.GetComponent<AudioSource>();
                audio.clip = deathSound;
                audio.enabled = true;
                audio.Play();
                if (zombieSlayer) {
                    LogText.GetComponent<Text>().text += '\n' + "Zombieslayer debuff prevented " + gameObject.name + " from reviving";
                }
=======
>>>>>>> e15dcd3dc1139838ce3a73cb952f131381a20ffa
                LogText.GetComponent<Text>().text += '\n' + gameObject.name + " was killed by " + attacker.name;
                transform.gameObject.SetActive(false);
            }else {
                health = maxHealth / 4;
                magicalArmorPercent = 0;
                physicalArmorPercent = 0;
                attack = attack * 2;
                healthBar.fillAmount = health/maxHealth;
                died = true;
                LogText.GetComponent<Text>().text += '\n' + gameObject.name + " revived";
            }
        }
    }
}
