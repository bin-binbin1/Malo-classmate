using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couple : MonoBehaviour
{
    public Collider2D player;
    private Vector3 originLocation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
           

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
    IEnumerator coupleDivide()
    {
        originLocation=transform.position;
        //猴子到两人中间
        //右边那个人平移，到最右边，猴子瞬移到中间
        yield return null;


        //等待2s
        transform.position = originLocation;
    }
}
