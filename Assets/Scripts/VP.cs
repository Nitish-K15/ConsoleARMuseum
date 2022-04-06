using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class VP : MonoBehaviour
{
    private VideoPlayer video;
    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoPlayer>();
    }

    public void PlayVideo()
    {
        video.Play();
    }

    public void StopVideo()
    {
        video.Stop();
    }
}
