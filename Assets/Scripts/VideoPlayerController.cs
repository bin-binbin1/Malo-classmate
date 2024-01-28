using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // ָ��VideoPlayer���
    public string nextSceneName; // ָ����һ������������

    void Start()
    {
        // ��Ӳ��Ž����¼��ļ�����
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // ������һ������
        SceneManager.LoadScene(nextSceneName);
    }
}
