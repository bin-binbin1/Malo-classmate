using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 指定VideoPlayer组件
    public string nextSceneName; // 指定下一个场景的名称

    void Start()
    {
        // 添加播放结束事件的监听器
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // 加载下一个场景
        SceneManager.LoadScene(nextSceneName);
    }
}
