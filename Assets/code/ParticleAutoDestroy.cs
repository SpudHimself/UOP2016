using UnityEngine;
using System.Collections;

#region Requirements
[RequireComponent(typeof(ParticleSystem))]
#endregion

public class ParticleAutoDestroy : MonoBehaviour
{
    #region Fields
    private ParticleSystem mSystem;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        mSystem = this.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!mSystem.IsAlive())
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}
