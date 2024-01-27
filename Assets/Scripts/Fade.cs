using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fade : MonoBehaviour
{
    public Vector2 offset;
    public float opacity;
    public float fadeDuration = 1.0f;    // 渐变持续时间
    private float timeElapsed;           // 已经过的时间
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

        // 设置新的透明度值
        currentColor.a = opacity;

        // 将新的颜色应用到 SpriteRenderer
        spriteRenderer.color = currentColor;

        if (timeElapsed < fadeDuration)
        {
            // 使用 Lerp 函数进行透明度的渐变
            float t = timeElapsed / fadeDuration;
            float newOpacity = Mathf.Lerp(1f, opacity, t);

            // 更新 SpriteRenderer 的透明度
            Color newColor = spriteRenderer.color;
            newColor.a = newOpacity;
            spriteRenderer.color = newColor;

            // 更新已经过的时间
            timeElapsed += Time.deltaTime;
        }

    }
    // Update is called once per frame
    void Update()
    {


        Color currentColor = spriteRenderer.color;

        // 设置新的透明度值
        currentColor.a = opacity;

        // 将新的颜色应用到 SpriteRenderer
        spriteRenderer.color = currentColor;
        Color newColor = spriteRenderer.color;

        if (timeElapsed > 0)
        {
            float newOpacity = Mathf.Lerp(1f, opacity, timeElapsed / fadeDuration);

            // 更新 SpriteRenderer 的透明度
            
            newColor.a = newOpacity;
            spriteRenderer.color = newColor;

            // 更新已经过的时间
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
