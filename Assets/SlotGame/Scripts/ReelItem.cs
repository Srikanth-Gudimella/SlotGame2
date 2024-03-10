using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace SlotGame
{
    public class ReelItem : MonoBehaviour
    {
        public int ItemIndex;
        public TextMeshProUGUI Text_Score;
        public Image ItemImg;
        public GameObject effectObj;

        public void SetItemImg()
        {
            ItemImg.sprite = GameManager.Instance.ReelItemImgs[ItemIndex];
        }
        public void ActivateEffect(bool status,int scoreToDisplay)
        {
            effectObj.SetActive(status);
            effectObj.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            iTween.ScaleTo(Text_Score.gameObject, iTween.Hash("x",1.2f,"y",1.2f,"z",1.2f,"delay",0.2f, 
                "time", 1f, "easetype", iTween.EaseType.easeOutBounce));
            Text_Score.text = scoreToDisplay > 0 ? scoreToDisplay.ToString() : string.Empty;
        }
    }
}
