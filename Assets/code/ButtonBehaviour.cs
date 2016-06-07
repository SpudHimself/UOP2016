using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ButtonBehaviour : MonoBehaviour, ISelectHandler
{
    private ColorBlock color;
    private Button currentButton;

    // Use this for initialization
    void Start()
    {
        currentButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        color = currentButton.colors;
        color.highlightedColor = Color.Lerp(Color.blue, Color.black, Mathf.PingPong(Time.time, 0.7f));
        currentButton.colors = color;
    }

    public void OnSelect(BaseEventData eventData)
    {

    }
}
