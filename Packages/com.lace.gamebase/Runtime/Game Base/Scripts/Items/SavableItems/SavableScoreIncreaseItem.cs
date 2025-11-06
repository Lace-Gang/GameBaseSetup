using UnityEngine;

namespace GameBase
{
    public class SavableScoreIncreaseItem : SavableItem
    {
        //Exposed Variables
        [Header("Score Increase Information")]
        [Tooltip("How much does this increase the player's score")]
        [SerializeField] float m_scoreIncrease;


        /// <summary>
        /// Uses this item and then hides it and marks it "Inactive in Scene"
        /// </summary>
        public override void OnPickedUp()
        {
            Use();

            ItemBaseOnPickedUp();   //Parent "On Picked Up" function (hide and mark inactive)
        }

        /// <summary>
        /// Adds to the players score
        /// </summary>
        public override void Use()
        {
            GameInstance.Instance.AddOrRemoveScore(m_scoreIncrease);
        }
    }
}
