using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class BackButtonBehaviour : MonoBehaviour
{
    public Button currentButton;
    public EventSystem eventSystem;

    public AudioSource mAudioSource;
    public AudioClip mAudioClipClick;

    private float waitTime;
    private bool mPlaying;
    private string mFilename;

    // Use this for initialization
    void Start()
    {
        mPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mPlaying)
        {
            Debug.Log(mAudioSource.isPlaying);

            if (!mAudioSource.isPlaying)
            {
                Application.LoadLevel(mFilename);
            }
        }
    }

    public void LoadLevel(string filename)
    {
        mAudioSource.clip = mAudioClipClick;
        mAudioSource.Play();

        mPlaying = true;

        mFilename = filename;
    }
}
