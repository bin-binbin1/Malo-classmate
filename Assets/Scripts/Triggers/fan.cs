using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fan : MonoBehaviour
{
    public Collider2D player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            //����������
            
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == player)
        {
            enabled = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == player)
        {
            enabled = false;

        }
    }
    IEnumerator fanDrop()
    {
        //ƽ�ƣ����ײ��滻���������
        yield return null;
        

        gameObject.SetActive(false);
    }
}
