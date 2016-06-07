using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreItem : MonoBehaviour
{
	#region Fields
	public int points;
	public float absorbDistance;

	private Transform mTransform;
	private Vector3 mSuckDirection;

	private float mSpeed;
	private float mSuckPower;
	#endregion

	#region Unity Methods
	private void Start()
	{
		mTransform = this.transform;
		mSpeed = 2f;
		mSuckPower = 4f;
	}

	private void Update()
	{
		foreach ( Car player in GameManager.Singleton().GetPlayers() )
		{
			// Check the distance of each player from the item.
			float dist = Vector3.Distance( mTransform.position, player.transform.position );

			if ( dist < absorbDistance )
			{
				float suckSpeed = ( mSpeed / dist ) * mSuckPower;
				mTransform.position = Vector3.MoveTowards( mTransform.position, player.transform.position, suckSpeed * Time.deltaTime );
			}
		}
	}

	private void OnCollisionEnter( Collision col )
	{
		if ( col.gameObject.tag.Equals( Tags.PLAYER ) )
		{
			// Get the score component.
			ScoreManager sm = col.gameObject.GetComponent<ScoreManager>();

			// It should have a score manager though...
			if ( sm )
			{
// 				Debug.Log("Incrementing score to ScoreManager.");
				sm.Increase( points );

//              Debug.Log(sm.Score);
			}

			// Temporary step for getting rid of it.
			Destroy( this.gameObject );
		}
	}
	#endregion
}
