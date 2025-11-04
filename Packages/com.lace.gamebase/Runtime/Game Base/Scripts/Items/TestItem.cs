using UnityEngine;

namespace GameBase
{
    public class TestItem : ItemBase
    {
        [SerializeField] ItemPickupPrompter m_pickupPrompter;


        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<PlayerCharacter>() != null)
            {
                ParentTriggerEnter();
                if(m_autoPickup) OnPickedUp();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.GetComponent<PlayerCharacter>() != null)
            {
                ParentTriggerExit();
            }
        }

        public override void OnPickedUp()
        {
            ItemBaseOnPickedUp();
        }
    }
}
