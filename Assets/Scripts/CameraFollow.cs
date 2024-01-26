using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector2 offset;
    public float opacity;
    public float fadeDuration = 1.0f;    // 渐变持续时间
    private bool fading=false;
    private float currentOpacity;        // 当前透明度值
    private float timeElapsed;           // 已经过的时间
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 newPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            transform.position = newPosition;

            // 设置摄像机的旋转，可以根据需要自行调整
            //transform.LookAt(player.position);
        }
        Ray ray = Camera.main.ScreenPointToRay(transform.position);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            // 如果射线击中了物体
            Debug.Log("射线击中了物体：" + hitInfo.collider.gameObject.name);
            spriteRenderer = hitInfo.collider.GetComponent<SpriteRenderer>();
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
            fading = true;
        }
        if(!fading)
        {
            if(timeElapsed > 0)
            {
                float newOpacity = Mathf.Lerp(1f, opacity, timeElapsed/fadeDuration);

                // 更新 SpriteRenderer 的透明度
                Color newColor = spriteRenderer.color;
                newColor.a = newOpacity;
                spriteRenderer.color = newColor;

                // 更新已经过的时间
                timeElapsed -= Time.deltaTime;
            }
        }
    }
}
