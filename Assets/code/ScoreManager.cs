using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    #region Properties
    public int Score { get; set; }
	private GameObject mScorePlumPrefab;
	private Car mCar;
    #endregion

    #region Unity Methods
    private void Start()
    {
		mCar = GetComponent<Car>();

        Score = 0;

		mScorePlumPrefab = (GameObject) Resources.Load( "prefabs/ScorePlum" );
    }
    #endregion

    #region Methods
    public void Increase(int amount)
    {
        Score += amount;

		mScorePlumPrefab = (GameObject) Resources.Load( "prefabs/ScorePlum" );

		Quaternion rotation = Quaternion.LookRotation( transform.position - mCar.GetCameraPosition().transform.position, Vector3.up );
		GameObject clone = (GameObject) Instantiate( mScorePlumPrefab, transform.position, rotation );
		clone.transform.parent = transform;
		clone.GetComponent<TextMesh>().text = "" + amount;

		GetComponent<Car>().AddItems( 1 );
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
