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
    /// ��ʰ��
    /// </summary>

    public void getItem()
    {

       cor= StartCoroutine(Addanger());

    }
    /// <summary>
    /// ʹ��
    /// </summary>

    public void useItem()
    {
        //������ײ��
        Invoke("usebook", timer);
    }

    public void dropItem()
    {
        StopCoroutine(cor);
        Debug.Log("ֹͣ");
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
    /// �Ӵ���
    /// </summary>
    public void dropToWindow()
    {

    }
}
