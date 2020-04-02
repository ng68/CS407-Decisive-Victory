using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Units : MonoBehaviour
{
    public float health = 100;
    public float attack = 1;
    public float attackSpeed = 1;
    public float range = 1;
    public bool magical;
    public float physicalArmorPercent;
    public float magicalArmorPercent;
    //public Button pauseButton;
    //public Button startButton;
    public float moveTime = 0.2f;
    public Image healthBar;
    
    
    [HideInInspector]
    public float maxHealth;
    public GameObject LogText;

    private SpriteRenderer mySpriteRenderer;
    private Animator animator;
    private GameObject target;
    private GameObject[] oppUnits;
    private Units targetScript;
    private bool takeTime = true;
    private float speed = 100.0f;
    private bool facingRight = true;
    private bool attackingPause = false;
    private bool movePause = false;

    public GameObject FindClosest()
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in oppUnits)
        {
            if (!go.activeSelf)
                continue;
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        LogText = GameObject.Find("/UIController/Log_Canvas/GameLog_Canvas/Log Field/Log Text");
        StartCoroutine(PauseTime());
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        
        animator = GetComponent<Animator>();
        if (transform.gameObject.tag == "Enemy")
        {
            //ally = false;
            oppUnits = GameObject.FindGameObjectsWithTag("Ally");
        }
        else if (transform.gameObject.tag == "Ally")
        {
            //ally = true;
            oppUnits = GameObject.FindGameObjectsWithTag("Enemy");
        }
        else
            Debug.Log("Need to assign ally or enemy");
          
        
    }

    public virtual void LoseHealth(GameObject attacker, float damage, bool typeMagical)
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
            //transform.gameObject.tag = "Dead";
            //Play death animation
            LogText.GetComponent<Text>().text += '\n' + gameObject.name + " was killed by " + attacker.name;
            transform.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        target = FindClosest();
        if (target == null)
        {
            animator.SetInteger("state", 0);
            //Debug.Log("Game over");
            return;
        }
        targetScript = target.GetComponent<Units>();
        if (!takeTime)
        {
            AttemptAction();
        }
    }

    public virtual void Attack()
    {
        Vector3 check = target.transform.position;
        if (check.x < transform.position.x && facingRight) {
            mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
            facingRight = false;
        }

        if (check.x > transform.position.x && !facingRight) {
            mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
            facingRight = true;
        }

        animator.SetInteger("state", 2);
        targetScript.LoseHealth(gameObject, attack, magical);
    }

    public virtual void Move()
    {
        Vector3 check = target.transform.position;
        if (check.x < transform.position.x && facingRight) {
            mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
            facingRight = false;
        }

        if (check.x > transform.position.x && !facingRight) {
             mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
             facingRight = true;
        }

        animator.SetInteger("state", 1);
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
    }

    void AttemptAction ()
    {
        //If in range to attack
        if (Mathf.Abs(target.transform.position.x - transform.position.x) <= range*5 && Mathf.Abs(target.transform.position.y - transform.position.y) <= range*5)
        {
            if (!attackingPause) {
                Attack();
                StartCoroutine(AttackTime());
            }  
        }else
        {
            if (!movePause) {
                Move();
                StartCoroutine(MoveTime());
            }  
        }
    }

    IEnumerator MoveTime()
    {
        takeTime = true;
        yield return new WaitForSeconds(moveTime);
        takeTime = false;
    }

    IEnumerator PauseTime()
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSeconds(.1f);
        takeTime = false;
    }

    IEnumerator AttackTime()
    {
        attackingPause = true;
        yield return new WaitForSeconds(attackSpeed);
        attackingPause = false;
    }
}
