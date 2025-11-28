using System.Collections;
using UnityEngine;

namespace GameBase
{
    public class MeleeWeapon : WeaponBase
    {
        //Exposed Variabls
        [SerializeField] protected Collider m_hitBox;
        [SerializeField] protected DamageSource m_damageSource;


        public override void SetWeaponOwner(GameObject owner) { m_weaponOwner = owner; m_damageSource.SetDamageOwner(owner); }     //Allows other scripts to set the owner of this weapon



        /// <summary>
        /// Turns off hitbox (collider)
        /// </summary>
        private void Start()
        {
            m_hitBox.enabled = false;
        }


        /// <summary>
        /// Turns on hit box (collider) and sets a timer to turn it back off
        /// </summary>
        public override void Attack()
        {
            //Play attack audio
            if (m_playAttackSound && m_attackAudio != null)
            {
                m_attackAudio.Play();
            }

            m_hitBox.enabled = true;

            Debug.Log("Showing Weapon: " + m_renderer.enabled);


            StartCoroutine(AttackTimer());
        }


        /// <summary>
        /// Makes weapon visible
        /// </summary>
        public override void ShowWeapon()
        {

            m_renderer.enabled = true;

            //Debug.Log("Showing Weapon: " + m_renderer.enabled);

            //GetComponentInChildren<MeshRenderer>().enabled = true;

        }

        /// <summary>
        /// Makes weapon invisible
        /// </summary>
        public override void HideWeapon()
        {

            //Debug.Log("Hiding Weapon");

            //GetComponentInChildren<MeshRenderer>().enabled = false;

            m_renderer.enabled = false;
        }



        /// <summary>
        /// Turns hitbox (collider) off after the length of the attackDuration
        /// </summary>
        /// <returns></returns>
        protected IEnumerator AttackTimer()
        {
            yield return new WaitForSeconds(m_attackDuration);
            m_hitBox.enabled = false;
        }


    }
}
