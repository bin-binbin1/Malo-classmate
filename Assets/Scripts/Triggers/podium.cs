using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class podium : MonoBehaviour
{
    public Collider2D player;
    bool isin = false;
    

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && isin)
        {
            //Ìøµ½½²Ì¨ÉÏ
            
            object[] obj = new object[2];
            obj[0] = transform.position.x;
            obj[1] = transform.position.y;
            player.gameObject.SendMessage("Jumptopodium", obj);
            
            
            player.gameObject.SendMessage("angerChange", 40);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player)
        {
            enabled = true;
            isin = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == player)
        {
            enabled = false;
            isin = false;
        }
    }

}
