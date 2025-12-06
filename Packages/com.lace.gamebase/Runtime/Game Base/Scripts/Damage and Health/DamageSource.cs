using UnityEngine;

namespace GameBase
{
    public class DamageSource : MonoBehaviour
    {
        #region Variables

        //Hidden Variables
        protected float m_incrementTimer;                         //Timer for increment damage
        protected bool m_incrementTimerActive = false;            //Shows if increment damage is active
        protected int m_numObjectsTakingIncrementDamage = 0;      //How many objects are currently taking increment damage from this object

        //Exposed Variables
        [Header("Universal Damage Information")]
        [Tooltip("Does not have to be this object this script is attatched to. Can also be set through the script.")]
        [SerializeField] protected GameObject m_damageOwner;
        [Tooltip("The duration in which the damage takes place. Can also be set through the script.")]
        [SerializeField] protected DamageDuration m_damageDuration = DamageDuration.INSTANTANIOUS;
        [Tooltip("Damage amount per hit. For Constant Damage, this means Damage Per Second. For Increment Damage, this means damage each increment.")]
        [SerializeField] protected float m_baseDamage;
        [Tooltip("Can the owner of this damage be damaged by this damage. (ie an enemy taking damage from their own explosive)")]
        [SerializeField] protected bool m_canDamageOwner = false;

        [Header("Instant Damage Information")]
        [Tooltip("Does this damaging object destroy itself after causing its damage. Only applies to Instant Damage")]
        [SerializeField] protected bool m_destroyOnDamageDealt = false;

        [Header("Increment Damage Information")]
        [Tooltip("How often should an instance of damage be applied. Measured in seconds.")]
        [SerializeField] protected float m_increment = 1f;
        [Tooltip("True if the first instance of damage should happen when the object enters the hit box. False if the first instance should happen after the first increment lapse.")]
        [SerializeField] protected bool m_dealDamageOnEnter = true;

        #endregion Variables


        /// <summary>
        /// Sets the owning object causing the damage. (so that damage owner can be set inside scripts)
        /// </summary>
        /// <param name="damageOwner">owning object causing the damage (ie, a player, enemy, or hazard)</param>
        public void SetDamageOwner(GameObject damageOwner) {  m_damageOwner = damageOwner; }

        /// <summary>
        /// Sets the damage duration. (so that damage duration can be set inside scripts)
        /// </summary>
        /// <param name="duration">When damage should be applied</param>
        public void SetDamageDuration(DamageDuration duration) { m_damageDuration = duration; }

        /// <summary>
        /// Updates timer for the incremented damage type
        /// </summary>
        private void FixedUpdate()
        {
            //resets increment timer for next increment if timer has hit zero
            if(m_incrementTimer <=  0f)
            {
                m_incrementTimer = m_increment; //resets incrememnt timer when it hits zero. (this is in FixedUpdate so that damage for this increment has already been applied
            } else if(m_incrementTimerActive)
            {
                m_incrementTimer -= Time.deltaTime; //decrements timer by delta time
            }
        }

        /// <summary>
        /// Deals damage or begins tracking values as necessary when a damagable object enters the hit box. Behavior varies depending on damage 
        /// type. Optionally does nothing if the object exiting is the object that owns this damage.
        /// </summary>
        /// <param name="other">The collider that enters the trigger (handled by game engine)</param>
        private void OnTriggerEnter(Collider other)
        {
            //Check for paused game
            if (GameInstance.Instance.getPaused()) return;

            //Does nothing if object cannot be damaged by this damage
            if (other.GetComponent<IDamagableInterface>() != null && (other.gameObject != m_damageOwner || m_canDamageOwner))
            {
                switch (m_damageDuration)
                {
                    //For Instantanious damage, deals one instance of base damage, and then destroys self if "destroy on damage dealt" is true
                    case DamageDuration.INSTANTANIOUS:
                        //Deals one instance of damage
                        other.GetComponent<IDamagableInterface>().TakeDamage(m_baseDamage, m_damageOwner);
                        //Destroys self (if applicable)
                        if (this.m_destroyOnDamageDealt) GameObject.Destroy(this.gameObject);
                        break;
                    //For Inrement damage, tracks how many damageable objects are within the hit box, begins timer (if timer is not on), and deals one instance of damage to the 
                    //current object entering the hit box if "deal damage on enter" is set to true
                    case DamageDuration.INCREMENT:
                        //If "deal damage on enter" is set to true, deals one instance of damage now.
                        if(m_dealDamageOnEnter)
                        other.GetComponent<IDamagableInterface>().TakeDamage(m_baseDamage, m_damageOwner);

                        //If this is the only object taking damage, begins tracking increment timer
                        if(m_numObjectsTakingIncrementDamage == 0)
                        {
                            m_incrementTimerActive = true;
                            m_incrementTimer = m_increment;
                        }

                        //Tracks number of objects currently taking damage
                        m_numObjectsTakingIncrementDamage++;
                        break;
                    //If damage duration is Constant, deals appropriate amount of damage relative to the delta time of the current frame
                    case DamageDuration.CONSTANT:
                        //Deals damage relative to delta time
                        other.GetComponent<IDamagableInterface>().TakeDamage(m_baseDamage * Time.deltaTime, m_damageOwner);
                        break;
                    default:
                        break;
                }

            }
        }

        /// <summary>
        /// Deals damage at intervals when a damagable object is within the hit box. Behavior varies depending on DamageDuration. Optionally 
        /// does nothing if the object exiting is the object that owns this damage.
        /// </summary>
        /// <param name="other">The collider that is inside the trigger (handled by game engine)</param>
        private void OnTriggerStay(Collider other)
        {
            //Check for paused game
            if (GameInstance.Instance.getPaused()) return;

            //Does nothing if object cannot be damaged by this damage
            if (other.GetComponent<IDamagableInterface>() != null && (other != m_damageOwner || m_canDamageOwner))
            {
                switch (m_damageDuration)
                {
                    //Does nothing if damage duration is Instantanious (as damage has already been applied)
                    case DamageDuration.INSTANTANIOUS:
                        break;
                    //For Increment damage duration, deals one instance of damage IF and only if the timer has lapsed
                    case DamageDuration.INCREMENT:
                        //if increment has lapsed, deals one instance of damage
                        if(m_incrementTimer <= 0)
                        {
                            other.GetComponent<IDamagableInterface>().TakeDamage(m_baseDamage, m_damageOwner);
                        }
                        break;
                    //If damage duration is Constant, deals appropriate amount of damage relative to the delta time of the current frame
                    case DamageDuration.CONSTANT:
                        //Deals damage relative to delta time
                        other.GetComponent<IDamagableInterface>().TakeDamage(m_baseDamage * Time.deltaTime, m_damageOwner);
                        break;
                    default:
                        break;
                }
            }

        }

        /// <summary>
        /// Updates necessary values when a damagable object leaves the hit box. Optionally does nothing if the object exiting is the object that owns this damage.
        /// </summary>
        /// <param name="other">The collider that exits the trigger (handled by game engine)</param>
        private void OnTriggerExit(Collider other)
        {
            //Check for paused game
            if (GameInstance.Instance.getPaused()) return;

            //Does nothing if object cannot be damaged by this damage
            if (other.GetComponent<IDamagableInterface>() != null && (other != m_damageOwner || m_canDamageOwner))
            {
                switch (m_damageDuration)
                {
                    //For instantaneous damage, does nothing as damage has already been applied
                    case DamageDuration.INSTANTANIOUS:
                        break;
                    //For Increment damage, tracks new number of damagable objects currently within the hit box. If no objects are still taking damage, deactivates timer
                    case DamageDuration.INCREMENT:
                        //decrement number of objects taking damage
                        m_numObjectsTakingIncrementDamage--;
                        //checks if any damageable objects are still present and turns off the timer if not
                        if(m_numObjectsTakingIncrementDamage <= 0)
                        {
                            m_incrementTimerActive = false;
                        }
                        break;
                    //For constant damage, does nothing
                    case DamageDuration.CONSTANT:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
