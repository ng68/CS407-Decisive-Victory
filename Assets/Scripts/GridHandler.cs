using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
	public GameObject[] boardTiles;	
	private GameGrid grid;
	private Transform boardHolder;
	private void Start() {
		//Because of first version (white/black) prefab size, the cellsize has to be 10
		//otherwise it can be variabled, but prefab size needs to scale to cellsize (it currently does not)
		grid = new GameGrid(10, 10, 10f);
		BoardSetup();
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)){
			grid.SquareClick(GetMouseWorldPosition());
		}
	}


	void BoardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject ("Board").transform;
		//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
		for(int x = 0; x < grid.getWidth(); x++){
			//Loop along y axis, starting from -1 to place floor or outerwall tiles.
			for(int y = 0; y < grid.getHeight(); y++){
				//Choose white or black tile based on grid position
				int color = 0; //white is 0, black is 1
				if((x % 2 == 0 && y % 2 == 1) || (x % 2 == 1 && y % 2 == 0)){
					//if the grid numbers are odd/even (not odd/odd or even/even) then it should be black
					color = 1;
				}
				GameObject toInstantiate = boardTiles[color];
				GameObject instance = Instantiate (toInstantiate, grid.GetProperWorldPosition(x,y), Quaternion.identity) as GameObject;
				instance.GetComponent<Cell>().setXY(x, y); //set X Y of object
				//to clean things up, make the parent of the object the boardHolder.
				instance.transform.SetParent (boardHolder);
			}
		}
	}

	//accessor function for grid snapping

	public void GridCheck(Vector3 pos, out Vector3 toSnap, out bool success){
		//annoying but this is the way I have it implemented currently:
		//bool successor;
		//Vector3 snapper;
		grid.DropUnitOnSquare(pos, out toSnap, out success);
		//toSnap = snapper;
		//success = successor;
	}


	//recursive functions to obtain the mouse's current position
	//MOVE ELSEWHERE... maybe?

	public static Vector3 GetMouseWorldPosition(){
		Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
		vec.z = 0f;
		return vec;
	}
	public static Vector3 GetMouseWorldPositionWithZ(){
		return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
	}
	public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera){
		return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
	}
	public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera){
		Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
		return worldPosition;
	}
}