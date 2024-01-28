using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Glass : MonoBehaviour
{

    public Collider2D player;
    public int itemType=3;
    public Sprite pi;
    public int an=20;
    private Coroutine cor;
    public GameObject window;
    List<Vector2> pointList = new List<Vector2>();
    bool isdrop = false;
    int num = 0;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name+"enter");
        if (collision == player)
        {
            player.SendMessage("setCurrentItem", itemType);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.name + "leave");
        if (other == player)
        {
            player.SendMessage("releaseItem", itemType);
        }
    }

    public void getItem()
    {
        //һ��������ŭ��
        player.gameObject.SendMessage("angerChange", an);

    }

    public void useItem()
    {
        //������ײ��
        Destroy(gameObject.GetComponent<Collider2D>());
        gameObject.GetComponent<Renderer>().enabled = false;
        player.SendMessage("wearGlass");
        //�۾�������
    }

    public void dropItem()
    {

    }
    public void dropToWindow()
    {

        Vector2 MoveSpeed = (window.transform.position - player.transform.position).normalized * 10;
        //����һ���б������еļ���ĵ�
        Debug.Log("movespeed" + MoveSpeed);
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
