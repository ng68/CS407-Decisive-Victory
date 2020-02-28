using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell: MonoBehaviour
{
	public int gridX;
	public int gridY;
    // Start is called before the first frame update
    void Start()
    {
        //this.gridX = gridX;
        //this.gridY = gridY;
    }

    //public setters for object

    public void setXY(int x, int y){
    	this.gridX = x;
    	this.gridY = y;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
