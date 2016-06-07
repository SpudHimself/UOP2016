using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    #region Fields
    [Header("Player")]
    public GameObject player;
    public Transform followPoint;

    [Header("Camera Movement")]
    public float followSpeed;
    public float distance;

    private Transform m_transform;
    private Car mPlayerCar;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        m_transform = this.transform;
        mPlayerCar = player.GetComponent<Car>();
    }

    private void LateUpdate()
    {
        float arbitraryValue = 100.0f;

        m_transform.position = Vector3.Lerp(m_transform.position, followPoint.position, arbitraryValue * Time.deltaTime);
        m_transform.LookAt(player.transform.position);
    }
    #endregion
}
