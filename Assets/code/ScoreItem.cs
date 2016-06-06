using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreItem : MonoBehaviour
{
    #region Fields
    public int points;
    public float absorbDistance;
    

    private Transform               m_transform;
    private Vector3                 m_suckDirection;

    private static List<GameObject> m_players = new List<GameObject>();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        m_transform = this.transform;
        // Gets all of the players within the current scene.
        if (m_players.Count.Equals(0))
        {
            Debug.Log("Populating players in the item.");
            GameObject[] players = GameObject.FindGameObjectsWithTag(Tags.PLAYER);

            foreach (GameObject go in players)
            {
                Debug.Log("Player added");
                m_players.Add(go);
            }
        }
    }

    private void Update()
    {
        float speed = 2.0f;

        foreach (GameObject player in m_players)
        {
            // Check the distance of each player from the item.
            float dist = Vector3.Distance(m_transform.position, player.transform.position);

            if (dist < absorbDistance)
            {
				// Make 4.0f a suck power variable.
                float s = (speed / dist) * 4.0f;

                m_transform.position = Vector3.MoveTowards(m_transform.position, player.transform.position, s * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals(Tags.PLAYER))
        {
            // Get the score component.
            ScoreManager sm = col.gameObject.GetComponent<ScoreManager>();

            // It should have a score manager though...
            if (sm)
            {
                Debug.Log("Incrementing score to ScoreManager.");
                sm.Increase(points);

                Debug.Log(sm.Score);
            }

            // Temporary step for getting rid of it.
            Destroy(this.gameObject);
        }
    }
    #endregion
}
