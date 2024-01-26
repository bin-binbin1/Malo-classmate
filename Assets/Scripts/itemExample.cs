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
    public void OnTriggerExit(Collider other)
    {
        if (other == player)
        {
            player.gameObject.SendMessage("releaseItem");
        }
    }
}
