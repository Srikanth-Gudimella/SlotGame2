using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReelItem : MonoBehaviour
{
    public int ItemIndex;
    public Image ItemImg;
    public void SetItemImg()
    {
        ItemImg.sprite = GameManager.Instance.ReelItemImgs[ItemIndex];
    }
}
