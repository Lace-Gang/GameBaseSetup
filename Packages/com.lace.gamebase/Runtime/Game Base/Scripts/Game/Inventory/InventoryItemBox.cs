using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public class InventoryItemBox : MonoBehaviour
    {
        //Hidden Variables
        private string m_itemName = "";
        private int m_numItems = 0;
        private InventoryItem m_item = null;
        private Sprite m_itemSprite;
        //private List<InventoryItem> m_items = new List<InventoryItem>();    //Stores all scripts of the item stored in this box


        [Header("Important Components")]
        [SerializeField] Image m_image;
        [SerializeField] RectTransform m_rectTransform;
        [SerializeField] TextMeshProUGUI m_nameText;
        [SerializeField] TextMeshProUGUI m_numberText;


        #region Getters and Setters
        public string GetItemName() { return m_itemName; }  //Allows other scripts to get the name of the item(s) being stored in this box
        public InventoryItem GetItemScript() { return m_item; }   //Allows other scripts to get the script of this item being held in this box


        /// <summary>
        /// Sets the position and dimensions of the inventory box
        /// </summary>
        /// <param name="positionX">Desired X position of this box on the canvas</param>
        /// <param name="positionY">Desired Y position of this box on the canvas</param>
        /// <param name="width">Desired width of this box</param>
        /// <param name="height">Desired height of this box</param>
        public void SetRectTransform(float positionX, float positionY, float width, float height)
        {
            m_rectTransform.sizeDelta = new Vector2(width, height); //Set dimensions of box

            m_rectTransform.anchoredPosition = new Vector2(positionX, positionY);   //set location of box
        }

        /// <summary>
        /// Sets this box's image
        /// </summary>
        /// <param name="image">The desired image</param>
        public void SetImage(Image image)
        {
            m_image.sprite = m_itemSprite;
        }
        #endregion Getters and Setters

        /// <summary>
        /// Adds item to this item box
        /// </summary>
        /// <param name="item">Item being added</param>
        public void AddItem(InventoryItem item)
        {
            m_item = item;
            m_itemName = item.GetItemName();
            m_itemSprite = item.GetInventorySprite();
            
            m_numItems++;

            UpdateBox();
        }



        /// <summary>
        /// Updates the inventory item box appearance in the UI
        /// </summary>
        public void UpdateBox()
        {
            m_image.sprite = m_itemSprite;
            m_nameText.text = (m_numItems > 0)? m_itemName : string.Empty;
            m_numberText.text = (m_numItems > 1)? m_numItems.ToString() : string.Empty;
        }

        //Test code
        public void turnOffImage()
        {
            m_image.canvasRenderer.SetColor(Color.red);
        }

    }
}
