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
    public AudioClip attackSound;
    public AudioClip deathSound;
    
    [HideInInspector]
    public string specialAbility = "None";
    [HideInInspector]
    public float maxHealth;
    [HideInInspector]
    public GameObject LogText;
    [HideInInspector]
    public string type = "Empty";
    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public SpriteRenderer mySpriteRenderer;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Units targetScript;
    [HideInInspector]
    public bool slowed = false;
    [HideInInspector]
    public bool haste = false;

    private bool stunned = false;
    private GameObject[] oppUnits;
    private bool takeTime = true;
    private float speed = 100.0f;
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
    public virtual void Start()
    {
        maxHealth = health;
        LogText = GameObject.Find("/UIController/Log_Canvas/GameLog_Canvas/Log Field/Log Text");
        StartCoroutine(PauseTime());
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        
        animator = GetComponent<Animator>();        
    }

    public virtual void LoseHealth(GameObject attacker, string attackerType, float damage, bool typeMagical)
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
            GameObject effectAudio = GameObject.Find("EffectsSource");
            AudioSource audio = effectAudio.GetComponent<AudioSource>();
            audio.clip = deathSound;
            audio.enabled = true;
            audio.Play();
            
            //Play death animation
            LogText.GetComponent<Text>().text += '\n' + gameObject.name + " was killed by " + attacker.name;
            //transform.gameObject.tag = "Dead";
            transform.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
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

        target = FindClosest();
        if (target == null)
        {
            animator.SetInteger("state", 0);
            //Debug.Log("Game over");
            return;
        }
        targetScript = target.GetComponent<Units>();
        if (!takeTime && !stunned)
        {
            AttemptAction();
        }
    }

    public virtual void Attack()
    {
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

        animator.SetInteger("state", 2);
        targetScript.LoseHealth(gameObject, type, attack, magical);
        //StartCoroutine(CatchUp());
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
        
        float xDif = Mathf.Abs(target.transform.position.x - transform.position.x);
        float yDif = Mathf.Abs(target.transform.position.y - transform.position.y);
        float hypotenuse = Mathf.Sqrt((xDif*xDif) + (yDif*yDif));
        if (hypotenuse <= range*5)
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

    public IEnumerator Stun(float stunTime) {
        stunned = true;
        yield return new WaitForSeconds(stunTime);
        stunned = false;
    }

    IEnumerator MoveTime()
    {
        takeTime = true;
        float adjMoveTime = moveTime;
        if (slowed) {
             adjMoveTime = adjMoveTime * 1.5f;
        } 
        
        if (haste) {
            adjMoveTime = adjMoveTime * .5f;
        }

        yield return new WaitForSeconds(adjMoveTime);
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
        float adjAttackSpeed = attackSpeed;
        
        if (haste) {
            adjAttackSpeed = adjAttackSpeed * .5f;
        }

        yield return new WaitForSeconds(adjAttackSpeed);
        attackingPause = false;
    }

    // public IEnumerator CatchUp()
    // {
    //     catchUp = true;
    //     yield return new WaitForSeconds(.1f);
    //     catchUp = false;
    // }
}
