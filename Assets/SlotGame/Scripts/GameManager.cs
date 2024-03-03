using UnityEngine;

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
                //int RandWinItemsCount = Random.Range(0, 10);
                //Debug.LogError("--- RandWinItemsCount=" + RandWinItemsCount);
                //if (RandWinItemsCount < 6)
                //    WinItemsCount = 3;
                //else if (RandWinItemsCount < 9)
                //    WinItemsCount = 4;
                //else
                //    WinItemsCount = 5;


                float randomValue = UnityEngine.Random.value;

                // Check which probability range the random number falls into
                if (randomValue <= probabilities[0])
                {
                    // 70% probability logic
                    Debug.Log("70% probability logic executed");
                    WinItemsCount = 3;
                    // Implement your logic here for 70% probability
                }
                else if (randomValue <= probabilities[0] + probabilities[1])
                {
                    // 30% probability logic
                    Debug.Log("30% probability logic executed");
                    WinItemsCount = 4;
                    // Implement your logic here for 30% probability
                }
                else if (randomValue <= probabilities[0] + probabilities[1] + probabilities[2])
                {
                    // 10% probability logic
                    Debug.Log("10% probability logic executed");
                    WinItemsCount = 5;
                    // Implement your logic here for 10% probability
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
                        //lineInfo.ReelItemsList[0].ActivateEffect(true);
                        //lineInfo.ReelItemsList[i].ActivateEffect(true);
                    }
                    else
                    {
                        break;
                    }
                }
                if (lineInfo.MatchCount >= 3)
                {
                    WinningLinesList.Add(lineInfo);
                    Debug.Log("WinLine Matchcount="+lineInfo.MatchCount);
                }
            }
            Debug.Log("--------- Find Winnings total winninglinescount=" + WinningLinesList.Count);
            ShowWinLineEffect();
        }
        public void ShowWinLineEffect()
        {
            for(int i=0;i<WinningLinesList.Count;i++)
            {
                for (int j = 0; j < WinningLinesList[i].MatchCount; j++)
                {
                    WinningLinesList[i].ReelItemsList[j].ActivateEffect(true);
                }
                //WinningLinesList[i].ReelItemsList[1].ActivateEffect(true);
                //WinningLinesList[i].ReelItemsList[2].ActivateEffect(true);
                //lineInfos[lineInfos.IndexOf(WinningLinesList[i])].ReelItemsList[0].ActivateEffect(true);
                //lineInfos[lineInfos.IndexOf(WinningLinesList[i])].ReelItemsList[1].ActivateEffect(true);
                //lineInfos[lineInfos.IndexOf(WinningLinesList[i])].ReelItemsList[2].ActivateEffect(true);
            }
        }
        public void ReelFinishedAction()
        {
            ReelsFinishedCount++;
            if (ReelsFinishedCount == 5)
            {
                // All Reels rotation finished and stopeed
                FindWinningLines();
                Invoke(nameof(SetReady), 2);
            }
        }
        public void SetReady()
        {
            ReelsFinishedCount = 0;
            UIHandler.Instance.StartBtn.interactable = true;
            foreach (LineInfo lineInfo in lineInfos)
            {
                for (int i = 0; i < lineInfo.ReelItemsList.Length; i++)
                    lineInfo.ReelItemsList[i].ActivateEffect(false);
            }
        }
    }
}
