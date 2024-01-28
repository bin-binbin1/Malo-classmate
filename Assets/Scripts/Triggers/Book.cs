using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Book : MonoBehaviour
{
    public Collider2D player;
    public int itemType=0;
    public float timer = 1;
    public Sprite pi;
    public int an=80;
    private Coroutine cor;
    public GameObject window;
    List<Vector2> pointList = new List<Vector2>();
    bool isdrop = false;
    int num = 0;
    public GameObject yihuo,dushu;
    public GameObject study_poeple, wushu_people;
    private void Start()
    {
        yihuo.SetActive(false);
        wushu_people.SetActive(false);
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
    /// ��ʰ��
    /// </summary>

    public void getItem()
    {
        yihuo.SetActive(true);
        dushu.SetActive(false);
       cor= StartCoroutine(Addanger());

    }
    /// <summary>
    /// ʹ��
    /// </summary>

    public void useItem()
    {
        player.gameObject.SendMessage("angerChange", an);
        //������ײ��
        Invoke("usebook", timer);
    }

    public void dropItem()
    {
        yihuo.SetActive(false);
        dushu.SetActive(true);
        StopCoroutine(cor);
        Debug.Log("ֹͣ");
    }

    void usebook()
    {

        gameObject.GetComponent<SpriteRenderer>().sprite = pi;
        yihuo.SetActive(false);
        dushu.SetActive(false);
        wushu_people.SetActive(true);
        study_poeple.SetActive(false) ;
        //Destroy(gameObject.GetComponent<Collider2D>());
        enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator Addanger()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            player.gameObject.SendMessage("angerChange", 5);
            
        }

    }
    /// <summary>
    /// �Ӵ���
    /// </summary>
    public void dropToWindow()
    {
            
        Vector2 MoveSpeed = ( window.transform.position- player.transform.position).normalized * 10;
        //����һ���б������еļ���ĵ�
        Debug.Log("movespeed"+MoveSpeed);
        pointList.Add(player.transform.position);
        Debug.Log(player.transform.position);
        for (int i = 1; i < 50; i++)
        {
            float time = i * 0.02f * 5;
            float timePow = time * time;
            //��һ����
            Vector2 point = new Vector2(pointList.First().x + MoveSpeed.x * time, pointList.First().y + MoveSpeed.y * time - 0.5f * Physics2D.gravity.magnitude * timePow);
     
    
                pointList.Add(point);//���뵽����б���
            
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
