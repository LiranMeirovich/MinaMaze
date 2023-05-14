using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NodeState
{
    Available,
    Current,
    Completed
}

public class MazeNode : MonoBehaviour
{
    int existing_walls = 1111;
    [SerializeField] GameObject[] walls;
    public void RemoveWall(int wallToRemove)
    {
        walls[wallToRemove].gameObject.SetActive(false);
        existing_walls -= (int) Mathf.Pow(10, wallToRemove);
    }
    public void SetState(NodeState state)
    {
    }
    public int availableWall()
    {
        return existing_walls;
    }
}
