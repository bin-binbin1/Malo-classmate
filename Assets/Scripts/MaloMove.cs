using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaloMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;
    }


}
