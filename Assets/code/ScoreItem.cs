using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreItem : MonoBehaviour
{
    #region Fields
    public int points;
    public float absorbDistance;

    private Transform               m_transform;
    private Rigidbody               m_rb;
    private float                   m_suckSpeed;
    private Vector3                 m_suckDirection;

    private static List<GameObject> m_players = new List<GameObject>();
    #endregion

    #region Unity Methods
    private void Awake()
    {
        m_transform = this.transform;
        m_rb = this.GetComponent<Rigidbody>();

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
        foreach (GameObject player in m_players)
        {
            // Check the distance of each player from the item.
            if (Vector3.Distance(m_transform.position, player.transform.position) < absorbDistance)
            {
                Debug.Log("In distance to suck.");
                m_suckDirection = player.transform.position - m_transform.position; 
            }
        }
    }

    private void FixedUpdate()
    {
        //m_rb.velocity = m_suckDirection;
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
