using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wig : MonoBehaviour
{

    public Collider2D player;
    public int itemType = 0;
    public float timer = 1;
    public Sprite pi;
    public int an = 60;
    public GameObject window;
    List<Vector2> pointList = new List<Vector2>();
    bool isdrop = false;
    int num = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    public void getItem()
    {

        player.gameObject.SendMessage("angerChange", an);

    }
    public void useItem()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        //������ײ��
        Invoke("usebook", timer);
    }

    public void dropItem()
    {
        
        Debug.Log("ֹͣ");
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
