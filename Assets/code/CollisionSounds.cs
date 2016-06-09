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

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.relativeVelocity.magnitude > 2)
        {
            if (col.collider.tag == "Plant")
            {
                audioSource.clip = bushCollision;
                audioSource.Play();
            }

            if (col.collider.tag == "Shelf")
            {
                audioSource.clip = shelfCollision;
                audioSource.Play();
            }

            if (col.collider.tag == "Cart")
            {
                audioSource.clip = cartCollision;
                audioSource.Play();
            }

            if (col.collider.tag == "Box")
            {
                audioSource.clip = boxCollision;
                audioSource.Play();
            }
        }
    }
}
