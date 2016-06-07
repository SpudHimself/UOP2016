using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
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
        //mAudioSource.PlayOneShot(mAudioClipClick, 1.0f);
        mAudioSource.clip = mAudioClipClick;
        mAudioSource.Play();

        mPlaying = true;

        mFilename = filename;
    }

    public void Quit()
    {
        mAudioSource.PlayOneShot(mAudioClipClick, 1.0f);
        Application.Quit();
    }
}