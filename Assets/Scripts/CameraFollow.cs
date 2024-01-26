using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector2 offset;
    public float opacity;
    public float fadeDuration = 1.0f;    // �������ʱ��
    private bool fading=false;
    private float currentOpacity;        // ��ǰ͸����ֵ
    private float timeElapsed;           // �Ѿ�����ʱ��
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

            // �������������ת�����Ը�����Ҫ���е���
            //transform.LookAt(player.position);
        }
        Ray ray = Camera.main.ScreenPointToRay(transform.position);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            // ������߻���������
            Debug.Log("���߻��������壺" + hitInfo.collider.gameObject.name);
            spriteRenderer = hitInfo.collider.GetComponent<SpriteRenderer>();
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
            fading = true;
        }
        if(!fading)
        {
            if(timeElapsed > 0)
            {
                float newOpacity = Mathf.Lerp(1f, opacity, timeElapsed/fadeDuration);

                // ���� SpriteRenderer ��͸����
                Color newColor = spriteRenderer.color;
                newColor.a = newOpacity;
                spriteRenderer.color = newColor;

                // �����Ѿ�����ʱ��
                timeElapsed -= Time.deltaTime;
            }
        }
    }
}
