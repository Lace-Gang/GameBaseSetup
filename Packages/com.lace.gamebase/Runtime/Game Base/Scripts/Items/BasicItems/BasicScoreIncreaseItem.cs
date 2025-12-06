using UnityEngine;

namespace GameBase
{
    public class BasicScoreIncreaseItem : ItemBase
    {
        [Header("Score Increase Information")]
        [Tooltip("How much does this increase the player's score")]
        [SerializeField] protected float m_scoreIncrease;


        /// <summary>
        /// Uses this item and then hides it and marks it "Inactive in Scene"
        /// </summary>
        public override void OnPickedUp()
        {
            Use();  //Use item

            HideItemInScene();   //Hides item in the scene
        }

        /// <summary>
        /// Adds to the players score
        /// </summary>
        public override void Use()
        {
            //Tells GameInstance to add to the score
            GameInstance.Instance.AddOrRemoveScore(m_scoreIncrease);
        }
    }
}
