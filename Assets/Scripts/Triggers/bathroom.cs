using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bathroom : MonoBehaviour
{

    public Collider2D player;
    private AudioSource audioS;
    private void Start()
    {
        enabled = false;
        shengqi= false;
        xizao.SetActive(true);
        yihuo.SetActive(false);
        fennu.SetActive(false);
        heimu.SetActive(false);
        off.SetActive(false);
        audioS=GetComponent<AudioSource>();
    }
    private int clickTimes = 0;
    private float lastClickTime = 0f;
    private float clickInterval = 5f;
    private bool shengqi=false;
    private bool lights = true;
    public GameObject xizao, yihuo, fennu,heimu,on,off;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            audioS.Play();
            if (lights)
            {
                if (!shengqi)
                {
                    yihuo.SetActive(true);
                    xizao.SetActive(false);
                }
                heimu.SetActive(true);
                off.SetActive(true);
                on.SetActive(false);
                player.SendMessage("angerChange", 10);
                player.SendMessage("interact");
                // ��ȡ��ǰʱ��
                float currentTime = Time.time;

                // ����Ƿ���һ����
                if (currentTime - lastClickTime < clickInterval && !shengqi)
                {

                    // ���ӵ������
                    clickTimes++;

                    // ����Ƿ�ﵽ���λ����
                    if (clickTimes >= 3)
                    {
                        player.SendMessage("angerChange", 20);
                        Debug.Log("����");
                        shengqi = true;
                        fennu.SetActive(true);
                        heimu.SetActive(true); yihuo.SetActive(false); xizao.SetActive(false);
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
                if (!shengqi)
                {
                    yihuo.SetActive(false);
                    xizao.SetActive(true);
                }
                off.SetActive(false);
                on.SetActive(true);
                heimu.SetActive(false);
            }
            lights = !lights;
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
