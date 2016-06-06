using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadLevel(string filename)
    {
        Application.LoadLevel(filename);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
