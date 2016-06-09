using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{
	public enum eState
	{
		Waiting,
		Alive,
		Dead
	}
	private eState mState;

    public List<AudioClip> clips;
    // PURE FILTH CODE.
    public List<Material> materials;

    public GameObject bloodEffect;

	// These 2 will have to be changed.
	private float mRoamRadius = 15f;
	private float mMoveTimer;
	private float mDeadTimer = 5.0f;

	private SkinnedMeshRenderer[] mRenderers;
    private Rigidbody[]  mRigidbodies;
    private Animator mAnimator;
    private AudioSource mAudioSource;

    public NavMeshAgent Agent { get; private set; }

	void Awake()
	{
		tag = Tags.NPC;

        Agent = this.GetComponent<NavMeshAgent>();
		mRenderers = this.GetComponentsInChildren<SkinnedMeshRenderer>();
        mRigidbodies = this.GetComponentsInChildren<Rigidbody>();

        mAnimator = this.GetComponent<Animator>();
        mAudioSource = this.GetComponent<AudioSource>();
	}

	void Start()
	{
		// This is just debug so it's fine to be first.
		Debug.Log("Start called.");

		// Leave this first.
		GameManager.Singleton().GetNPCs().Add( this );

        foreach (SkinnedMeshRenderer renderer in mRenderers)
        {
            renderer.material = materials[Random.Range(0, materials.Count)];
        }

		mMoveTimer = 0f;

        Agent.Resume();
        Agent.destination = transform.position;

        //SetState(eState.Alive);

        DisableRagdoll();
	}

	void Update()
	{
        // Debug.Log(mState);
        mAnimator.SetBool("IsMoving", Agent.velocity != Vector3.zero);

		switch ( mState )
		{
			case eState.Waiting:
				break;

			case eState.Alive:
				StateAlive();
				break;

			case eState.Dead:
				StateDead();
				break;
		}
	}

	void OnTriggerEnter ( Collider col )
	{
        if ( mState != eState.Dead && col.gameObject.CompareTag( Tags.PLAYER ) )
        {
            SetState(eState.Dead);

            Collider c = this.GetComponent<Collider>();
            Vector2 spawnPoint = transform.position;

            mAudioSource.clip = clips[Random.Range(0, clips.Count)];
            mAudioSource.Play();

            spawnPoint.y += 2.0f;

            Instantiate(bloodEffect, transform.position, transform.rotation);

            EnableRagdoll();

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.Log("Point: " + hit.point);
            }

            this.GetComponent<Collider>().enabled = false;
        }
	}

	void OnDestroy()
	{
		GameManager.Singleton().GetNPCs().Remove( this );
	}

	public eState GetState()
	{
		return mState;
	}

	public void SetState( eState state )
	{
		mState = state;

		switch ( mState )
		{
			case eState.Waiting:
                mAnimator.enabled = true;
				break;

			case eState.Alive:
                mAnimator.enabled = true;
				break;

			case eState.Dead:
                Agent.Stop();
				break;
		}
	}

	private void StateAlive()
	{
		mMoveTimer = Mathf.Max( mMoveTimer - Time.deltaTime, 0f );
        if (mMoveTimer <= 0f && Agent.remainingDistance < 0.25f)
		{
			mMoveTimer = Random.Range( 3f, 5f );
			MakeNewMovement();
		}
	}

	private void StateDead()
	{
		if ( mDeadTimer >= 0.0f )
		{
			mDeadTimer -= Time.deltaTime;
		}
		else
		{
            foreach (SkinnedMeshRenderer smr in mRenderers)
            {
                Color color = smr.material.color;

                color.a -= Time.deltaTime;

                smr.material.color = color;

                if (color.a <= 0.0f)
                {
                    Destroy(this.gameObject);
                    GameManager.Singleton().SpawnNPC();
                }
            }
		}
	}

	// Find a random new position within mRoamRadius to move towards.
	private void MakeNewMovement()
	{
		Vector2 unitCircle = Random.insideUnitCircle;
		Vector3 circle = new Vector3( unitCircle.x, 0f, unitCircle.y ) * mRoamRadius;
		circle += transform.position;

		NavMeshHit hit;
		NavMesh.SamplePosition( circle, out hit, mRoamRadius, 1 );

		if ( Vector3.Distance( transform.position, hit.position ) < 1f )
		{
			MakeNewMovement();
			return;
		}

        Agent.destination = hit.position;

		//print( "NPC is moving: " + mAgent.destination );
	}

    private void EnableRagdoll()
    {
        mAnimator.enabled = false;

        foreach (Rigidbody rb in mRigidbodies)
        {
            rb.detectCollisions = true;
            rb.isKinematic = false;
        }
    }

    private void DisableRagdoll()
    {
        foreach (Rigidbody rb in mRigidbodies)
        {
            rb.detectCollisions = false;
            rb.isKinematic = true;
        }
    }
}