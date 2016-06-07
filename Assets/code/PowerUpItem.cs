using UnityEngine;
using System.Collections;

public enum ePowerUpType
{
    SPEED_BOOST
}

public class PowerUpItem : MonoBehaviour
{
    #region Fields
    public ePowerUpType type;
    public float modifier;
    #endregion

    #region Unity Methods
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals(Tags.PLAYER))
        {
            switch (type)
            {
                case ePowerUpType.SPEED_BOOST:
                    // TODO: Move player faster when collided with.
                    break;
            }
        }
    }
    #endregion

    #region Methods
    #endregion
}