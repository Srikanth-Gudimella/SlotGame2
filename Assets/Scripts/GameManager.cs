using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Sprite[] ReelItemImgs;
    public static GameManager Instance;
    public LineHandler[] LinesList;
    public List<LineHandler> WinningLinesList = new List<LineHandler>();
    public int ReelsFinishedCount;
    public ReelsList_FristIndexs[] _ReelsListFristIndexs;
    //public 
    private void Awake()
    {
        Instance = this;
    }
    public void FindWinningLines()
    {
        Debug.Log("------- FindWinningLines");
        WinningLinesList.Clear();
        foreach(LineHandler _lineHandler in LinesList)
        {
            _lineHandler.FirstReelItemIndex = _lineHandler.ReelItemsList[0].ItemIndex;
            _lineHandler.MatchCount = 1;
            for (int i=1;i<_lineHandler.ReelItemsList.Length;i++)
            {
                if (_lineHandler.FirstReelItemIndex == _lineHandler.ReelItemsList[i].ItemIndex)
                {
                    _lineHandler.MatchCount++;
                }
                else
                {
                    break;
                }
            }
            if(_lineHandler.MatchCount >= 3)
            {
                WinningLinesList.Add(_lineHandler);
            }
        }

        Debug.Log("--------- Find Winnings total winninglinescount="+WinningLinesList.Count);
    }
    public void ReelFinishedAction()
    {
        ReelsFinishedCount++;
        if(ReelsFinishedCount == 5)
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
