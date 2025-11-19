using System.Collections;
using UnityEngine;

namespace GameBase
{
    //[RequireComponent(typeof(Collider))]
    //[RequireComponent(typeof(DamageSource))]
    public class MeleeWeapon : WeaponBase
    {
        //Hidden Variables
        //protected DamageSource m_hitBox;

        //Exposed Variabls
        [SerializeField] protected Collider m_hitBox;
        [SerializeField] protected DamageSource m_damageSource;


        [SerializeField] protected float m_attackDuration = 1f;


        private void Awake()
        {
            //m_hitBox = GetComponent<DamageSource>();
        }

        private void Start()
        {
            m_hitBox.enabled = false;
        }



        public override void Attack()
        {
            m_hitBox.enabled = true;
            StartCoroutine(AttackTimer());
        }



        public override void ShowWeapon()
        {
            //m_hitBox.enabled = true;
            GetComponentInChildren<MeshRenderer>().enabled = true;

        }

        public override void HideWeapon()
        {
            GetComponentInChildren<MeshRenderer>().enabled = false;
            //m_hitBox.enabled = false;
            //m_damageSource.enabled = false;
        }




        protected IEnumerator AttackTimer()
        {
            yield return new WaitForSeconds(m_attackDuration);
            m_hitBox.enabled = false;
        }


    }
}
