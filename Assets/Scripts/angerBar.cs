using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angerBar : MonoBehaviour
{
    public SpriteRenderer leftBar;

    public float progress;

    // ��ʼʱ��¼��� SpriteRenderer ��λ�úʹ�С
    private Vector3 leftInitialPosition;
    private Vector3 leftInitialScale;

    void Start()
    {
        // ��¼��� SpriteRenderer ��λ�úʹ�С
        leftInitialPosition = leftBar.transform.localPosition;
        leftInitialScale = leftBar.transform.localScale;
    }

    void Update()
    {
        progress += Time.deltaTime/10f;
        // ���ƽ��ȷ�Χ�� 0 �� 1 ֮��
        progress = Mathf.Clamp01(progress);
   
        // ��������������λ�úʹ�С
        leftBar.transform.localScale = new Vector3(leftInitialScale.x,leftInitialScale.y*progress,leftInitialScale.z);
        float deltaY = (1 - progress) * leftInitialScale.y / 2;
        leftBar.transform.localPosition = new Vector3(leftInitialPosition.x, leftInitialPosition.y + deltaY, leftInitialPosition.z);
    }
}
