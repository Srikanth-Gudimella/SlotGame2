using UnityEngine;
using UnityEngine.UI;

namespace SlotGame
{
    public class ReelItem : MonoBehaviour
    {
        public int ItemIndex;
        public Image ItemImg;
        public void SetItemImg()
        {
            ItemImg.sprite = GameManager.Instance.ReelItemImgs[ItemIndex];
        }
    }
}
