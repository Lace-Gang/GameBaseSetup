using System.Collections;
using UnityEngine;

namespace GameBase
{
    public class MeleeWeapon : WeaponBase
    {
        //Exposed Variabls
        [Header("Melee Weapon Base Details")]
        [Tooltip("Reference to weapon collider for the damaging hitbox")]
        [SerializeField] protected Collider m_hitBox;
        [Tooltip("Reference to the DamageSource component")]
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
            //Play attack audio if audio exists
            if (m_playAttackSound && m_attackAudio != null)
            {
                m_attackAudio.Play();
            }

            m_hitBox.enabled = true;    //turns hitbox on

            StartCoroutine(AttackTimer());  //Starts timer to tell weapon when to turn it's hitbox off
        }

        /// <summary>
        /// Makes weapon visible
        /// </summary>
        public override void ShowWeapon()
        {
            m_renderer.enabled = true;
        }

        /// <summary>
        /// Makes weapon invisible
        /// </summary>
        public override void HideWeapon()
        {
            m_renderer.enabled = false;
        }

        /// <summary>
        /// Turns hitbox (collider) off after the length of the attackDuration
        /// </summary>
        /// <returns>Yield return for coroutine</returns>
        protected IEnumerator AttackTimer()
        {
            yield return new WaitForSeconds(m_attackDuration);
            m_hitBox.enabled = false;
        }
    }
}
