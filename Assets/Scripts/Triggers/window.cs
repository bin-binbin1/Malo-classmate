using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class window : MonoBehaviour
{
    public Collider2D player;
    public string nextSceneName;
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
            Debug.Log(KeyCode.F);
            if (Input.GetKeyUp(KeyCode.F))
            {
                //player.SendMessage("interact");
                if (nextSceneName == "end")
                {
                    Application.Quit();
                }
                else
                {
                    SceneManager.LoadScene(nextSceneName);
                }
            }
        }
    }
}