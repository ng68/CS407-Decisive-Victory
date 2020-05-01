using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GameGrid<PathNode> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;

    public PathNode originNode;

    public PathNode(GameGrid<PathNode> grid, int x, int y){
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.isWalkable = true;
    }

    public void calculateFCost(){
        fCost = gCost + hCost;
    }

    public void SetIsWalkable(bool isWalkable) {
        this.isWalkable = isWalkable;
        grid.TriggerGridObjectChanged(x, y);
    }
}