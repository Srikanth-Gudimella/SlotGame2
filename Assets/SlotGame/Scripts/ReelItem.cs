using UnityEngine;
using UnityEngine.UI;

namespace SlotGame
{
    public class ReelItem : MonoBehaviour
    {
        public int ItemIndex;
        public Image ItemImg;
        public GameObject effectObj;

        public void SetItemImg()
        {
            ItemImg.sprite = GameManager.Instance.ReelItemImgs[ItemIndex];
        }
        public void ActivateEffect(bool status)
        {
            effectObj.SetActive(status);
            effectObj.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }
    }
}
