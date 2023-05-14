using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCopy : MonoBehaviour
{
    public int mazeLevels;
    public MapController mcv;
    public NPCController npc;
    [SerializeField] MazeGeneratorCopy mg;
    [SerializeField] MazeNode nodePrefab;
    [SerializeField] Vector3Int mazeSize;
    [SerializeField] Staircase staircase;
    [SerializeField] Key key;
    private void Start()
    {
        
    }
    public void GenerateMaze(Vector3Int size, int leftOver, int fe, int se, int kl, GameObject PlayerEye, PlayerControl player, MapController omcv, NPCController onpc)
    {
        npc = onpc;
        mcv = omcv;
        mazeLevels = leftOver;
        List<MazeNode> nodes = new List<MazeNode>();
        List<MazeNode> specialNodes = new List<MazeNode>();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), size.z, y - (size.y / 2f));
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                nodes.Add(newNode);
                if (nodes.Count - 1 == fe || nodes.Count - 1 == se)
                {
                    nodes[nodes.Count - 1].RemoveWall(4);
                }
            }
        }
        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);

        //currentPath[0].SetState(NodeState.Current);

        while (completedNodes.Count < nodes.Count)
        {
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleNextDirection = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;
            if (currentNodeX < size.x - 1)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) && !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleNextDirection.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }

            }
            if (currentNodeX > 0)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) && !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleNextDirection.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if (currentNodeY < size.y - 1)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) && !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleNextDirection.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            if (currentNodeY > 0)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) && !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleNextDirection.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }
            if (possibleNextDirection.Count > 0)
            {
                Random.seed = System.DateTime.Now.Millisecond;
                int chosenDirection = Random.Range(0, possibleNextDirection.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];
                switch (possibleNextDirection[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }
                currentPath.Add(chosenNode);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);

                currentPath.RemoveAt(currentPath.Count - 1);

            }
        }
        int rnd = 0;
        while (rnd < size.x || rnd > nodes.Count - size.x || rnd % size.x == 0 || rnd % size.x == size.x - 1 || rnd == fe || rnd == se)
        {
            Random.seed = System.DateTime.Now.Millisecond;
            rnd = Random.Range(size.x, nodes.Count);
        }
        int rnd1 = 0;
        while (rnd1 < size.x || rnd1 > nodes.Count - size.x || rnd1 % size.x == 0 || rnd1 % size.x == size.x - 1 || rnd1 == rnd || rnd1 == fe || rnd1 == se)
        {
            Random.seed = System.DateTime.Now.Millisecond;
            rnd1 = Random.Range(size.x, nodes.Count);
        }
        Random.seed = System.DateTime.Now.Millisecond;
        int rnd2 = Random.Range(0, nodes.Count);
        while (rnd1 == rnd2 || rnd == rnd2 || rnd2 == fe || rnd2 == se)
        {
            Random.seed = System.DateTime.Now.Millisecond;
            rnd2 = Random.Range(0, nodes.Count);
        }
        int rndn = nodes[rnd].availableWall();
        int rnd1n = nodes[rnd1].availableWall();
        Vector3 change = new Vector3(0.4f, -0.4f, 0);
        Vector3 changeKey = new Vector3(0, -0.44f, 0);
        Staircase sc1 = Instantiate(staircase, nodes[rnd].transform.position + change, Quaternion.identity, transform);
        rotate_obj(sc1, rndn);
        Staircase sc2 = Instantiate(staircase, nodes[rnd1].transform.position + change, Quaternion.identity, transform);
        rotate_obj(sc2, rnd1n);
        Key k = Instantiate(key, nodes[rnd2].transform.position + changeKey, Quaternion.identity, transform);
        k.setNum(size.z);
        sc1.setPlayer(PlayerEye, player, npc);
        sc1.setNum(size.z);
        sc2.setPlayer(PlayerEye, player,npc);
        sc2.setNum(size.z);
        k.setPlayer(PlayerEye, player);
        if (mazeLevels > 0)
        {
            MazeGeneratorCopy newMaze = Instantiate(mg, new Vector3(0, 0, 0), Quaternion.identity);
            foreach (Transform id in newMaze.transform)
            {
                GameObject.Destroy(id.gameObject);
            }
            Vector3Int ns = new Vector3Int(size.x, size.y, size.z + 1);
            newMaze.GenerateMaze(ns, mazeLevels - 1, rnd, rnd1, rnd2, PlayerEye, player, mcv, npc);
        }
        int[,] test = new int[20, 20];
        for (int i = 0; i < nodes.Count; i++)
        {
            if (i == rnd1)
            {
                sc2.setLoc(new Vector3Int(2 - mazeLevels, i / 20, i % 20));
                test[i / 20, i % 20] = 2;
            }
            else
            {
                if (i == rnd)
                {
                    sc1.setLoc(new Vector3Int(2 - mazeLevels, i / 20, i % 20));
                    test[i / 20, i % 20] = 2;
                }
                else
                {
                    if (i == rnd2)
                    {
                        test[i / 20, i % 20] = 1;
                        k.setLoc(new Vector3Int(2 - mazeLevels, i / 20, i % 20));
                    }
                    else
                    {
                        test[i / 20, i % 20] = 0;
                    }
                }
            }
        }
        mcv.updateData(test, 2 - mazeLevels);
    }
    void rotate_obj(Staircase sc, int walls)
    {
        //Debug.Log(walls);
        if (walls / 1000 == 1)
        {
            sc.transform.Rotate(0, 270, 0);
            sc.transform.position = sc.transform.position + new Vector3(-0.4f, 0f, 0.4f);
            return;

        }
        if (walls / 100 == 1)
        {
            sc.transform.Rotate(0, 90, 0);
            sc.transform.position = sc.transform.position + new Vector3(-0.4f, 0f, -0.4f);
            return;
        }
        if (walls / 10 == 1)
        {
            return;
        }
    }
}
