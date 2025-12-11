using UnityEngine;
using UnityEngine.Video;

public class WinVideoController : MonoBehaviour
{
    [Header("Video Settings")]
    public VideoPlayer videoPlayer;

    [Tooltip("If > 0, video will auto-stop at this time (in seconds).")]
    public float cutTime = 0f;   

    [Tooltip("Resume background music after video finishes or is cut.")]
    public bool resumeMusicAfterVideo = true;

    private bool cutting = false;

    void Start()
    {
        if (videoPlayer != null)
        {
            // When video reaches true end, callback
            videoPlayer.loopPointReached += OnVideoFinished;
        }
    }

    public void PlayWinVideo()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("WinVideoController: VideoPlayer is NOT assigned!");
            return;
        }

        //  Stop background music BEFORE playing video 
        if (AudioManager.Instance != null && AudioManager.Instance.longMusicSource != null)
        {
            AudioManager.Instance.longMusicSource.Stop();
        }

        // Reset video & play
        videoPlayer.time = 0;
        videoPlayer.Stop();
        videoPlayer.Play();

        cutting = (cutTime > 0);

        Debug.Log("WinVideoController: PlayWinVideo triggered");
    }

    void Update()
    {
        // Custom cut-time shortening
        if (cutting && videoPlayer.isPlaying)
        {
            if (videoPlayer.time >= cutTime)
            {
                videoPlayer.Stop();
                cutting = false;

                if (resumeMusicAfterVideo)
                    ResumeBackgroundMusic();

                Debug.Log("WinVideoController: Video cut early at " + cutTime + " sec");
            }
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        if (resumeMusicAfterVideo)
            ResumeBackgroundMusic();

        Debug.Log("WinVideoController: Video reached natural end");
    }

    private void ResumeBackgroundMusic()
    {
        if (AudioManager.Instance != null && AudioManager.Instance.longMusicSource != null)
        {
            AudioManager.Instance.PlayLongMusic();
        }
    }
}

