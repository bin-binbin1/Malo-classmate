using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fade : MonoBehaviour
{
    public Vector2 offset;
    public float opacity;
    public float fadeDuration = 1.0f;    // �������ʱ��
    private float timeElapsed;           // �Ѿ�����ʱ��
    private SpriteRenderer spriteRenderer;
    private float originOpacity;
    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer= GetComponent<SpriteRenderer>();
        originOpacity = spriteRenderer.color.a;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        enabled = true;
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        Color currentColor = spriteRenderer.color;

        // �����µ�͸����ֵ
        currentColor.a = opacity;

        // ���µ���ɫӦ�õ� SpriteRenderer
        spriteRenderer.color = currentColor;

        if (timeElapsed < fadeDuration)
        {
            // ʹ�� Lerp ��������͸���ȵĽ���
            float t = timeElapsed / fadeDuration;
            float newOpacity = Mathf.Lerp(1f, opacity, t);

            // ���� SpriteRenderer ��͸����
            Color newColor = spriteRenderer.color;
            newColor.a = newOpacity;
            spriteRenderer.color = newColor;

            // �����Ѿ�����ʱ��
            timeElapsed += Time.deltaTime;
        }

    }
    // Update is called once per frame
    void Update()
    {


        Color currentColor = spriteRenderer.color;

        // �����µ�͸����ֵ
        currentColor.a = opacity;

        // ���µ���ɫӦ�õ� SpriteRenderer
        spriteRenderer.color = currentColor;
        Color newColor = spriteRenderer.color;

        if (timeElapsed > 0)
        {
            float newOpacity = Mathf.Lerp(1f, opacity, timeElapsed / fadeDuration);

            // ���� SpriteRenderer ��͸����
            
            newColor.a = newOpacity;
            spriteRenderer.color = newColor;

            // �����Ѿ�����ʱ��
            timeElapsed -= Time.deltaTime;
        }
        else
        {
            newColor.a = originOpacity;
            spriteRenderer.color = newColor;
            enabled = false;
        }

    }
}
