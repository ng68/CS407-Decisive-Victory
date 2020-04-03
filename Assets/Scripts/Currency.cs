using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    public uint startingGold = 0; //amount should be different per stage
    public uint gold = 0; //the actual amount of gold the player has, should be made private post-testing

    private GameObject UIController;
    private GameObject currencyText;
    // Start is called before the first frame update
    void Start()
    {
        gold = startingGold;
        UIController = GameObject.Find("UIController");
        currencyText = UIController.GetComponent<GameUI>().GetCurrency();
    }

    // Update is called once per frame
    void Update()
    {
        currencyText.GetComponent<Text>().text = "Gold: " + gold;   
    }

    public int changeGold(int amount) {
        int newGold = (int)gold - amount;
        if (newGold < 0){
            return -1;
        }
        else {
            gold = (uint)newGold;
            return newGold;
        }
    }
}
