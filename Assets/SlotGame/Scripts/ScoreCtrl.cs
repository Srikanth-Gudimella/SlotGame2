using UnityEngine;
using UnityEngine.UI;

namespace SlotGame
{
    public class ScoreCtrl : MonoBehaviour
    {
        public static ScoreCtrl Instance;

        private float cash;
        public float Cash { get => cash; set => cash = value; }

        private float betAmount;
        public float BetAmount { get => betAmount; set => betAmount = value; }

        public Text Text_Score,Text_BetAmount,Text_WinStatus;

        private void OnEnable()
        {
            if (!Instance)
                Instance = this;

            AddORDeductCash(1500);//Testing 
            UpdateBetAmount(0.1f, true);//Testing 
        }
       
        public void AddORDeductCash(float  cash,bool canAdd = true)
        {
            Cash = canAdd ? Cash + cash : Cash - cash;
            float val = Cash / 100.0f;
            Text_Score.text = val.ToString("F2");

            //Text_WinStatus.text = canAdd ? (cash/100).ToString("F2"):"GOOD LUCK";
            //Invoke(nameof(UpdateWinStatus), 1f);
        }
        private void UpdateWinStatus()
        {
            Text_WinStatus.text = "GOOD LUCK";
        }
        public void UpdateBetAmount(float betAmount ,bool canIncrease)
        {
            //BetAmount = canIncrease ? BetAmount + betAmount / 100 : BetAmount- betAmount / 100;
            BetAmount = canIncrease ? BetAmount + betAmount : BetAmount - betAmount;
            Text_BetAmount.text = BetAmount.ToString("F2");
            Debug.Log("---- Bet = " + BetAmount);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
                AddORDeductCash(30);
        }
    }
}
