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
    }
    private int clickTimes = 0;
    private float lastClickTime = 0f;
    private float clickInterval = 1f;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            player.SendMessage("angerChange", 10);
            // 获取当前时间
            float currentTime = Time.time;

            // 检查是否在一秒内
            if (currentTime - lastClickTime < clickInterval)
            {
                // 增加点击次数
                clickTimes++;

                // 检查是否达到三次或更多
                if (clickTimes >= 3)
                {
                    player.SendMessage("angerChange", 20);
                }
            }
            else
            {
                // 如果超过一秒，重置点击次数
                clickTimes = 1;
            }

            // 更新上一次点击的时间
            lastClickTime = currentTime;
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
