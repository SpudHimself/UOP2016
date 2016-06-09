using UnityEngine;
using System.Collections;

public class CollisionSounds : MonoBehaviour
{
    private GameObject bush;
    public AudioSource audioSource;
    public AudioClip bushCollision;
    public AudioClip shelfCollision;
    public AudioClip cartCollision;
    public AudioClip boxCollision;

    private bool hasCollided;

    public float timer;

    // Use this for initialization
    void Start()
    {
        timer = 0.0f;

        hasCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter(Collider col)
    {
        if (!hasCollided)
        {
            if (col.tag == Tags.PLANT)
            {
                ///audioSource.clip = bushCollision;
                audioSource.PlayOneShot(bushCollision);
                hasCollided = true;
            }

            if (col.tag == Tags.SHELF)
            {
                //audioSource.clip = shelfCollision;
                audioSource.PlayOneShot(shelfCollision);
                hasCollided = true;
            }

            if (col.tag == Tags.CART)
            {
                audioSource.clip = cartCollision;
                audioSource.Play();
                hasCollided = true;
            }

            if (col.tag == Tags.BOX)
            {
                audioSource.clip = boxCollision;
                audioSource.Play();
                hasCollided = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject && timer > 1.0f)
        {
            hasCollided = false;
            timer = 0.0f;
        }
    }
}
