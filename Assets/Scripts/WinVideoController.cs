using UnityEngine;
using UnityEngine.Video;

public class WinVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public void PlayWinVideo()
    {
        if (videoPlayer == null) return;

        
        videoPlayer.Stop();
        videoPlayer.Play();

        Debug.Log("PlayWinVideo called");
    }
}

