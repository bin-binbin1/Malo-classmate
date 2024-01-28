using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fan : MonoBehaviour
{
    public Collider2D player;
    bool isin=false;
    public Sprite p1;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.gameObject.name);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F)&&isin)
        {
            //Ìøµ½·çÉÈÉÏ
            Debug.Log(transform.position.y);
            player.gameObject.SendMessage("JumptoFan",transform.position.y);
            float dis = (transform.position.y - player.gameObject.transform.position.y)/10;
            StartCoroutine("fanDrop",dis);
            player.gameObject.SendMessage("angerChange", 50);
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
            isin=false;
        }
    }
    IEnumerator fanDrop(float dis)
    {
        Vector3 pos = transform.position;
        int num = 0;

        while (true)
        {
            yield return new WaitForSeconds(0.05f);

            num++;
             if (num >= 30 && num < 40)
            {
                pos.y -= dis;
            }
            else if (num == 40)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = p1;
                break;
            }
            transform.position = pos;
            Debug.Log("POSY" + transform.position.y);
        }
    }
}
