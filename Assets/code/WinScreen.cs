using UnityEngine;
using System.Collections;

public class WinScreen : MonoBehaviour
{
    //public GameManager gm;
    private static float timer;

    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.eState state = GameManager.Singleton().GetState();

		timer = GameManager.Singleton().GetGameTime();

        if (timer <= 0f)
        {
            gameObject.SetActive(true);
        }

        else
            gameObject.SetActive(false);
    }
}
