using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angerBar : MonoBehaviour
{
    public SpriteRenderer leftBar;

    public float progress;

    // 初始时记录左侧 SpriteRenderer 的位置和大小
    private Vector3 leftInitialPosition;
    private Vector3 leftInitialScale;

    void Start()
    {
        // 记录左侧 SpriteRenderer 的位置和大小
        leftInitialPosition = leftBar.transform.localPosition;
        leftInitialScale = leftBar.transform.localScale;
    }

    void Update()
    {
        progress += Time.deltaTime/10f;
        // 限制进度范围在 0 到 1 之间
        progress = Mathf.Clamp01(progress);
   
        // 调整左侧进度条的位置和大小
        leftBar.transform.localScale = new Vector3(leftInitialScale.x,leftInitialScale.y*progress,leftInitialScale.z);
        float deltaY = (1 - progress) * leftInitialScale.y / 2;
        leftBar.transform.localPosition = new Vector3(leftInitialPosition.x, leftInitialPosition.y + deltaY, leftInitialPosition.z);
    }
}
