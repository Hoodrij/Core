using UnityEngine;

namespace Core.Ui.Components
{
    public class UICloseEventComponent : MonoBehaviour
    {
        // button inspector
        public void Close()
        {
            GetComponent<UIView>().Close();
        }
    }
}