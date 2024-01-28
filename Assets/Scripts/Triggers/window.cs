using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class window : MonoBehaviour
{
    public Collider2D player;
    public string nextSceneName;
    private bool toNextScene=false;
    public void OnTriggerEnter2D(Collider2D other)
    {

        if(player==other)
        {
            player.SendMessage("gotoWindow");
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(player==collision&&!toNextScene)
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
                toNextScene = true;
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
