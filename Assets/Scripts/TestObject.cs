using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
	private Grid grid;
	private void Start() {
		grid = new Grid(4, 4, 10f);		
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)){
			grid.SquareClick(GetMouseWorldPosition());
		}
	}

	//recursive functions to obtain the mouse's current position
	//MOVE ELSEWHERE

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