using UnityEngine;
using System.Collections;

public enum ePowerUpType
{
    SPEED_BOOST
}

public class PowerUpItem : MonoBehaviour
{
    #region Fields
    [Header("Power-up basics")]
    public ePowerUpType type;
    public float modifier;

    [Header("Effect")]
    public float modifyTime;

    private Car mCar;
    #endregion

    #region Unity Methods
    private void Update()
    {
        if (mCar)
        {
            if (modifyTime > 0.0f)
            {
                modifier -= Time.deltaTime;
            }
            else
            {

            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals(Tags.PLAYER))
        {
            switch (type)
            {
                case ePowerUpType.SPEED_BOOST:
                    mCar = col.gameObject.GetComponent<Car>();
                    mCar.mMaxMotorTorque *= modifier;

                    this.GetComponent<Renderer>().enabled = false;
                    this.GetComponent<Collider>().enabled = false;

                    break;
            }
        }
    }
    #endregion

    #region Methods
    #endregion
}