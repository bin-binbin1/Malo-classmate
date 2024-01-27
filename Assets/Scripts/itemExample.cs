using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemExample : MonoBehaviour
{
    public Collider2D player;
    public int itemType;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player)
        {

            player.gameObject.SendMessage("setCurrentItem", itemType);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other == player)
        {
            
            player.gameObject.SendMessage("releaseItem", itemType);
        }
    }
    void userItem()
    {

    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            //触发事件的流程
        }
    }
}
