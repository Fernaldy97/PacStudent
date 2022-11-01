using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //If collides with a wall
        if (other.gameObject.CompareTag("Impassable"))
        {
            //Get particle system
            ParticleSystem collisionParticles = gameObject.GetComponent<ParticleSystem>();
            //Get collision sound
            AudioSource collisionSound = gameObject.GetComponent<AudioSource>();
            //Emit particles
            collisionParticles.Emit(100);
            //Play collision sound
            collisionSound.Play();
        }
    }
}
