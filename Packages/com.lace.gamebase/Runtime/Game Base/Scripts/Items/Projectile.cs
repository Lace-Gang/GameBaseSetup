using System.Collections;
using UnityEngine;

namespace GameBase
{
    public abstract class Projectile : MonoBehaviour
    {
        //Hidden Variables
        Coroutine m_lifeTimer = null;       //keeps reference DestroyAfterLifetime Coroutine


        //Exposed Variables
        [Header("Projectile Base Components")]
        [Tooltip("Reference to this projectile's collider")]
        [SerializeField] protected Collider m_collider;
        [Tooltip("Reference to this projectile's mesh renderer")]
        [SerializeField] protected Renderer m_renderer;


        [Header("Projectile Base Information")]
        [Tooltip("What is the lifespan of this object in seconds")]
        [SerializeField] protected float m_lifespan = 5f;
        [Tooltip("Should the projectile be destroyed when this projectile hits something")]
        [SerializeField] protected bool m_destroyOnHit = true;
        [Tooltip("Should the projectile play audio when this projectile hits something")]
        [SerializeField] protected bool m_playAudioOnHit = false;
        [Tooltip("What audio should be played when this projectile hits something")]
        [SerializeField] protected AudioClip m_hitAudio;



        public float GetLifespan() { return m_lifespan; }  //Allows other scripts to see this projectile's lifespan
        public void SetLifespan(float lifespan) { m_lifespan = lifespan; }   //Allows other scripts to set this projectile's lifespan



        
        /// <summary>
        /// After hitting another collider, plays audio (if indicated to do so), and destroys this projectile (if indicated to do so)
        /// </summary>
        /// <param name="other">The collider that was hit (handled by game engine)</param>
        private void OnTriggerEnter(Collider other)
        {   
            //plays audio at the location where the projectile hit (if inidcated to do so)
            if(m_playAudioOnHit)
            {
                GameInstance.Instance.SpawnSoundAtLocation(m_hitAudio, transform.position);
            }

            //destroys this projectile (if indicated to do so)
            if(m_destroyOnHit)
            {
                GameObject.Destroy(this.gameObject);
            }
            
        }



        /// <summary>
        /// Starts the lifetime timer. The projectile will be destroyed after its lifespan lapses
        /// </summary>
        public void StartLifetimeTimer()
        {
            m_lifeTimer = StartCoroutine(DestroyAfterLifetime());   //starts coroutine for lifetimer & object destruction
        }

        /// <summary>
        /// Stops the lifetime timer
        /// </summary>
        public void StopLifetimeTimer()
        {
            if(m_lifeTimer != null)     //if a lifeTimer coroutine is being tracked
            {
                StopCoroutine(m_lifeTimer);     //stops coroutine for lifetimer & object destruction
                m_lifeTimer = null;     //stops tracking the cancelled lifeTimer coroutine
            }
        }

        /// <summary>
        /// Destroys this projectile game object after the lifespan lapses
        /// </summary>
        /// <returns>Yield return for Coroutine</returns>
        public IEnumerator DestroyAfterLifetime()
        {
            yield return new WaitForSeconds(m_lifespan);    //waits until the lifetime lapses

            GameObject.Destroy(gameObject);     //destroys this game object
        }



        /// <summary>
        /// Sets the trajectory of this projectile
        /// </summary>
        /// <param name="direction">Direction projectile should move</param>
        /// <param name="velocity">Desired Velocity of the projectile</param>
        public abstract void SetTrajectory(Vector3 direction, float velocity);
    }
}
