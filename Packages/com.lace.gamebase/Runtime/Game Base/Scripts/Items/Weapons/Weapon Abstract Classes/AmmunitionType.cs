using UnityEngine;

namespace GameBase
{
    public class AmmunitionType : MonoBehaviour
    {
        //Exposed Variables
        [Header("AmmunitionType Basic Info")]
        [Tooltip("Name of this Ammunition Type")]
        [SerializeField] protected string m_name;
        [Tooltip("Reference to a prefab of the Ammunition this AmmunitionType represents")]
        [SerializeField] protected GameObject m_ammunition;


        public string GetName() {  return m_name; }     //Allows other scripts to see this AmmunitionType's name
        public GameObject GetAmmunition() {  return m_ammunition; }     //Allows other scripts to get this AmmunitionType's Ammunition prefab
    }
}
