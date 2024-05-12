using System;
using UnityEngine;
using UnityEngine.UI;

namespace SlotGame
{
    public class UIHandler : MonoBehaviour
    {
        public static UIHandler Instance;
        public static Action StartAction;
        public Button StartBtn;

        private void Awake()
        {
            Instance = this;
        }
        public void StartClick()
        {
            StartBtn.interactable = false;
            StartAction?.Invoke();
            Debug.Log("---- Bet = " + ScoreCtrl.Instance.BetAmount);
            ScoreCtrl.Instance.AddORDeductCash(ScoreCtrl.Instance.BetAmount*100, false);
        }
        public void Btn_Add(bool canIncrease)
        {
            ScoreCtrl.Instance.UpdateBetAmount(0.1f,canIncrease);
        }
    }
}
