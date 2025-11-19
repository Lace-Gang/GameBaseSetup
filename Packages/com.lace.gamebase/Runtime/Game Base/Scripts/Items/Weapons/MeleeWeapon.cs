using UnityEngine;

namespace GameBase
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(DamageSource))]
    public class MeleeWeapon : WeaponBase
    {
        //Hidden Variables


        //Exposed Variabls





        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}
