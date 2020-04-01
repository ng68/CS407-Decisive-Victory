using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //for holding a ref to the textbox the unit has
    private GameObject priceTextBox;
    private Text priceText;
    //for holding a ref to the healthbar the unit has
    private GameObject healthBar;
    //for money interactions
    private bool isPurchasable = false;
    private bool isSellable = false;
    public int price = 0;

    void Start(){
        priceText = null;
        priceTextBox = null;
        Transform trans = this.transform;
        Transform childTrans = trans.Find("Canvas");

        if(childTrans != null){
            Transform transFinal = childTrans.Find("Price");
            Transform transHealth = childTrans.Find("HealthBG");
            //just to make sure none of this is causing errors.
            if(transFinal != null){//good practice, and I have a feeling sometimes there could be errors with this so I'll be safe.
                priceTextBox = transFinal.gameObject;
                priceText = priceTextBox.GetComponent<Text>();
                //move to update if any units are going to have "changing prices"
                priceText.text = price.ToString();
            }else{
                Debug.Log("Couldn't find Price Text:");
                Debug.Log(this.name);
            }

            if(transHealth != null){
                healthBar = transHealth.gameObject;
            }else{
                Debug.Log("Couldn't find Health Bar:");
                Debug.Log(this.name);
            }
        }else{
            Debug.Log("Couldn't find Price Canvas:");
            Debug.Log(this.name);
        }

        //finds the gameHandler
        gameHandler = GameObject.Find("GameHandler");
		//checks if the tag is enemy or ally and sets isMovable accordingly. 
		//If neither we likely shouldnt be able to move it too.
		if(this.tag == "Enemy"){
			this.isMovable = false;
            //these are just to make sure these things are set correctly. 
            this.isPurchasable = false;
            this.isSellable = false;
            //Yes this next line causes an error if the "priceText" doesn't exist, however that does not break the game functioning at all.
            healthBar.active = true;
            priceTextBox.active = false;
		}else if(this.tag == "Ally"){
            //sets the "to sell position", only matters for units we can buy/sell
            sellPosX = this.transform.localPosition.x;
            sellPosY = this.transform.localPosition.y;
            //these are just to make sure things are set correctly. IF FRIENDLY UNITS SPAWN(like start the level) ON BOARD THIS LOGIC DOES NOT WORK
            this.isPurchasable = true;
            this.isSellable = false;
            this.isMovable = true;
            healthBar.active = false;
            priceTextBox.active = true;
		}else{
			this.isMovable = false;
		}
	}
    // Update is called once per frame
    void Update(){
    	if(isBeingHeld == true){
    		Vector3 mousePos;
    		mousePos = Input.mousePosition;
    		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    		this.gameObject.transform.localPosition = new Vector3(mousePos.x - tempPosX, mousePos.y - tempPosY, 0);
    	}
        if(isPurchasable == true && Time.timeScale != 0 ){
            gameObject.active = false;            
        }
    }
    //might be needed to detect victory conditions
    public bool CanAct(){
        return isSellable;
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
                        priceTextBox.active = false;
                        healthBar.active = true;
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
                    priceTextBox.active = true;
                    healthBar.active = false;
                }else{
                    this.gameObject.transform.localPosition = new Vector3(ogPosX, ogPosY, 0);
                }
    		}

    	}
    	isBeingHeld = false;
    }

}
