using UnityEngine;

namespace GameBase
{
    public enum DamageDuration
    {
        INSTANTANIOUS,
        INCREMENT,
        CONSTANT
    }

    public class DamageSource : MonoBehaviour
    {
        //Hidden Variables
        private float m_incrementTimer;
        private bool m_incrementTimerActive = false;
        private int m_numObjectsTakingIncrementDamage = 0;

        //Editor Variables
        [Header("Universal Damage Information")]
        [Tooltip("Does not have to be this object this script is attatched to. Can also be set through the script.")]
        [SerializeField] GameObject m_damageOwner;
        [Tooltip("The duration in which the damage takes place")]
        [SerializeField] DamageDuration m_damageDuration = DamageDuration.INSTANTANIOUS;
        [Tooltip("Damage amount per hit. For Constant Damage, this means Damage Per Second. For Increment Damage, this means damage per increment.")]
        [SerializeField] float m_baseDamage;
        [SerializeField] bool m_canDamageOwner = false;

        [Header("Instant Damage Information")]
        [SerializeField] bool m_destroyOnDamageDealt = false;

        [Header("Increment Damage Information")]
        [SerializeField] float m_increment = 1f;



        public void SetDamageOwner(GameObject damageOwner) {  m_damageOwner = damageOwner; }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        private void FixedUpdate()
        {

            //resets increment timer for next increment if timer has hit zero
            if(m_incrementTimer <=  0f)
            {
                m_incrementTimer = m_increment;
            } else if(m_incrementTimerActive)
            {
                m_incrementTimer -= Time.deltaTime;
            }
        }

        /// <summary>
        /// Deals instant damage to damagable objects if they enter this objects trigger zone. Does not deal damage if duration is not Instant. Optionally does not damage the object that owns this damage.
        /// </summary>
        /// <param name="other">The object that enters the trigger (handled by game engine)</param>
        private void OnTriggerEnter(Collider other)
        {
            //Does nothing if object cannot be damaged by this damage
            if (other.GetComponent<IDamagableInterface>() != null && (other != m_damageOwner || m_canDamageOwner))
            {
                switch (m_damageDuration)
                {
                    case DamageDuration.INSTANTANIOUS:
                        Debug.Log("Instant Damage Dealt!");

                        //Deals one instance of damage
                        other.GetComponent<IDamagableInterface>().TakeDamage(m_baseDamage, m_damageOwner);
                        //Destroys self (if applicable)
                        if (this.m_destroyOnDamageDealt) GameObject.Destroy(this);
                        break;
                    case DamageDuration.INCREMENT:
                        //If this is the only object taking damage, begins tracking increment timer
                        if(m_numObjectsTakingIncrementDamage == 0)
                        {
                            m_incrementTimerActive = true;
                            m_incrementTimer = m_increment;
                        }

                        //Tracks objects currently taking damage
                        m_numObjectsTakingIncrementDamage++;
                        break;
                    case DamageDuration.CONSTANT:
                        break;
                    default:
                        break;
                }

            }
        }


        private void OnTriggerStay(Collider other)
        {
            //Does nothing if object cannot be damaged by this damage
            if (other.GetComponent<IDamagableInterface>() != null && (other != m_damageOwner || m_canDamageOwner))
            {
                switch (m_damageDuration)
                {
                    case DamageDuration.INSTANTANIOUS:
                        break;
                    case DamageDuration.INCREMENT:
                        //if increment has lapsed, deals one instance of damage
                        if(m_incrementTimer <= 0)
                        {
                            other.GetComponent<IDamagableInterface>().TakeDamage(m_baseDamage, m_damageOwner);
                        }
                        break;
                    case DamageDuration.CONSTANT:
                        //Deals damage relative to delta time
                        other.GetComponent<IDamagableInterface>().TakeDamage(m_baseDamage * Time.deltaTime, m_damageOwner);
                        break;
                    default:
                        break;
                }
            }

        }


        private void OnTriggerExit(Collider other)
        {
            //Does nothing if object cannot be damaged by this damage
            if (other.GetComponent<IDamagableInterface>() != null && (other != m_damageOwner || m_canDamageOwner))
            {
                switch (m_damageDuration)
                {
                    case DamageDuration.INSTANTANIOUS:
                        break;
                    case DamageDuration.INCREMENT:
                        if(m_numObjectsTakingIncrementDamage == 1)
                        {
                            m_incrementTimerActive = false;
                        }
                        m_numObjectsTakingIncrementDamage--;
                        break;
                    case DamageDuration.CONSTANT:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
