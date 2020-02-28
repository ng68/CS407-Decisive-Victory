using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid {
	private int width;
	private int height;
	private int[,] gridArray;
	private float cellSize;

	public GameGrid(int width, int height, float cellSize){

		this.width = width;
		this.height = height;
		this.cellSize = cellSize;

		gridArray = new int[width, height];

		Debug.Log(width + " " + height);

		for(int x = 0; x < gridArray.GetLength(0); x++){
			for(int y = 0; y < gridArray.GetLength(1); y++){
				//following lines are to see the effective grid with debug lines
				//uncomment for testing / do not delete
				//Debug.Log(x + ", " + y);
				//Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.black, 100f);
				//Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.black, 100f);
			}
		}
		//These draw the outer edges of the grid
		//Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.black, 100f);
		//Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.black, 100f);
	}

	//accessor methods
	public int getWidth(){
		return this.width;
	}

	public int getHeight(){
		return this.height;
	}

	//Modified world position to spawn cells in correct locations
	public Vector3 GetProperWorldPosition(int x, int y){
		Vector3 properPos = new Vector3(x, y) * cellSize;
		properPos = properPos + new Vector3(cellSize/2, cellSize/2);
		return properPos;
	}

	//testing function
	private Vector3 GetWorldPosition(int x, int y){
		return new Vector3(x, y) * cellSize;
	}

	//methods for On Click
	private void GetXY(Vector3 worldPosition, out int x, out int y){
		x = Mathf.FloorToInt(worldPosition.x / cellSize);
		y = Mathf.FloorToInt(worldPosition.y / cellSize);
	}

	public void SquareClick (Vector3 worldPosition){
		int x, y;
		GetXY(worldPosition, out x, out y);
		if(x >= 0 && y >= 0 && x < width && y < height){

			//currently this function just counts how many times you click on it
			gridArray[x, y]++;		
			//Debug.Log(x + " " + y + " " + gridArray[x, y]);
		}else{
			//Debug.Log("Out of bounds click");
		}
	}
}