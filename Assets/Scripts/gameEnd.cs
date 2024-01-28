using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameEnd : MonoBehaviour
{
    private Image[] childs;
    public float fadeDuration;
    public AudioSource audioS;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnEnable()
    {
        Destroy(audioS);
        childs = GetComponentsInChildren<Image>();
        foreach (var child in childs)
        {
            Color c= child.color;
            c.a = 0;
        }
        StartCoroutine(fadeOut());
    }
    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene("OpenEye");
        }else if (Input.GetKeyUp(KeyCode.T))
        {
            Application.Quit();
        }
    }
    IEnumerator fadeOut()
    {
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            // 使用 Lerp 函数进行透明度的渐变
            float t = timeElapsed / fadeDuration;
            float newOpacity = Mathf.Lerp(0f, 1f, t);

            // 更新 SpriteRenderer 的透明度
            foreach (var child in childs) {
                Color newColor = child.color;
                newColor.a = newOpacity;
                child.color = newColor;
            }
            Debug.Log(newOpacity);

            // 更新已经过的时间
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        foreach (var child in childs)
        {
            Color newColor = child.color;
            newColor.a = 1f;
            child.color = newColor;
        }
    }
}
