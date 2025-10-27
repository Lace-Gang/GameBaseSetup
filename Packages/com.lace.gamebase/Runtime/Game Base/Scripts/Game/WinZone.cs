using UnityEngine;

namespace GameBase
{
    public class WinZone : MonoBehaviour
    {
        /// <summary>
        /// Triggers the "win game" condition when the player character enters the hit box
        /// </summary>
        /// <param name="other">The collider that enters the trigger (handled by game engine)</param>
        private void OnTriggerEnter(Collider other)
        {
            //If collider belongs to player character, set game state to "win game"
            if(other.gameObject.GetComponent<PlayerCharacter>() != null)
            {
                GameInstance.Instance.m_gameState = GameState.WINGAME;
            }
        }
    }
}
