using UnityEngine;

namespace GameBase
{
    public class BasicHPRecoveryItem : ItemBase
    {
        //Exposed Variables
        [Tooltip("How much will the player be healed by this item")]
        [SerializeField] float m_healAmount;

        /// <summary>
        /// Notifies parent if the player enters the trigger
        /// </summary>
        /// <param name="other">The collider that entered the trigger (handled by game engine)</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerCharacter>() != null)  //only trigger if the player is the one who entered
            {
                ParentTriggerEnter();   //tell parent about the player entering the trigger

                if(m_autoPickup) OnPickedUp();  //execute OnPickedUp
            }
        }

        /// <summary>
        /// Heals player
        /// </summary>
        public override void OnPickedUp()
        {
            GameInstance.Instance.GetPlayerScript().HealDamage(m_healAmount);   //Heal player

            ItemBaseOnPickedUp(); //Base item "On pickup" execution
        }
    }
}
