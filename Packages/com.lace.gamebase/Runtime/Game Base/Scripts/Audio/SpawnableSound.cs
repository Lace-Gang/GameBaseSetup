using System.Collections;
using UnityEngine;

namespace GameBase
{
    public class SpawnableSound : MonoBehaviour
    {
        //Hidden Variables
        protected AudioClip m_audioClip = null;     //AudioClip for this SpawnableSound to play
        protected Coroutine m_lifespanCoroutine;    //Holds reference to lifespan coroutine so it can be stopped if need be

        //Exposed Variables
        [Tooltip("Audiosource component used to play sound")]
        [SerializeField] protected AudioSource m_audio;
        [Tooltip("How long is lifespan (once StartLifespanTimer is called)")]
        [SerializeField] protected float m_lifeSpan = 1.0f;


        public void SetAudio(AudioClip soundClip) { m_audioClip = soundClip; }  //Lets other scripts set the audio clip for this spawnable sound
        public void SetLifespan(float lifespan) { m_lifeSpan = lifespan; }  //Lets other scripts set the lifespan of this spawnable sound

        /// <summary>
        /// Plays the current set audio clip
        /// </summary>
        public void PlayAudio()
        {
            m_audio.PlayOneShot(m_audioClip);   //plays audio
        }


        /// <summary>
        /// Starts the lifespan timer coroutine. This spawnable sound will be destroyed once the lifespan has elapsed.
        /// </summary>
        public void StartLifespanTimer()
        {
            m_lifespanCoroutine = StartCoroutine(LifespanTimer());
        }

        /// <summary>
        /// Stops the lifespan timer coroutine.
        /// </summary>
        public void StopLifespanTimer()
        {
            if(m_lifespanCoroutine != null)
            {
                StopCoroutine(m_lifespanCoroutine);
                m_lifespanCoroutine = null;
            }
        }

        /// <summary>
        /// Destroys this SpawnableSound after the lifspan elapses
        /// </summary>
        /// <returns>Yield return for coroutine to wait</returns>
        protected IEnumerator LifespanTimer()
        {
            yield return new WaitForSeconds(m_lifeSpan);

            m_audio.Stop(); //stops audio if it is still playing
            GameObject.Destroy(this.gameObject);
        }
    }
}
