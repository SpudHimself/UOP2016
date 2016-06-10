using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shelf : MonoBehaviour
{
    #region Fields
    public List<GameObject> itemTypes;
    private List<ScoreItem> mScoreItems;

	private Rigidbody mRigidbody;
	private Collider mCollider;
    #endregion

    #region Unity Methods
    private void Awake()
    {
		mRigidbody = GetComponent<Rigidbody>();
		mRigidbody.constraints = RigidbodyConstraints.FreezePosition;

		mCollider = GetComponent<Collider>();

        SpawnItems();
    }

    private void OnLevelWasLoaded()
    {
        SpawnItems();
    }

    private void OnCollisionEnter(Collision col)
    {
        switch ( col.gameObject.tag )
		{
			case Tags.PLAYER:
			case Tags.SHELF:
				{
					mRigidbody.constraints = RigidbodyConstraints.None;

// 					Debug.Log(mScoreItems.Count);

					foreach ( ScoreItem item in mScoreItems )
					{
						item.SetState( eItemState.ACTIVE );
					}
				}
				break;

			case Tags.ITEM:
				Physics.IgnoreCollision( mCollider, col.collider );
				break;
		}
    }
    #endregion

    #region Methods
    private void SpawnItems()
    {
        if (mScoreItems == null)
        {
            mScoreItems = new List<ScoreItem>();
        }

        foreach(Transform child in transform)
        {
            if (child.CompareTag(Tags.SHELF_SPAWN))
            {
                GameObject go = itemTypes[Random.Range(0, itemTypes.Count)];
                GameObject item = Instantiate(go, child.position, child.rotation) as GameObject;

                mScoreItems.Add(item.GetComponent<ScoreItem>());
            }
        }
    }
    #endregion
}
