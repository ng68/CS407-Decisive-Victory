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
    //for holding a ref to the UIController instead
    private GameObject uiController;
    //for holding a ref to the healthbar the unit has
    private GameObject healthBar;
    //for holding a ref to the UI text log
    //private GameObject textLog;
    //for money interactions
    public bool multiplePurchasable = true;
    //this is public so it can be turned off by us on a per unit basis, but is currently- 
    //redudant because the "start" function sets it to true immediately, but if that is changed then this will function.
    private bool isPurchasable = false;
    private bool isSellable = false;
    public int price = 0;

    //FOR TRAINING MODE ONLY
    public bool purchasableEnemy = false;
    //Enemy needs to be tagged as enemy already and this bool only works if changed before the unit is loaded (it relys on Start)

    void Start(){
        multiplePurchasable = true;
        uiController = GameObject.Find("UIController");
        //textLog = uiController.GetComponent<GameUI>().GetTextLog();
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
            healthBar.SetActive(true);
            priceTextBox.SetActive(false);
            if(purchasableEnemy == true){
                //This is sort of lazy coding but I just wanted to amke sure everything is set the same way
                sellPosX = this.transform.localPosition.x;
                sellPosY = this.transform.localPosition.y;
                this.isPurchasable = true;
                this.isSellable = false;
                this.isMovable = true;
                healthBar.SetActive(false);
                priceTextBox.SetActive(true);
                Debug.Log("Purchasable Enemy Loaded.");
            }
		}else if(this.tag == "Ally"){
            //sets the "to sell position", only matters for units we can buy/sell
            sellPosX = this.transform.localPosition.x;
            sellPosY = this.transform.localPosition.y;
            //these are just to make sure things are set correctly. IF FRIENDLY UNITS SPAWN(like start the level) ON BOARD THIS LOGIC DOES NOT WORK
            this.isPurchasable = true;
            this.isSellable = false;
            this.isMovable = true;
            healthBar.SetActive(false);
            priceTextBox.SetActive(true);
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
        if(Time.timeScale != 0){
            if(isPurchasable == true){
                gameObject.SetActive(false);            
            }else{
                this.isMovable = false;
            }
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
            
            string unitDescription;
            Units unitScript = this.gameObject.GetComponent<Units>();
            string damageType;
            if (unitScript.magical) {
                damageType = "Magical";
            }else {
                damageType = "Physical";
            }
            unitDescription = "Name: " + unitScript.type + '\n' +
                                "Health: " + unitScript.health + '\n' +
                                "Attack: " + unitScript.attack + '\n' +
                                "Attack Speed: " + unitScript.attackSpeed + '\n' +
                                "Range: " + unitScript.range + '\n' +
                                "Damage Type: " + damageType + '\n' +
                                "Physical Armor (%): " + unitScript.physicalArmorPercent + '\n' +
                                "Magical Armor (%): " + unitScript.magicalArmorPercent + '\n' +
                                "Move Speed: " + unitScript.moveTime + '\n' +
                                "Special Ability: " + unitScript.specialAbility;
            uiController.GetComponent<GameUI>().ChangeUnitDescription(unitDescription);
            
            // if(this.gameObject.GetComponent<Units>().type == "Zombie") {
            //     unitDescription = "Name: Zombie" + '\n' + 
            //                       "Price: 200 Gold" + '\n' +
            //                       "Health: 100" + '\n' +
            //                       "Attack: 4" + '\n' +
            //                       "Attack Speed: 0.2" + '\n' +
            //                       "Range: 1" + '\n' +
            //                       "Damage Type: Magical" + '\n' +
            //                       "Physical Armor (%): 0.4" + '\n' +
            //                       "Magical Armor (%): 0.4" + '\n' +
            //                       "Move Speed: 0.1" + '\n' +
            //                       "Special Ability: When this" + '\n' + "unit is killed, it will revive" + '\n' + 
            //                       "with half health";
            //     uiController.GetComponent<GameUI>().ChangeUnitDescription(unitDescription);
            // }
            // if(this.gameObject.GetComponent<Units>().type == "Knight") {
            //     unitDescription = "Name: Knight" + '\n' + 
            //                       "Price: 150 Gold" + '\n' +
            //                       "Health: 100" + '\n' +
            //                       "Attack: 3" + '\n' +
            //                       "Attack Speed: 1" + '\n' +
            //                       "Range: 1" + '\n' +
            //                       "Damage Type: Physical" + '\n' +
            //                       "Physical Armor (%): 0.6" + '\n' +
            //                       "Magical Armor (%): 0.6" + '\n' +
            //                       "Move Speed: 0.1" + '\n' +
            //                       "Special Ability: None";
            //     uiController.GetComponent<GameUI>().ChangeUnitDescription(unitDescription);
            // }
            // if(this.gameObject.GetComponent<Units>().type == "Mage") {
            //     unitDescription = "Name: Mage" + '\n' + 
            //                       "Price: 500 Gold" + '\n' +
            //                       "Health: 100" + '\n' +
            //                       "Attack: 25" + '\n' +
            //                       "Attack Speed: 5" + '\n' +
            //                       "Range: 3" + '\n' +
            //                       "Damage Type: Magical" + '\n' +
            //                       "Physical Armor (%): 0" + '\n' +
            //                       "Magical Armor (%): 0" + '\n' +
            //                       "Move Speed: 0.1" + '\n' +
            //                       "Special Ability: None";
            //     uiController.GetComponent<GameUI>().ChangeUnitDescription(unitDescription);
            // }
    		if(check == true){//we dropped the unit in a legal position
                //check if unit is "to be purchased"
                if (isPurchasable == true) {
                    if (gameHandler.GetComponent<Currency>().changeGold(price) >= 0){
                        if(multiplePurchasable){
                            //make a new one at the buy location 
                            GameObject copy;
                            copy = Instantiate(this.gameObject);
                            copy.gameObject.transform.localPosition = new Vector3(sellPosX, sellPosY, 0);
                            //IF multiple purchasing is suspect of messing up, then I believe I should take more time here to check the values of the new object
                            //however through testing this appears to work.
                        }
                        //we have enough money to buy the object
                        isPurchasable = false;
                        isSellable = true;
                        this.gameObject.transform.localPosition = snapPos;
                        string temp = "Purchased " + this.gameObject.GetComponent<Units>().type + "! You lost " + this.price + " gold.";
                        uiController.GetComponent<GameUI>().AppendLog(temp);
                        priceTextBox.SetActive(false);
                        healthBar.SetActive(true);
                    }
                    else{
                        //we don't have enough money to buy the object
                        this.gameObject.transform.localPosition = new Vector3(ogPosX, ogPosY, 0);
                        string temp = "You don't have enough money to purchase " + this.gameObject.GetComponent<Units>().type;
                        uiController.GetComponent<GameUI>().AppendLog(temp);
                    }
                }else{
                    //We already purchased it but are rearranging it on screen
                    this.gameObject.transform.localPosition = snapPos;
                }
    		}else{
    			//if we dropped the unit in an illegal position
                //check if its sellable
                if(isSellable == true){

                    int newPrice = price * -1;//invert price
                    gameHandler.GetComponent<Currency>().changeGold(newPrice);
                    //now we need to place the gameObject back where it came from...
                    this.gameObject.transform.localPosition = new Vector3(sellPosX, sellPosY, 0);
                    string temp = "Sold " + this.gameObject.GetComponent<Units>().type + "! You obtained " + this.price + " gold.";
                    uiController.GetComponent<GameUI>().AppendLog(temp);
                    if(multiplePurchasable){
                        //just "delete" this one
                        transform.gameObject.SetActive(false); //this way actually works
                    }//else{ okay I dont think I need this. So to make sure this code gets ran...
                        isSellable = false;
                        isPurchasable = true;
                        priceTextBox.SetActive(true);
                        healthBar.SetActive(false);
                    //}
                    //textLog.GetComponent<Text>().text += '\n' + "Sold " + this.gameObject.GetComponent<Units>().type + "! You obtained " + this.price + " gold.";

                }else{
                    this.gameObject.transform.localPosition = new Vector3(ogPosX, ogPosY, 0);
                }
    		}

    	}
    	isBeingHeld = false;
    }

}
