using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    bool isOn = false;
    public GameObject mapPanel;
    public PlayerControl player;
    public NPCController npc;
    private bool waiting = false;
    private float timer = 0.0f;
    public int[,,] buttonsDataTrue = new int[3,20,20];
    public bool[,,] buttonsDataKnownToPlayer = new bool[3, 20, 20];
    public List<MapButton> buttons = new List<MapButton>();
    public MapButton button_prefab;
    private Vector3Int lastPlayerLocation;
    public int cf = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y<3; y++)
        {
            for(int x = 0; x<20; x++)
            {
                for(int z = 0; z<20; z++)
                {
                    if (buttonsDataTrue[y, x, z] == null)
                    {
                       buttonsDataTrue[y, x, z] = 0;
                    }
                        
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 1 && waiting)
        {
            timer = 0;
            waiting = false;
        }
        else
        {
            if (waiting)
            {
                timer += Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.M)&&!waiting)
        {
            waiting = true;
            isOn = !isOn;
            mapPanel.GetComponent<Image>().enabled = isOn;
            player.mapControl(isOn);
            controlMap();
        }
    }
    void controlMap()
    {
        if (isOn)
        {
            int px = Mathf.FloorToInt(player.transform.position.z+10.5f); 
            int py = Mathf.FloorToInt(player.transform.position.x + 10.5f);
            int nx = Mathf.FloorToInt(npc.transform.position.z + 10.5f);
            int ny = Mathf.FloorToInt(npc.transform.position.x + 10.5f);
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    MapButton mp = Instantiate(button_prefab, transform);
                    buttons.Add(mp);
                    mp.insertNPC(npc);
                    if (j == px && i == py)
                    {
                        mp.Spawn(i, j, 6);
                    }
                    else
                    {
                        if (j == nx && i == ny)
                        {
                            mp.Spawn(i, j, 3);
                        }
                        else
                        {
                            if (buttonsDataKnownToPlayer[player.current_floor, i, j])
                            {
                                mp.Spawn(i, j, buttonsDataTrue[player.current_floor, i, j]);
                                Debug.Log("KNOWS!, " + buttonsDataTrue[player.current_floor, i, j]);
                                Debug.Log(player.current_floor + "," + i + "," + j);
                            }
                            else
                            {
                                mp.Spawn(i, j, 0);
                                //Debug.Log(player.current_floor+","+i + "," + j + " DOESNT KNOW!");

                            }
                        }
                    }
                }
            }
        }
        else
        {
            //Debug.Log(buttons.Count);
            for(int i = 0; i<buttons.Count; i++)
            {
                //Debug.Log(i);
                Destroy(buttons[i].GetComponent<Button>().gameObject);
            }
            buttons.Clear();
        }
    }
    public void updateData(int[,] data, int f)
    {
        for(int i = 0; i<data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                buttonsDataTrue[f, i, j] = data[i, j];
            }
        }
    }
    public void updateKnown(Vector3Int tu)
    {
        buttonsDataKnownToPlayer[tu.x, tu.y, tu.z] = true;
    }
}
