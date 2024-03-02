using UnityEngine;

namespace SlotGame
{
    public class GameManager : GameConstants
    {
        public static GameManager Instance;

        public Sprite[] ReelItemImgs;
        public int ReelsFinishedCount;

        private void Awake()
        {
            Instance = this;
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
                    WinningLinesList
                        .Add(lineInfo);
                }
            }
            Debug.Log("--------- Find Winnings total winninglinescount=" + WinningLinesList.Count);
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
        }
    }
}
