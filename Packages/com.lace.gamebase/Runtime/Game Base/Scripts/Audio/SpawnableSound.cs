using UnityEngine;

namespace GameBase
{
    public class SpawnableSound : MonoBehaviour
    {
        [SerializeField] protected AudioSource m_audio;
        [SerializeField] protected bool m_destroyAfterLifespan = false;
        [SerializeField] protected float m_lifeSpan = 1.0f;

        public void PlaySound()
        {
            m_audio.Play();
        }
    }
}
