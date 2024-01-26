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
    private void Start()
    {
        startGameButton.onClick.AddListener(Click);
    }
    private void Click()
    {
        changeCoroutine();
    }

    private IEnumerator changeCoroutine()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = backgroundToMove.GetComponent<Transform>().transform.position;

        while (elapsedTime < timeDuration)
        {
            // ���㵱ǰ֡��ƽ�ƾ���
            float t = elapsedTime / timeDuration;
            float currentMove = Mathf.Lerp(0, moveLength, t);

            // ʹ��Translate����ƽ������
            transform.Translate(Vector3.up * currentMove * Time.deltaTime);

            // �ȴ�����һ֡
            yield return null;

            // �����Ѿ�������ʱ��
            elapsedTime += Time.deltaTime;
        }

        // ȷ����timeDuration���ƶ���ָ��λ��
        transform.position = initialPosition + Vector3.up * moveLength;

        SceneManager.LoadScene(nextSceneName);
    }

}
