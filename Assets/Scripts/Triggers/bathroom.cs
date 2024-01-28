using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bathroom : MonoBehaviour
{

    public Collider2D player;
    private void Start()
    {
        enabled = false;
        shengqi= false;
        xizao.SetActive(true);
        yihuo.SetActive(false);
        fennu.SetActive(false);
    }
    private int clickTimes = 0;
    private float lastClickTime = 0f;
    private float clickInterval = 1f;
    private bool shengqi=false;
    private bool lights = true;
    public GameObject xizao, yihuo, fennu;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (!shengqi)
            {
                if(lights)
                {

                    yihuo.SetActive(true) ;
                    xizao .SetActive(false) ;
                    player.SendMessage("angerChange", 10);
                    // ��ȡ��ǰʱ��
                    float currentTime = Time.time;

                    // ����Ƿ���һ����
                    if (currentTime - lastClickTime < clickInterval)
                    {
                        shengqi = true;
                        // ���ӵ������
                        clickTimes++;

                        // ����Ƿ�ﵽ���λ����
                        if (clickTimes >= 3)
                        {
                            player.SendMessage("angerChange", 20);
                        }
                    }
                    else
                    {
                        // �������һ�룬���õ������
                        clickTimes = 1;
                    }

                    // ������һ�ε����ʱ��
                    lastClickTime = currentTime;
                }
                else
                {
                    yihuo.SetActive(false);
                    xizao.SetActive(true);
                }

            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player)
        {
            enabled = true;
            Debug.Log("enter");
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision == player)
        {
            enabled = false;
            Debug.Log("Leave");
        }
    }
}
