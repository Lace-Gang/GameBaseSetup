using UnityEngine;

namespace GameBase
{
    public class BasicScoreIncreaseItem : ItemBase
    {
        [Header("Item Specific Information")]
        [Tooltip("How much does this increase the player's score")]
        [SerializeField] float m_scoreIncrease;


        //private void OnTriggerEnter(Collider other)
        //{
        //    if(other.gameObject.GetComponent<PlayerCharacter>() != null) ParentTriggerEnter();
        //}

        /// <summary>
        /// Increases score
        /// </summary>
        public override void OnPickedUp()
        {
            GameInstance.Instance.AddOrRemoveScore(m_scoreIncrease);
        }
    }
}
