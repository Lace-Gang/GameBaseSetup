using UnityEngine;

namespace GameBase
{
    public class AmmunitionType : MonoBehaviour
    {
        [SerializeField] protected string m_name;
        [SerializeField] protected GameObject m_ammunition;




        public string GetName() {  return m_name; }
        public GameObject GetAmmunition() {  return m_ammunition; }
    }
}
