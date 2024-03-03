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
        public int WinLineIndex,WinItemIndex;

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
                Debug.LogError("--- WinLineIndex=" + WinLineIndex);
                Debug.LogError("--- WinItemIndex=" + WinItemIndex);
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
                WinningLinesList[i].ReelItemsList[0].ActivateEffect(true);
                WinningLinesList[i].ReelItemsList[1].ActivateEffect(true);
                WinningLinesList[i].ReelItemsList[2].ActivateEffect(true);
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
