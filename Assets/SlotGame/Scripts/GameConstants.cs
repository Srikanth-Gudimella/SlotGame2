using System.Collections.Generic;
using UnityEngine;

namespace SlotGame
{
    public class GameConstants:MonoBehaviour
    {
        public float TotalAnimTime;
        public int TotalReelItems;

        [System.Serializable]
        public class ReelInfo
        {
            public int[] ReelFirstIndexs;
        }
        public List<ReelInfo> reelInfos;
        

        [System.Serializable]
        public class LineInfo
        {
            public ReelItem[] ReelItemsList;
            public int FirstReelItemIndex = 0;
            public int MatchCount = 0;
        }
        public List<LineInfo> lineInfos;

        public List<LineInfo> WinningLinesList = new List<LineInfo>();
        public List<ReelHandler> reelHandlersList;

    }
}

