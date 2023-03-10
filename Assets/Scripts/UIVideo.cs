using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class UIVideo : MonoBehaviour
{
    [SerializeField] VideoPlayer _video;
    [SerializeField] UIController UIcon;


    // Update is called once per frame
    void Update()
    {
        if (UIcon.GetVideoOn())
        {
            _video.Play();
        }
        else
        {
            _video.Stop();
        }
    }
}
