using UnityEngine;
using Custom.Utils;

namespace SlotGame
{
    public class GameManager : GameConstants
    {
        public static GameManager Instance;

        public Sprite[] ReelItemImgs;
        public int ReelsFinishedCount;
        public bool IsActivateLossCondition;
        public bool IsActivateWinCondition;

        public bool useFixedStartIndexs;
        public int WinLineIndex,WinItemIndex,WinItemsCount;
        private float[] probabilities = { 0.8f, 0.2f, 0.1f }; // You can adjust these values as needed
        
        public SpinAnimCtrl medusaChar,zeusChar,jackPot;
        private void Awake()
        {
            Instance = this;
            //IsActivateLossCondition = true;
#if !UNITY_EDITOR
            useFixedStartIndexs=false;
#endif
        }
        private void OnEnable()
        {
            UIHandler.StartAction += OnStartClick;
        }
        private void OnDisable()
        {
            UIHandler.StartAction -= OnStartClick;
        }
        private void OnStartClick()
        {
            if (IsActivateWinCondition)
            {
                WinLineIndex = Random.Range(0, lineInfos.Count);
                WinItemIndex = Random.Range(0, 10);

                float randomValue = Random.value;

                // Check which probability range the random number falls into
                if (randomValue <= probabilities[0])
                {
                    Debug.Log("70% probability logic executed");
                    WinItemsCount = 3;
                }
                else if (randomValue <= probabilities[0] + probabilities[1])
                {
                    Debug.Log("30% probability logic executed");
                    WinItemsCount = 4;
                }
                else if (randomValue <= probabilities[0] + probabilities[1] + probabilities[2])
                {
                    Debug.Log("10% probability logic executed");
                    WinItemsCount = 5;
                }
                else
                {
                    // This should never happen if probabilities are properly set
                    Debug.LogError("Error: Probability sum exceeds 1");
                }
                Debug.LogError("--- WinLineIndex=" + WinLineIndex);
                Debug.LogError("--- WinItemIndex=" + WinItemIndex);
                Debug.LogError("--- WinItemsCount=" + WinItemsCount);
            }

            foreach (LineInfo lineInfo in lineInfos)
            {
                for (int i = 0; i < lineInfo.ReelItemsList.Length; i++)
                    lineInfo.ReelItemsList[i].ActivateEffect(false, 0);
            }
            medusaChar.PlayCharAnim(0);
            zeusChar.PlayCharAnim(0);
        }

        const int NUM_OF_REELS = 5;
        public void ReelFinishedAction()
        {
            ReelsFinishedCount++;
            if (ReelsFinishedCount == NUM_OF_REELS)
            {
                // All Reels rotation finished and stopeed
                FindWinningLines();
                CoroutineUtils.instance.WaitUntillGivenTime(2, () =>
                {
                    ReelsFinishedCount = 0;
                    UIHandler.Instance.StartBtn.interactable = true;
                });
            }
        }

        public void FindWinningLines()
        {
            Debug.Log("------- FindWinningLines");
            WinningLinesList.Clear();
            foreach(LineInfo lineInfo  in lineInfos)
            {
                lineInfo.FirstReelItemIndex = lineInfo.ReelItemsList[0].ItemIndex;
                lineInfo.MatchCount = 1;
                for (int i = 1; i < lineInfo.ReelItemsList.Length; i++)
                {
                    if (lineInfo.FirstReelItemIndex == lineInfo.ReelItemsList[i].ItemIndex)
                    {
                        lineInfo.MatchCount++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (lineInfo.MatchCount >= 3)
                {
                    WinningLinesList.Add(lineInfo);
                    Debug.Log("WinLine Matchcount = "+lineInfo.MatchCount);
                }
            }
            Debug.Log("---- Find Winnings total winninglinescount=" + WinningLinesList.Count);
            ShowWinLineEffect();
        }

        public void ShowWinLineEffect()
        {
            for(int i = 0; i < WinningLinesList.Count; i++)
            {
                for (int j = 0; j < WinningLinesList[i].MatchCount; j++)
                {
                    ScoreCtrl.Instance.AddORDeductCash(scoreValuesBasedOnItem[WinningLinesList[i].ReelItemsList[j].ItemIndex]);
                    int scoreOnItem = scoreValuesBasedOnItem[WinningLinesList[i].ReelItemsList[j].ItemIndex];
                    WinningLinesList[i].ReelItemsList[j].ActivateEffect(true, scoreOnItem);
                    Debug.Log("---- Each Item Score : " + scoreValuesBasedOnItem[WinningLinesList[i].ReelItemsList[j].ItemIndex]);
                }
            }
        }
    }
}
