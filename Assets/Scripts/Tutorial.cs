using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private GameObject uiController;
    private bool spoken = false;
    private bool takeTime = true;
    //this is a lazy way to do it, but its only for the tutorial and works and once it does its path once, will never get in the way again.
    private bool takeTimeOne = true;
    private bool takeTimeTwo = true;
    private bool takeTimeThree = true;
    private bool takeTimeFour = true;
    private bool takeTimeFive = true;    
    //yes I had to go this far...
    private bool firstTime = true;
    private bool secondTime = true;
    private bool thirdTime = true;
    private bool fourthTime = true;
    private bool fifthTime = true;    

    void Start()
    {
    	spoken = false;
    	takeTime = true;
    	uiController = GameObject.Find("UIController");
    	string temp = "... and also welcome to the tutorial!\nThe unit(s) that are currently on the board when a level is loaded are \nthe enemy units you need to defeat all of to advance to the next level.";
        uiController.GetComponent<GameUI>().AppendLog(temp);
    }

    void Update(){
    	//Time for the big loops...
    	if(spoken == false){
    		if(takeTime == false){
				if(firstTime == true){
					string temp = "Clicking on friendly units in the shop or on the board will display (scrollable) information about them in the \nunit description panel on the left side of the screen.";
        			uiController.GetComponent<GameUI>().AppendLog(temp);
        			firstTime = false;
        		}
        		if(takeTimeTwo == false){
        			if(secondTime == true){
        				string temp = "To purchase a unit, click and drag any of the units in the shop to the board in any position you desire. \nTo sell, simply drag any friendly unit off of the board.";
        				uiController.GetComponent<GameUI>().AppendLog(temp);
        				secondTime = false;
        			}
       				if(takeTimeThree == false){
       					if(thirdTime == true){
       						string temp = "As you may have noticed, purchasing and selling units appropriately updates your gold amount\n in the upper right hand corner of the screen.";
       						uiController.GetComponent<GameUI>().AppendLog(temp);
       						thirdTime = false;
       					}
       					if(takeTimeFour == false){
       						if(fourthTime == true){
       							string temp = "Go ahead and experiment with different units for this simple tutorial, and after any victory or loss, \nyou always have the option to retry the current level if you would like to test more before heading into Level 1.";
       							uiController.GetComponent<GameUI>().AppendLog(temp);
       							fourthTime = false;
       						}
       						if(takeTimeFive == false){
       							if(fifthTime == true){
									string temp = "With these things in mind, which units you purchase and where you place them will be vital \nin achieving a DECISIVE VICTORY. Have fun!";
        							uiController.GetComponent<GameUI>().AppendLog(temp);
        							spoken = true;
        							fifthTime = false;
        						}
       						}else{
       							StartCoroutine(WaitForRealSecondsFive());
       						}
        				}else{
        					StartCoroutine(WaitForRealSecondsFour());
        				}
        			}else{
        				StartCoroutine(WaitForRealSecondsThree());
       				}
       			}else{
       				StartCoroutine(WaitForRealSecondsTwo());
       			}
        	}else{
        		 StartCoroutine(WaitForRealSeconds());
        	}
        } 

    }


    IEnumerator WaitForRealSeconds (){
    	float startTime = Time.realtimeSinceStartup;
    	float seconds = 10;
    	while(Time.realtimeSinceStartup - startTime < seconds){
    		yield return null;
    	}
    	takeTime = false;
    }

    IEnumerator WaitForRealSecondsTwo (){
       	float startTime = Time.realtimeSinceStartup;
       	float seconds = 8;
    	while(Time.realtimeSinceStartup - startTime < seconds){
    		yield return null;
    	}
    	takeTimeTwo = false;
    }

    IEnumerator WaitForRealSecondsThree (){
    	float startTime = Time.realtimeSinceStartup;
    	float seconds = 8;
    	while(Time.realtimeSinceStartup - startTime < seconds){
    		yield return null;
    	}
    	takeTimeThree = false;
    }

    IEnumerator WaitForRealSecondsFour (){
    	float startTime = Time.realtimeSinceStartup;
    	float seconds = 8;
    	while(Time.realtimeSinceStartup - startTime < seconds){
    		yield return null;
    	}
    	takeTimeFour = false;
    }

    IEnumerator WaitForRealSecondsFive (){
    	float startTime = Time.realtimeSinceStartup;
    	float seconds = 8;
    	while(Time.realtimeSinceStartup - startTime < seconds){
    		yield return null;
    	}
    	takeTimeFive = false;
    }
}
