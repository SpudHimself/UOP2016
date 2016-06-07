using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
    #region Fields
    [Header("Player")]
    public Transform player;
    public Transform followPoint;

    [Header("Camera Movement")]
    public float followSpeed;
    public float distance;

    private Transform m_transform;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        m_transform = this.transform;
    }

    private void LateUpdate()
    {
        m_transform.position = Vector3.Lerp(m_transform.position, followPoint.position, followSpeed * Time.deltaTime);
        m_transform.LookAt(player.position);
    }
    #endregion
}
