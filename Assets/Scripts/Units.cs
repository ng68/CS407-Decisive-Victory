using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    public int health;
    public int attack;
    public int range;
    //public float moveTime = 0.1f;

    //private Animator animator;
    private GameObject target;
    private GameObject[] oppUnits;
    private Units targetScript;
    private bool takeTime;
    private float speed = 100.0f;

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
        //animator = GetComponent<Animator>();
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

    void LoseHealth(int loss)
    {
        health -= loss;
        if (health <= 0)
        {
            //transform.gameObject.tag = "Dead";
            //Play death animation
            transform.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        target = FindClosest();
        if (target == null)
        {
            Debug.Log("Game over");
            return;
        }
        targetScript = target.GetComponent<Units>();
        if (!takeTime)
        {
            AttemptAction();
            StartCoroutine(ReduceTime());
        }
    }

    void Attack()
    {
        targetScript.LoseHealth(attack);
    }

    void Move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
    }

    void AttemptAction ()
    {
        //If in range to attack
        if (Mathf.Abs(target.transform.position.x - transform.position.x) <= range*10 && Mathf.Abs(target.transform.position.y - transform.position.y) <= range*10)
        {
            Attack();
        }else
        {
            Move();
        }
    }

    IEnumerator ReduceTime()
    {
        takeTime = true;
        yield return new WaitForSeconds(.05f);
        takeTime = false;
    }
}
