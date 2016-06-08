using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ButtonBehaviour : MonoBehaviour, IDeselectHandler
{
    private ColorBlock color;
    private Button currentButton;
    public Button backButton;

    public AudioSource m_audioSource;
    public AudioClip m_audioClipScroll;

    // Use this for initialization
    void Start()
    {
        currentButton = GetComponent<Button>();

        //DontDestroyOnLoad(m_audioSource);
    }

    // Update is called once per frame
    void Update()
    {
        color = currentButton.colors;
        color.highlightedColor = Color.Lerp(Color.blue, Color.black, Mathf.PingPong(Time.time, 0.7f));
        currentButton.colors = color;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (!backButton)
            m_audioSource.PlayOneShot(m_audioClipScroll, 1.0f);
    }
}
