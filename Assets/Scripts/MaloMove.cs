using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaloMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private GameObject hold;
    public GameObject itemFather;
    private Transform[] items;
    private int currentItemIndex=-1;
    private Transform[] malodata;
    private GameObject holddata;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        items=itemFather.GetComponentsInChildren<Transform>();
        foreach (var child in items)
        {
            Debug.Log(child.name);
        }
        malodata = rb.GetComponentsInChildren<Transform>();
        holddata = malodata[1].gameObject;
        holddata.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if(hold != null)
            {   hold.SetActive(true);
                //丢弃物品
                float x = this.transform.position.x;
                float y = this.transform.position.y;
                hold.transform.position = new Vector3(x,y,0);
                holddata.GetComponent<Renderer>().enabled = false;
                
                hold = null;
            }
            else
            {
                if (currentItemIndex != -1)
                {
                    hold = items[1].gameObject;
                    Debug.Log(hold.name);
                    hold.SetActive(false);
                    //转换到拿起的图片

                    Texture2D tt = hold.GetComponent<SpriteRenderer>().sprite.texture;
                    
                    holddata.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tt, hold.GetComponent<SpriteRenderer>().sprite.textureRect, new Vector2(0.5f, 0.5f),500);

                    holddata.GetComponent<Renderer>().enabled = true;
                }
                //拿起物品
            }
        }
        
    }
    public void setCurrentItem(int itemType)
    {
        currentItemIndex = itemType;
        Debug.Log("MALO碰到了" + itemType);
    }
    public void releaseItem()
    {
        currentItemIndex = -1;
        Debug.Log("MALO没有碰到物体");
    }


}
