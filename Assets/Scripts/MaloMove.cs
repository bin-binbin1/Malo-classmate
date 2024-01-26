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
    private int currentItemIndex;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        items=itemFather.GetComponentsInChildren<Transform>();
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
            {
                //丢弃物品
                hold.transform.SetLocalPositionAndRotation(transform.position,transform.rotation);
                hold.SetActive(true);
                hold = null;
            }
            else
            {
                if (currentItemIndex != -1)
                {
                    hold = items[currentItemIndex].gameObject;
                    hold.SetActive(false);
                    //转换到拿起的图片

                }
                //拿起物品
            }
        }
        
    }
    public void setCurrentItem(int itemType)
    {
        currentItemIndex = itemType;
    }
    public void releaseItem()
    {
        currentItemIndex = -1;
    }


}
