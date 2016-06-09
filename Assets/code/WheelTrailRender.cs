using UnityEngine;
using System.Collections;

public class WheelTrailRender : MonoBehaviour
{
    public WheelCollider mCorrespondingCollider;
    public GameObject mSkidmarkTrailRenderer;
    public GameObject mSmokeParticles;


    // Use this for initialization
    void Start()
    {
        mSkidmarkTrailRenderer.gameObject.SetActive(false);
        mSmokeParticles.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Vector3 ColliderCenterPoint = mCorrespondingCollider.transform.TransformPoint(mCorrespondingCollider.center);

        if (Physics.Raycast(ColliderCenterPoint, -mCorrespondingCollider.transform.up, out hit, mCorrespondingCollider.suspensionDistance + mCorrespondingCollider.radius))
        {
            //transform.position = hit.point + (mCorrespondingCollider.transform.up * mCorrespondingCollider.radius);
        }
        else
        {
            //transform.position = ColliderCenterPoint - (mCorrespondingCollider.transform.up * mCorrespondingCollider.suspensionDistance);
        }

        WheelHit correspondingGroundHit;
        mCorrespondingCollider.GetGroundHit(out correspondingGroundHit);

        if (Mathf.Abs(correspondingGroundHit.sidewaysSlip) > 0.6f)
        {
            mSkidmarkTrailRenderer.gameObject.SetActive(true);
            mSmokeParticles.gameObject.SetActive(true);
        }
        else if (Mathf.Abs(correspondingGroundHit.sidewaysSlip) <= 0.55f)
        {
            mSkidmarkTrailRenderer.gameObject.SetActive(false);
            mSmokeParticles.gameObject.SetActive(false);
        }
            
    }
}
