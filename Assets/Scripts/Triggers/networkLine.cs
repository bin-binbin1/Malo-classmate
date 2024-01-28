using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class networkLine : MonoBehaviour
{
    public Collider2D player;
    private float t=0;
    private bool lights;
    public Vector3 moveLine;
    public GameObject net,aixin,suixin;
    public Sprite lianwang,duanwang;
    
    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
        lights = true;
        suixin.SetActive(false);
        lianwang = net.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            SpriteRenderer t= net.GetComponent<SpriteRenderer>();
            if (lights)//ÓÐÍø£¬Ôò¶ÏÍø
            {
                aixin.SetActive(false);
                suixin.SetActive(true);
                t.sprite = duanwang;
            }
            else//Ã°°®ÐÄ
            {
                t.sprite=lianwang;
                suixin.SetActive(false);
                aixin.SetActive(true);
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
