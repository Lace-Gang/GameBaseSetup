using UnityEngine;
using UnityEngine.UIElements;

namespace GameBase
{
    public abstract class SavableItem : ItemBase, IDataPersistence
    {
        [Header ("Save Information")]
        [Tooltip("Should this instance be saved and loaded")]
        [SerializeField] protected bool m_save = true;
        [Tooltip("This field is absolutely REQUIRED if 'save' is checked to true, and should be unique between each instance of an object. (Zero if an invalid ID)")]
        [SerializeField] protected int m_ID = 0;
        [Tooltip("Should 'if this item is active in the scene' be saved or not? (Allows items that have already been picked up to remain picked up")]
        [SerializeField] protected bool m_saveActiveInScene = false;
        [Tooltip("Should the position of this object be saved and loaded")]
        [SerializeField] protected bool m_savePosition = false;
        [Tooltip("Should the rotation of this object be saved and loaded")]
        [SerializeField] protected bool m_saveRotation = false;

        /// <summary>
        /// Validates that item has had a valid ID set unless the item is being created in response to the game being loaded
        /// </summary>
        void Start()
        {
            //only checks when not saving because objects instantiated during loading phase will not have an ID on start (but it's okay)
            if(this is InventoryItem && (GameInstance.Instance.m_gameState == GameState.LOADSAVE || GameInstance.Instance.m_gameState == GameState.LOADTITLE)) return;
                //An individual ID MUST be assigned to each savable item
                Debug.Assert (m_ID != 0, "Item (" + m_name + ") does not have a valid ID");        
        }

        public int GetID() { return m_ID; } //Allows other scripts to get this item's ID



        #region Save and Load


        /// <summary>
        /// If this instance of this item is indicated to save, saves all data that is indicated to be saved
        /// </summary>
        /// <param name="data">A reference to the GameData object to save the data to</param>
        public void SaveData(ref GameData data)
        {
            if(!m_save) return;    //only saves if indicated to do so
            
            //Save "Active in Scene" (if indicated to do so)
            if(m_saveActiveInScene)
            {
                //Check boolData for key. If key exists, change value to current value, else add key with current value
                if(data.boolData.ContainsKey("SavableItem." + m_ID + ".ActiveInScene"))
                {
                    data.boolData["SavableItem." + m_ID + ".ActiveInScene"] = m_activeInScene;
                }
                else
                {
                    data.boolData.Add("SavableItem." + m_ID + ".ActiveInScene", m_activeInScene);
                }
            }

            //Save position (if indicated to do so)
            if(m_savePosition)
            {
                //Check Vector3Data for key. If key exists, change value to current value, else add key with current value
                if(data.vector3Data.ContainsKey("SavableItem." + m_ID + ".Position"))
                {
                    data.vector3Data["SavableItem." + m_ID + ".Position"] = transform.position;
                }
                else
                {
                    data.vector3Data.Add("SavableItem." + m_ID + ".Position", transform.position);
                }
            }

            //Save rotation (if indicated to do so)
            if (m_saveRotation)
            {
                //Check quaternionData for key. If key exists, change value to current value, else add key with current value
                if (data.quaternionData.ContainsKey("SavableItem." + m_ID + ".Rotation"))
                {
                    data.quaternionData["SavableItem." + m_ID + ".Rotation"] = transform.rotation;
                }
                else
                {
                    data.quaternionData.Add("SavableItem." + m_ID + ".Rotation", transform.rotation);
                }
            }
        }


        /// <summary>
        /// If this instance of this item is indicated to load, loads all data that is indicated to be loaded
        /// </summary>
        /// <param name="data">The GameData from which this item is supposed to load</param>
        public void LoadData(GameData data)
        {
            if (!m_save) return;    //only load if indicated to do so

            //Load "Active in Scene" (if indicated to do so)
            if(m_saveActiveInScene)
            {
                //Check boolData if key exists, if so load "Active in Scene", if not then do nothing
                if(data.boolData.ContainsKey("SavableItem." + m_ID + ".ActiveInScene"))
                {
                    m_activeInScene = data.boolData["SavableItem." + m_ID + ".ActiveInScene"];

                    //Hide this object if not active in scene
                    m_renderer.enabled = m_activeInScene;     //item no longer visible
                    m_collider.enabled = m_activeInScene;     //item no longer collidable


                    //Checks if this item has a pickup prompter. If so, sets "PromptActive" to whether this item is active in the scene
                    //so that the prompt will display if the item is active in the scene, but not if the item is not active in the scene
                    ItemPickupPrompter pickupPrompter = GetComponentInChildren<ItemPickupPrompter>();
                    if(pickupPrompter != null)
                    {
                        pickupPrompter.SetPromptActive(m_activeInScene);
                    }
                }
            }

            //Load position (if indicated to do so)
            if (m_savePosition)
            {
                //Check if key exists in vector3Data, if so load position, if not then do nothing
                if (data.vector3Data.ContainsKey("SavableItem." + m_ID + ".Position"))
                {
                    transform.position = data.vector3Data["SavableItem." + m_ID + ".Position"];
                }
            }


            //Load rotation (if indicated to do so)
            if (m_saveRotation)
            {
                //Check if key exists in item quaternionData, if so load rotation, if not then do nothing
                if (data.quaternionData.ContainsKey("SavableItem." + m_ID + ".Rotation"))
                {
                    transform.rotation = data.quaternionData["SavableItem." + m_ID + ".Rotation"];
                }
            }
        }

        #endregion Save and Load


    }
}
