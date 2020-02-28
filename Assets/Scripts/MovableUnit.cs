using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableUnit : MonoBehaviour
{
	//for putting the units back if their location is illegal
	private float ogPosX;
	private float ogPosY;
	//for making the dragging smoother
	private float tempPosX;
	private float tempPosY;
	private bool isBeingHeld = false;
	//maybe make private?
	public bool isMovable = false;
	//for holding a ref to the GameHandler gameobject
	private GameObject gameHandler;

	void Start(){
		//checks if the tag is enemy or ally and sets isMovable accordingly. 
		//If neither we likely shouldnt be able to move it too.
		if(this.tag == "Enemy"){
			this.isMovable = false;
		}else if(this.tag == "Ally"){
			this.isMovable = true;
		}else{
			this.isMovable = false;
		}
		//finds the gameHandler
		gameHandler = GameObject.Find("GameHandler");
	}
    // Update is called once per frame
    void Update(){
    	if(isBeingHeld == true){
    		Vector3 mousePos;
    		mousePos = Input.mousePosition;
    		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    		this.gameObject.transform.localPosition = new Vector3(mousePos.x - tempPosX, mousePos.y - tempPosY, 0);
    	}
        
    }

    private void OnMouseDown(){

    	//Makes sure its only on left click
    	if(Input.GetMouseButtonDown(0) && isMovable == true){
    		Vector3 mousePos;
    		mousePos = Input.mousePosition;
    		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    		//save original position
    		ogPosX = this.transform.localPosition.x;
    		ogPosY = this.transform.localPosition.y;
    		//this makes the game object not snap to the center of the mouse when held
    		tempPosX = mousePos.x - this.transform.localPosition.x;
    		tempPosY = mousePos.y - this.transform.localPosition.y;    		

    		isBeingHeld = true;
    	}

    }

    private void OnMouseUp(){
    	if(isBeingHeld == true){
    		//check where we are trying to drop the unit for legality
    		bool check = false;
    		Vector3 snapPos;
    		Vector3 mousePos;
    		mousePos = Input.mousePosition;
    		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    		gameHandler.GetComponent<GridHandler>().GridCheck(mousePos, out snapPos, out check);
    		if(check == true){
    			this.gameObject.transform.localPosition = snapPos;
    		}else{
    			//if we dropped the unit in an illegal position
    			this.gameObject.transform.localPosition = new Vector3(ogPosX, ogPosY, 0);
    		}

    	}
    	isBeingHeld = false;
    }

}
