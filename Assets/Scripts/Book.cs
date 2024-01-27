using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Book : MonoBehaviour
{
    public Collider2D player;
    public int itemType;
    public float timer = 1;
    public Sprite pi;
    public int an;
    private Coroutine cor;
    public GameObject window;
    List<Vector2> pointList = new List<Vector2>();
    bool isdrop = false;
    int num = 0;
    private void Start()
    {

    }
    private void Update()
    {
        
    }
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
    /// <summary>
    /// 被拾起
    /// </summary>

    public void getItem()
    {

       cor= StartCoroutine(Addanger());

    }
    /// <summary>
    /// 使用
    /// </summary>

    public void useItem()
    {
        //销毁碰撞体
        Invoke("usebook", timer);
    }

    public void dropItem()
    {
        StopCoroutine(cor);
        Debug.Log("停止");
    }

    void usebook()
    {

        gameObject.GetComponent<SpriteRenderer>().sprite = pi;

        Destroy(gameObject.GetComponent<Collider2D>());
    }

    IEnumerator Addanger()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            player.SendMessage("angerChange", an);
            
        }

    }
    /// <summary>
    /// 扔窗外
    /// </summary>
    public void dropToWindow()
    {
            
        Vector2 MoveSpeed = ( window.transform.position- player.transform.position).normalized * 10;
        //定义一个列表存放所有的计算的点
        Debug.Log("movespeed"+MoveSpeed);
        pointList.Add(player.transform.position);
        Debug.Log(player.transform.position);
        for (int i = 1; i < 50; i++)
        {
            float time = i * 0.02f * 5;
            float timePow = time * time;
            //下一个点
            Vector2 point = new Vector2(pointList.First().x + MoveSpeed.x * time, pointList.First().y + MoveSpeed.y * time - 0.5f * Physics2D.gravity.magnitude * timePow);
     
    
                pointList.Add(point);//加入到点的列表中
            
        }
        isdrop = true;
        StartCoroutine(dropwindow());
    }

    IEnumerator dropwindow()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (isdrop)
            {
                transform.position = pointList[num];
                num++;
                if (num == 20)
                {
                    isdrop = false;
                    Destroy(gameObject);
                }
            }
        }
    }
}
