using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{

    public string nextSceneName;
    public float timeDuration; // �������ʱ��
    public Button startGameButton;
    public GameObject backgroundToMove;
    public float moveLength;
    void Start()
    {
        startGameButton.onClick.AddListener(Click);
    }
    public void Click()
    {
        StartCoroutine(changeCoroutine());
    }

    public IEnumerator changeCoroutine()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = backgroundToMove.transform.position; // ע������Ļ�ȡ��ʽ

        while (elapsedTime < timeDuration)
        {
            // ���㵱ǰ֡��ƽ�ƾ���
            float t = elapsedTime / timeDuration;
            float currentMove = Mathf.Lerp(0, moveLength, t);

            // �����µ� Y ����
            float newY = initialPosition.y + currentMove;

            // ʹ���µ�λ�ø�������� Y ����
            backgroundToMove.transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);

            // �ȴ�����һ֡
            yield return null;

            // �����Ѿ�������ʱ��
            elapsedTime += Time.deltaTime;
        }

        // ȷ���� timeDuration ���ƶ���ָ��λ��
        backgroundToMove.transform.position = new Vector3(initialPosition.x, initialPosition.y + moveLength, initialPosition.z);

        SceneManager.LoadScene(nextSceneName);

    }

}
