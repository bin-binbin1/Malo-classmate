using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class networkLine : MonoBehaviour
{
    public Collider2D player;
    private float t=0;
    private bool lights;
    public Vector3 moveLine;
    public GameObject aixin;
    public GameObject xinsui;
    // Start is called before the first frame update
    void Start()
    {
        xinsui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if(lights)//ÓÐÍø£¬Ôò¶ÏÍø
            {
                xinsui.SetActive(true);
                aixin.SetActive(false);
                transform.position += moveLine;
            }
            else//Ã°°®ÐÄ
            {
                xinsui.SetActive(false);
                aixin.SetActive(true);
                transform.position -= moveLine;
            }
            lights=!lights;
        }
        if (!lights)
        {
            t += Time.deltaTime;
            if (t > 1)
            {
                player.SendMessage("angerChange", 15);
                t -=1;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player)
        {
            enabled = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == player)
        {
            enabled = false;
            
        }
    }
}
