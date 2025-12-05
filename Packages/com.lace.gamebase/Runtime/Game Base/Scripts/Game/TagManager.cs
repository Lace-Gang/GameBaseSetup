using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase
{
    public class TagManager : MonoBehaviour
    {
        [Tooltip("List of tags present in this manager at start")]
        [SerializeField] protected List<string> m_tags = new List<string>();

        public List<string> GetList() { return m_tags; }    //Allows other scripts to see this list


        #region TagManager Basic Functionality

        /// <summary>
        /// Searches list for a tag
        /// </summary>
        /// <param name="tag">Tag being searched for</param>
        /// <returns>If the tag was found</returns>
        public bool SearchForTag(string tag)
        {
            //Search list of tags for a tag that matches, returns true if a matching tag is found
            foreach(string s in m_tags)
            {
                if(s == tag) return true;
            }

            return false;   //returns false if no matching tags are found
        }

        /// <summary>
        /// Adds a tag
        /// </summary>
        /// <param name="tag">Tag to be added</param>
        public void AddTag(string tag)
        {
            if(!m_tags.Contains(tag))   //prevent duplicate tags from being added
            {
                m_tags.Add(tag);    //Adds tag
            }
        }

        /// <summary>
        /// Removes a tag
        /// </summary>
        /// <param name="tag">Tag to be removed</param>
        public void RemoveTag(string tag)
        {
            m_tags.Remove(tag); //Removes one instance of a tag
        }

        #endregion TagManager Basic Functionality


        #region Save and Load
        /// <summary>
        /// Saves this list of tags to save file
        /// </summary>
        /// <param name="data">GameData object being saved to</param>
        /// <param name="ID">ID to use for this instance when saving/loading</param>
        public void SaveTags(ref GameData data, string ID)
        {
            //Save tags
            if(data.stringListData.ContainsKey("Tag-" + ID + ".TagList"))
            {
                data.stringListData["Tag-" + ID + ".TagList"] = m_tags;
            }
            else
            {
                data.stringListData.Add("Tag-" + ID + ".TagList", m_tags);
            }
        }

        /// <summary>
        /// Loads list of tags from save file
        /// </summary>
        /// <param name="data">GameData object containing data from save file</param>
        /// <param name="ID">ID to use for this instance when saving/loading</param>
        public void LoadTags(GameData data, string ID)
        {
            //Load Tags
            if(data.stringListData.ContainsKey("Tag-" + ID + ".TagList"))
            {
                m_tags = data.stringListData["Tag-" + ID + ".TagList"];
            }
        }

        #endregion Save and Load
    }
}
