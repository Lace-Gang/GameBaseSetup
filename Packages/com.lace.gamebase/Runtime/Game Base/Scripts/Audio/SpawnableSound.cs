using System.Collections;
using UnityEngine;

namespace GameBase
{
    public class SpawnableSound : MonoBehaviour
    {
        //Hidden Variables
        protected AudioClip m_audioClip = null;
        protected Coroutine m_lifespanCoroutine;

        //Exposed Variables
        [SerializeField] protected AudioSource m_audio;
        //[SerializeField] protected bool m_destroyAfterLifespan = false;
        [SerializeField] protected float m_lifeSpan = 1.0f;


        public void SetAudio(AudioClip soundClip) { m_audioClip = soundClip; }
        public void SetLifespan(float lifespan) { m_lifeSpan = lifespan; }

        public void PlayAudio()
        {
            m_audio.PlayOneShot(m_audioClip);
        }


        public void StartLifespanTimer()
        {
            m_lifespanCoroutine = StartCoroutine(LifespanTimer());
        }

        public void StopLifespanTimer()
        {
            if(m_lifespanCoroutine != null)
            {
                StopCoroutine(m_lifespanCoroutine);
                m_lifespanCoroutine = null;
            }
        }

        protected IEnumerator LifespanTimer()
        {
            yield return new WaitForSeconds(m_lifeSpan);

            m_audio.Stop();
            GameObject.Destroy(this.gameObject);
        }
    }
}
