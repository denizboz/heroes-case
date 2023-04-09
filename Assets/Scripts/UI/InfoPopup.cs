using UnityEngine;

namespace UI
{
    public class InfoPopup : MonoBehaviour
    {
        private static GameObject popupObject;

        private void Awake()
        {
            popupObject = gameObject;
            popupObject.SetActive(false);
        }

        public static void DisplayDataAt()
        {
            // set data
            // set position
            popupObject.SetActive(true);
        }
        
        public static void Close()
        {
            popupObject.SetActive(false);
        }
    }
}
