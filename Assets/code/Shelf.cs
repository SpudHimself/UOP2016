using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shelf : MonoBehaviour
{
    #region Fields
    public List<GameObject> itemTypes;
    private List<ScoreItem> mScoreItems;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        SpawnItems();
    }

    private void OnLevelWasLoaded()
    {
        SpawnItems();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals(Tags.PLAYER))
        {
            Debug.Log(mScoreItems.Count);

            foreach (ScoreItem item in mScoreItems)
            {
                item.State = eItemState.ACTIVE;
            }
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
