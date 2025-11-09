using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public class InventoryItemBox : MonoBehaviour
    {
        [Header("Important Components")]
        [SerializeField] Image m_image;
        [SerializeField] RectTransform m_rectTransform;


        public void SetRectTransform(float anchorX, float anchorY, float width, float height)
        {
            m_rectTransform.sizeDelta = new Vector2(width, height);

            //m_rectTransform.anchoredPosition = new Vector2(0, 0);
            m_rectTransform.anchoredPosition = new Vector2(anchorX, anchorY);
            //m_rectTransform.position = new Vector2(anchorX, anchorY);
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
