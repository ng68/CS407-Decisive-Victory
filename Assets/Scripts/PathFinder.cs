using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public static PathFinder Instance { get; private set; }

    private GameGrid<PathNode> grid;
    private List<PathNode> openblocks;
    private List<PathNode> closedblocks;

    public PathFinder(int width, int height){
        Instance = this;
        grid = new GameGrid<PathNode>(width, height, 10f, Vector3.zero, (GameGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));

    }

    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition) {
        grid.GetXY(startWorldPosition, out int startX, out int startY);
        grid.GetXY(endWorldPosition, out int endX, out int endY);

        List<PathNode> path = FindPath(startX, startY, endX, endY);
        if (path == null) {
            return null;
        } else {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in path) {
                vectorPath.Add(new Vector3(pathNode.x, pathNode.y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f);
            }
            return vectorPath;
        }
    }

    private List<PathNode> FindPath(int startX, int startY, int  endX, int endY){
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);
        if (startNode == null || endNode == null) {
            // Invalid Path
            return null;
        }
        openblocks = new List<PathNode> {startNode};
        closedblocks = new List<PathNode>();
        
        for(int i=0; i<grid.GetWidth(); i++){
            for(int j=0; j<grid.GetHeight(); j++){
                PathNode n = grid.GetGridObject(i,j);
                n.gCost = int.MaxValue;
                n.calculateFCost();
                n.originNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = calculateDistance(startNode, endNode);
        startNode.calculateFCost();

        while(openblocks.Count > 0){
            PathNode curr = getLowestNode(openblocks);
            if(curr == endNode){
                return calculatePath(endNode);
            }
            openblocks.Remove(curr);
            closedblocks.Add(curr);

            foreach (PathNode neighbourNode in getNeighbors(curr)) {
                if (closedblocks.Contains(neighbourNode)) continue;
                if (!neighbourNode.isWalkable) {
                    closedblocks.Add(neighbourNode);
                    continue;
                }
                int tentativeGCost = curr.gCost + calculateDistance(curr, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost) {
                    neighbourNode.originNode = curr;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = calculateDistance(neighbourNode, endNode);
                    neighbourNode.calculateFCost();

                    if (!openblocks.Contains(neighbourNode)) {
                        openblocks.Add(neighbourNode);
                    }
                }
            }
        }
        return null;
    }

    private List<PathNode> getNeighbors(PathNode curr){
        List<PathNode> neighbourList = new List<PathNode>();
        if (curr.x - 1 >= 0) {
            // Left
            neighbourList.Add(GetNode(curr.x - 1, curr.y));
            // Left Down
            if (curr.y - 1 >= 0) neighbourList.Add(GetNode(curr.x - 1, curr.y - 1));
            // Left Up
            if (curr.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(curr.x - 1, curr.y + 1));
        }
        if (curr.x + 1 < grid.GetWidth()) {
            // Right
            neighbourList.Add(GetNode(curr.x + 1, curr.y));
            // Right Down
            if (curr.y - 1 >= 0) neighbourList.Add(GetNode(curr.x + 1, curr.y - 1));
            // Right Up
            if (curr.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(curr.x + 1, curr.y + 1));
        }
        // Down
        if (curr.y - 1 >= 0) neighbourList.Add(GetNode(curr.x, curr.y - 1));
        // Up
        if (curr.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(curr.x, curr.y + 1));

        return neighbourList;
    }

    private List<PathNode> calculatePath(PathNode endNode){
        List<PathNode> p = new List<PathNode>();
        p.Add(endNode);
        PathNode curr = endNode;
        while(curr.originNode != null){
            p.Add(curr.originNode);
            curr = curr.originNode;
        }
        p.Reverse();
        return p;
    }

    private int calculateDistance(PathNode a, PathNode b){
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode getLowestNode(List<PathNode> pnodes){
        PathNode lowestNode = pnodes[0];
        for(int i=0; i<pnodes.Count; i++){
            if(pnodes[i].fCost < lowestNode.fCost){
                lowestNode = pnodes[i];
            }
        }
        return lowestNode;
    }

    public PathNode GetNode(int x, int y) {
        return grid.GetGridObject(x, y);
    }

    public GameGrid<PathNode> GetGrid() {
        return grid;
    }

}