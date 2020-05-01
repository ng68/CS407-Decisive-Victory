using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid<TGridObject> {

	public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs {
        public int x;
        public int y;
    }

	private int width;
	private int height;
	private float cellSize;
	private Vector3 originPos;
	private TGridObject[,] gridArray;

	public GameGrid(int width, int height, float cellSize, Vector3 originPos, Func<GameGrid<TGridObject>, int, int, TGridObject> createGridObject){

		this.width = width;
		this.height = height;
		this.cellSize = cellSize;
		this.originPos = originPos;
		gridArray = new TGridObject[width, height];

		for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

		//Debug.Log(width + " " + height);
		/*
		//following lines are to see the effective grid with debug lines
		//uncomment for testing / do not delete
		for(int x = 0; x < gridArray.GetLength(0); x++){
			for(int y = 0; y < gridArray.GetLength(1); y++){
				//Debug.Log(x + ", " + y);
				Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.black, 100f);
				Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.black, 100f);
			}
		}
		//These draw the outer edges of the grid
		Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.black, 100f);
		Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.black, 100f);
		*/
	}

	//accessor methods
	public int GetWidth(){
		return this.width;
	}

	public int GetHeight(){
		return this.height;
	}

	public float GetCellSize() {
        return cellSize;
    }

	//testing function
	private Vector3 GetWorldPosition(int x, int y){
		return new Vector3(x, y) * cellSize + originPos;
	}

	//Modified world position to spawn cells in correct locations
	public Vector3 GetProperWorldPosition(int x, int y){
		Vector3 properPos = new Vector3(x, y) * cellSize + originPos;
		properPos = properPos + new Vector3(cellSize/2, cellSize/2);
		return properPos;
	}

	//methods for On Click
	public void GetXY(Vector3 worldPosition, out int x, out int y){
		x = Mathf.FloorToInt((worldPosition - originPos).x / cellSize);
		y = Mathf.FloorToInt((worldPosition - originPos).y / cellSize);
	}

	public void SetGridObject(int x, int y, TGridObject value) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            gridArray[x, y] = value;
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }
    }

	public void TriggerGridObjectChanged(int x, int y) {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }

	public void SetGridObject(Vector3 worldPosition, TGridObject value) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            return gridArray[x, y];
        } else {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }

	//clicking functions
	public void SquareClick (Vector3 worldPosition){
		//currently there is no need for a function on click, but is still here for testing purposes
		int x, y;
		GetXY(worldPosition, out x, out y);
		if(x >= 0 && y >= 0 && x < width && y < height){
			//currently this function just counts how many times you click on it
			//gridArray[x, y]++;	
			//Debug.Log(x + " " + y + " " + gridArray[x, y]);
		}else{
			//Debug.Log("Out of bounds click");
		}
	}

	public void DropUnitOnSquare (Vector3 worldPosition, out Vector3 endPos, out bool success){
		int x, y;
		GetXY(worldPosition, out x, out y);
		//this is so the function atleast sets endPos once even though it wont matter if it fails
		endPos = worldPosition;
		if(x >= 0 && y >= 0 && x < width && y < height){
			success = true;
			endPos = GetProperWorldPosition(x, y);
		}else{
			success = false;
			//Debug.Log("Out of bounds drop");
		}
	}
}