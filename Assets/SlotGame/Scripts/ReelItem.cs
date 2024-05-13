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
        public GameObject medusa, zeus;
        
        public void SetItemImg()
        {
            medusa.SetActive(false);zeus.SetActive(false);
            ItemImg.enabled = true;
            ItemImg.sprite = GameManager.Instance.ReelItemImgs[ItemIndex];
        }

        public void ActivateEffect(bool status,int scoreToDisplay)
        {
            effectObj.SetActive(status);
            effectObj.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            iTween.ScaleTo(Text_Score.gameObject, iTween.Hash("x",1.2f,"y",1.2f,"z",1.2f,"delay",0.2f, 
                "time", 1f, "easetype", iTween.EaseType.easeOutBounce));
            Text_Score.text = scoreToDisplay > 0 ? scoreToDisplay.ToString() : string.Empty;
            if(status)
            {
                //GameManager.Instance.zeusChar.PlayCharAnim(1);// Test

                if (ItemIndex == 2)
                {
                    medusa.SetActive(true); ItemImg.enabled = false;
                    GameManager.Instance.medusaChar.PlayCharAnim(1);
                }
                if (ItemIndex == 1)
                {
                    zeus.SetActive(true); ItemImg.enabled = false;
                    GameManager.Instance.zeusChar.PlayCharAnim(1);
                }
            }
        }
    }
}
