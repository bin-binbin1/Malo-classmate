using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class window : MonoBehaviour
{
    Collider2D player;
    string nextSceneName;
    public void OnTriggerEnter2D(Collider2D other)
    {

        if(player==other)
        {
            player.SendMessage("gotoWindow");
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(player==collision)
        {
            player.SendMessage("leaveWindow");
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision==player)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                //player.SendMessage("interact");
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
