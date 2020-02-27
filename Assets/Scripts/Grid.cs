using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
	private int width;
	private int height;
	private int[,] gridArray;
	private float cellSize;

	public Grid(int width, int height, float cellSize){

		this.width = width;
		this.height = height;
		this.cellSize = cellSize;

		gridArray = new int[width, height];

		Debug.Log(width + " " + height);

		for(int x = 0; x < gridArray.GetLength(0); x++){
			for(int y = 0; y < gridArray.GetLength(1); y++){
				//following lines are to see the effective grid
				//Debug.Log(x + ", " + y);
				Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.black, 100f);
				Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.black, 100f);
			}
		}
		Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.black, 100f);
		Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.black, 100f);
	}

	private Vector3 GetWorldPosition(int x, int y){
		return new Vector3(x, y) * cellSize;
	}

	private void GetXY(Vector3 worldPosition, out int x, out int y){
		x = Mathf.FloorToInt(worldPosition.x / cellSize);
		y = Mathf.FloorToInt(worldPosition.y / cellSize);
	}

	public void SquareClick (Vector3 worldPosition){
		int x, y;
		GetXY(worldPosition, out x, out y);
		gridArray[x, y]++;
		Debug.Log(x + " " + y + " " + gridArray[x, y]);
	}
}
