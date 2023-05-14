using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    public Button btn;
    public NPCController npc;
    public Vector2Int cords;
    public Sprite[] img;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Spawn(int x, int y,int type)
    {
        cords[0] = x;
        cords[1] = y;
        btn.image.sprite = img[type];
        transform.position += new Vector3((x - 10) * 10f, (y - 10) * 10f);
        btn.onClick.AddListener(sendNpc);
    }
    public void insertNPC(NPCController onpc)
    {
        npc = onpc;
    }
    void sendNpc()
    {
        Debug.Log("PUSH the button!");
        Vector3 com_pos = new Vector3(cords[0] - 10f, npc.transform.position.y, cords[1] - 10f);
        Debug.Log(com_pos);
        npc.command(com_pos);
    }
}
