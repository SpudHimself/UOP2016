using UnityEngine;
using System.Collections;

public class CollisionSounds : MonoBehaviour
{
    private GameObject bush;
    public AudioSource audioSource;
    public AudioClip bushCollision;
    public AudioClip bushCollision1;
    public AudioClip bushCollision2;
    public AudioClip shelfCollision;
    public AudioClip shelfCollision1;
    public AudioClip shelfCollision2;
    public AudioClip cartCollision;
    public AudioClip cartCollision1;
    public AudioClip cartCollision2;
    public AudioClip boxCollision;
    public AudioClip wallCollision;
    public AudioClip wallCollision1;
    public AudioClip wallCollision2;

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
        float random = Random.Range(1, 3);

        if (!hasCollided)
        {
            if (col.tag == Tags.PLANT)
            {
                if (random == 1)
                    audioSource.PlayOneShot(bushCollision);

                else if (random == 2)
                    audioSource.PlayOneShot(bushCollision1);

                else
                    audioSource.PlayOneShot(bushCollision2);
                hasCollided = true;
            }

            if (col.tag == Tags.SHELF)
            {
                if (random == 1)
                    audioSource.PlayOneShot(shelfCollision);

                else if (random == 2)
                    audioSource.PlayOneShot(shelfCollision1);

                else
                    audioSource.PlayOneShot(shelfCollision2);
                hasCollided = true;
            }

            if (col.tag == Tags.CART)
            {
                if (random == 1)
                    audioSource.PlayOneShot(cartCollision);

                else if (random == 2)
                    audioSource.PlayOneShot(cartCollision1);

                else
                    audioSource.PlayOneShot(cartCollision2);
                hasCollided = true;
            }

            if (col.tag == Tags.BOX)
            {
                audioSource.clip = boxCollision;
                audioSource.Play();
                hasCollided = true;
            }

            if (col.tag == Tags.WALL)
            {
                if (random == 1)
                    audioSource.PlayOneShot(wallCollision);

                else if (random == 2)
                    audioSource.PlayOneShot(wallCollision1);

                else
                    audioSource.PlayOneShot(wallCollision2);
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
