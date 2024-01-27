using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public Collider2D player;
    public int itemType;
    public float timer = 1;
    public Sprite pi;
    public int an;
    private Coroutine cor;
    private void Start()
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
            Debug.Log(an);
        }

    }
    /// <summary>
    /// 扔窗外
    /// </summary>
    public void dropToWindow()
    {

    }
}
