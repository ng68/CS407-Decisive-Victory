﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableUnit : MonoBehaviour
{
    //for putting sold units back where they came from
    private float sellPosX;
    private float sellPosY;
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
    //for money interactions
    private bool isPurchasable = false;
    private bool isSellable = false;
    public int price = 0;

    void Start(){
		//checks if the tag is enemy or ally and sets isMovable accordingly. 
		//If neither we likely shouldnt be able to move it too.
		if(this.tag == "Enemy"){
			this.isMovable = false;
            //these are just to make sure these things are set correctly. 
            this.isPurchasable = false;
            this.isSellable = false;
		}else if(this.tag == "Ally"){
            //sets the "to sell position", only matters for units we can buy/sell
            sellPosX = this.transform.localPosition.x;
            sellPosY = this.transform.localPosition.y;
            //these are just to make sure things are set correctly. IF FRIENDLY UNITS SPAWN(like start the level) ON BOARD THIS LOGIC DOES NOT WORK
            this.isPurchasable = true;
            this.isSellable = false;
			
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
    		if(check == true){//we dropped the unit in a legal position
                //check if unit is "to be purchased"
                if (isPurchasable == true) {
                    if (gameHandler.GetComponent<Currency>().changeGold(price) >= 0){
                        //we have enough money to buy the object
                        isPurchasable = false;
                        isSellable = true;
                        this.gameObject.transform.localPosition = snapPos;
                    }
                    else{
                        //we don't have enough money to buy the object
                        this.gameObject.transform.localPosition = new Vector3(ogPosX, ogPosY, 0);
                    }
                }
    		}else{
    			//if we dropped the unit in an illegal position
                //check if its sellable
                if(isSellable == true){
                    int newPrice = price * -1;//invert price
                    gameHandler.GetComponent<Currency>().changeGold(newPrice);
                    //now we need to place the gameObject back where it came from...
                    this.gameObject.transform.localPosition = new Vector3(sellPosX, sellPosY, 0);
                    isSellable = false;
                    isPurchasable = true;
                }else{
                    this.gameObject.transform.localPosition = new Vector3(ogPosX, ogPosY, 0);
                }
    		}

    	}
    	isBeingHeld = false;
    }

}
