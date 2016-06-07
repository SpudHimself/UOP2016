using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    #region Properties
    public int Score { get; set; }
    #endregion

    #region Unity Methods
    private void Start()
    {
        Score = 0;
    }
    #endregion

    #region Methods
    public void Increase(int amount)
    {
        Score += amount;
    }

    public void Decrease(int amount)
    {
        Score -= amount;
    }

    public void Reset()
    {
        Score = 0;
    }
    #endregion
}
