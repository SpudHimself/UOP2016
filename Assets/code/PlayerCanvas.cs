using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCanvas : MonoBehaviour
{
    #region Fields
    public GameObject player;

    private Canvas mCanvas;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        mCanvas = this.GetComponent<Canvas>();
    }

    private void Update()
    {

    }
    #endregion

    #region Methods
    private void AddScoreText()
    {

    }
    #endregion
}
