﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameTransfer : MonoBehaviour
{
    // Start is called before the first frame update
    public string theName;
    public GameObject inputField;
    
    public void StoreName()
    {
        theName = inputField.GetComponent<Text>().text;
        Globals.saves[Globals.currSlot].charname = theName;
    }
}
