using System.Collections;
using UnityEngine;

namespace GameBase
{
    public abstract class Projectile : MonoBehaviour
    {
        //Hidden Variables
        Coroutine m_lifeTimer = null;


        //Exposed Variables
        [Header("Projectile Base Components")]

        [SerializeField] protected Collider m_collider;
        [SerializeField] protected Renderer m_renderer;


        [Header("Projectile Base Information")]
        //[Tooltip("Does this projectile have a max lifespan")]
        //[SerializeField] protected bool m_hasLifespan = true;
        [Tooltip("What is the lifespan of this object in seconds")]
        [SerializeField] protected float m_lifespan = 5f;


        //public bool GetHasLifespan() { return m_hasLifespan; }  //Allows other scripts to see this projectile's lifespan
       

       //public void SetLifespan(float lifespan) { m_lifespan = lifespan; }   //Allows other scripts to set this projectile's lifespan

        
        /// <summary>
        /// Starts the lifetime timer. The projectile will be destroyed after its lifespan lapses
        /// </summary>
        public void StartLifetimeTimer()
        {
            m_lifeTimer = StartCoroutine(DestroyAfterLifetime());
        }

        /// <summary>
        /// Destroys this projectile game object after the lifespan lapses
        /// </summary>
        /// <returns>Yield return for Coroutine</returns>
        public IEnumerator DestroyAfterLifetime()
        {
            yield return new WaitForSeconds(m_lifespan);

            GameObject.Destroy(gameObject);
        }



        /// <summary>
        /// Sets the trajectory of this projectile
        /// </summary>
        /// <param name="direction">Direction projectile should move</param>
        /// <param name="velocity">Desired Velocity of the projectile</param>
        public abstract void SetTrajectory(Vector3 direction, float velocity);
    }
}
