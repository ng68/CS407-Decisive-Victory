using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameHandler : MonoBehaviour
{
    private double highestAlly = 0;
    private GameObject winScreen;
    private GameObject loseScreen;
    private GameObject[] allyUnits;
    private GameObject[] enemyUnits;
    private bool allyDeadCheck;
    private bool enemyDeadCheck;
    private bool gameEnd = false;
    private bool takeTime = true;
    private bool trainingRoom = false;
    
    // Start is called before the first frame update
    void Start()
    {
        winScreen = GameObject.Find("Win_Canvas").transform.GetChild(0).GetChild(0).gameObject;
        loseScreen = GameObject.Find("Lose_Canvas").transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnd) {
            allyDeadCheck = true;
            enemyDeadCheck = true;

            allyUnits = GameObject.FindGameObjectsWithTag("Ally");
            enemyUnits = GameObject.FindGameObjectsWithTag("Enemy");
            double currentallies = 0;
            foreach (GameObject go in allyUnits) {
                if (go.activeSelf) {
                    allyDeadCheck = false;
                    currentallies++;
                }
            }
            if (currentallies > highestAlly)
            {
                highestAlly = currentallies;
            }

            foreach (GameObject go in enemyUnits) {
                if (go.activeSelf) {
                    enemyDeadCheck = false;
                }
            }

            if (allyDeadCheck) {
                //gameEnd = true;
                if (!takeTime) {
                    Time.timeScale = 0.0f;
                    loseScreen.SetActive(true);
                }else {
                    StartCoroutine(DelayTime());
                } 
            }

            if (enemyDeadCheck) {
                //gameEnd = true;
                if (!takeTime) {
                    if(trainingRoom == true){
                        winScreen.SetActive(true);
                        double finalscore = (currentallies / highestAlly) * 10;
                        int finalstring = Convert.ToInt32(finalscore);
                        string toChange = "Your Score: " + finalstring;
                        GameObject.Find("ScoreText").GetComponentInChildren<Text>().text = toChange;
                        //I don't believe score is done, but basically the training room version wont save score for anything (since its a sandbox level)
                        //PlayerPrefs.SetInt("score", finalstring);
                    }else{
                        winScreen.SetActive(true);
                        double finalscore = (currentallies / highestAlly) * 10;
                        int finalstring = Convert.ToInt32(finalscore);
                        string toChange = "Your Score: " + finalstring;
                        GameObject.Find("ScoreText").GetComponentInChildren<Text>().text = toChange;
                        int currProfile = -1;
                        for(int i = 0; i < 2; i++)
                        {
                            if (string.Equals(Globals.saves[i].charname, Globals.currUser))
                            {
                                currProfile = i;
                            }
                        }
                        if (currProfile < 0)
                        {
                            Debug.Log("Error: Profile not found.");
                        }
                        else
                        {
                            if (Globals.saves[currProfile].scores[Globals.currLevel] < finalstring)
                            {
                                Globals.saves[currProfile].scores[Globals.currLevel] = finalstring;
                            }
                            int scoreSum = 0;
                            Debug.Log("Sum of Scores before change: " + Globals.saves[currProfile].sumofScores);
                            for (int i = 0; i < 10; i++)
                            {
                                scoreSum = scoreSum + Globals.saves[currProfile].scores[i];
                            }
                            Globals.saves[currProfile].sumofScores = scoreSum;
                            //Debug.Log("Sum of Scores after change: " + Globals.saves[currProfile].sumofScores);
                            //PlayerPrefs.SetInt("score", finalstring);
                        }

                    }

                }
                else {
                    StartCoroutine(DelayTime());
                }   
            }  
        }
    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(3);
        takeTime = false;
    }

    public void ActivateTrainingRoom(){
        trainingRoom = true;
    }
    
}
