using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StressTestNPC : MonoBehaviour
{
    #region Fields
    private Rigidbody[] m_rbs;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        m_rbs = this.GetComponentsInChildren<Rigidbody>();
        Debug.Log(m_rbs.Length);

        this.DisableRagdoll();
    }

    private void OnCollisionEnter(Collision col)
    {
        float value = 50.0f;
        Debug.Log("Collision!");

        if (col.gameObject.CompareTag(Tags.PLAYER))
        {
            Car car = col.gameObject.GetComponent<Car>();

            Vector3 dir = col.contacts[0].point;
            dir.y = 5.0f;

            this.EnableRagdoll();
            this.GetComponent<Rigidbody>().AddForce(dir * (car.Motor / value));
        }
    }
    #endregion

    #region Methods
    private void EnableRagdoll()
    {
        foreach (Rigidbody rb in m_rbs)
        {
            rb.isKinematic = false;
        }
    }

    private void DisableRagdoll()
    {
        foreach (Rigidbody rb in m_rbs)
        {
            rb.isKinematic = true;
        }
    }
    #endregion
}
