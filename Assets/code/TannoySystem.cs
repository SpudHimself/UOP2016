using UnityEngine;
using System.Collections.Generic;

#region Requirements
[RequireComponent(typeof(AudioSource))]
#endregion

public class TannoySystem : MonoBehaviour
{
    #region Fields
    public List<AudioClip> announcements;

    private AudioSource mAudioSource;

    private float mMinWaitTime = 7.5f;
    private float mMaxWaitTime = 15.0f;
    private float mTimer;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        mAudioSource = this.GetComponent<AudioSource>();
    }

    private void Start()
    {
        mTimer = Random.Range(mMinWaitTime, mMaxWaitTime);
    }

    private void Update()
    {
        if (mTimer >= 0.0f)
        {
            mTimer -= Time.deltaTime;
        }
        else
        {
            if (!mAudioSource.isPlaying)
            {
                mAudioSource.clip = announcements[Random.Range(0, announcements.Count)];
                mAudioSource.Play();
            }
        }
    }
    #endregion
}
