using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public uint startingGold = 0; //amount should be different per stage
    public uint gold = 0; //the actual amount of gold the player has, should be made private post-testing

    // Start is called before the first frame update
    void Start()
    {
        gold = startingGold;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        return -2;
    }
}
