using Custom.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace SlotGame
{
    public class ReelHandler : MonoBehaviour
    {
        public int ReelIndex = 0;
        float speed = 1;
        public GameObject LastTopObj;
        public bool IsStartMachine = false;
        public List<ReelItem> ReelItemsList = new List<ReelItem>();
        public bool StopAtFinalItem = false;
        public int WinItemIndex = 0;//it should be 0 always, first 3 only come and stop always. these first 3 objects we will fix
        public List<int> ReelImgIndexList = new List<int>();
        public List<int> StartingFixedIndexs = new List<int>();


        public float SlotPosY = 0;
        bool IsSetFinalSpeed = false;

        private void OnEnable()
        {
            UIHandler.StartAction += OnStartClick;
        }
        private void OnDisable()
        {
            UIHandler.StartAction -= OnStartClick;
        }
        private void Start()
        {
            Debug.Log("------------- Reelhandler Start");
            ShuffleReelImgIndexList();
            SetItemImgs();
        }
        private void OnStartClick()
        {

            CoroutineUtils.instance.WaitUntillGivenTime(((ReelIndex+1)* 0.1f), () =>
            {
                //if (ReelIndex==0 && GameManager.Instance.IsActivateWinCondition)
                //{
                //    GameManager.Instance.WinLineIndex = UnityEngine.Random.Range(0, GameManager.Instance.lineInfos.Count);
                //    GameManager.Instance.WinItemIndex = UnityEngine.Random.Range(0, 10);
                //    Debug.LogError("--- WinLineIndex="+GameManager.Instance.WinLineIndex);
                //    Debug.LogError("--- WinItemIndex=" + GameManager.Instance.WinItemIndex);

                //}

                if (GameManager.Instance.IsActivateWinCondition)
                {
                    ShuffleReelImgIndexList_WinLogic();
                }
                else
                {
                    ShuffleReelImgIndexList();
                }
                SetItemImgs();
                // Need to change first 3 images after they move out of screen, becuase at this time this is in visible area,
                // so if we change now then it will be like sudden change/jerk
                StartGame();
            });

        }


        public void StartGame()
        {
            speed = Constants.SLOT_SPEED;
            IsStartMachine = true;
            StopAtFinalItem = false;
            IsSetFinalSpeed = false;
            IsSlowDownActivated = false;
            //Invoke(nameof(StartSlotMachine), 1);
            //Invoke(nameof(SetFinalItem), GameManager.Instance.TotalAnimTime);
            //Invoke(nameof(SlowSpeed), 3 * GameManager.Instance.TotalAnimTime / 4);

            CoroutineUtils.instance.WaitUntillGivenTime(GameManager.Instance.TotalAnimTime + (ReelIndex * 0.1f), () =>
            {
                SetFinalItem();
            });
            //CoroutineUtils.instance.WaitUntillGivenTime(3 * GameManager.Instance.TotalAnimTime / 4, () =>
            //{
            //    SlowSpeed();
            //});
        }
        void SetStartingFixedIndexsRandom()//This is temporary code , first shuffling ReelimgIndexList then adding first 3 in StartingFixedIndexs
        {
            //Debug.Log("------------- Reelhandler SetStartingFixedIndexsRandom Reelindex="+ReelIndex);
            StartingFixedIndexs.Clear();

            if (GameManager.Instance.useFixedStartIndexs)
            {
                for (int i = 0; i < GameManager.Instance.reelInfos[ReelIndex].ReelFirstIndexs.Length; i++)
                {
                    StartingFixedIndexs.Add(GameManager.Instance.reelInfos[ReelIndex].ReelFirstIndexs[i]);
                }
            }
            else
            {
                while (StartingFixedIndexs.Count < 3)
                {
                    int randomNumber = Random.Range(0, 10);
                    if (GameManager.Instance.IsActivateLossCondition && ReelIndex == 1)
                    {
                        Debug.Log("------------- Reelhandler SetStartingFixedIndexsRandom Reelindex=" + ReelIndex + "::count=" + (GameManager.Instance.reelHandlersList[0].StartingFixedIndexs));
                        if (!StartingFixedIndexs.Contains(randomNumber) && !GameManager.Instance.reelHandlersList[0].StartingFixedIndexs.Contains(randomNumber))
                        {
                            StartingFixedIndexs.Add(randomNumber);
                        }
                    }
                    else if (!StartingFixedIndexs.Contains(randomNumber))
                    {
                        StartingFixedIndexs.Add(randomNumber);
                    }
                }
            }
        }
        void ShuffleReelImgIndexList()
        {
            SetStartingFixedIndexsRandom();//this we need to be in our control, we need to handle this, for now this also random

            ReelImgIndexList.Clear();
            for (int i = 0; i < GameManager.Instance.TotalReelItems; i++)
            {
                ReelImgIndexList.Add(i);
            }

            // return;
            //below logic is for keep first 3 same as StartingFixedDIndex and then remaining Random
            for (int i = 0; i < StartingFixedIndexs.Count; i++)
            {
                int IndexOfValue = ReelImgIndexList.IndexOf(StartingFixedIndexs[i]);
                int value = ReelImgIndexList[IndexOfValue];
                ReelImgIndexList[IndexOfValue] = ReelImgIndexList[i];
                ReelImgIndexList[i] = value;
            }

            for (int i = 3; i < ReelImgIndexList.Count - 1; i++)
            {
                int k = Random.Range(i + 1, ReelImgIndexList.Count);
                int value = ReelImgIndexList[k];
                ReelImgIndexList[k] = ReelImgIndexList[i];
                ReelImgIndexList[i] = value;
            }
        }
        void ShuffleReelImgIndexList_WinLogic()
        {
            //SetStartingFixedIndexsRandom();//this we need to be in our control, we need to handle this, for now this also random

            ReelImgIndexList.Clear();

            while (ReelImgIndexList.Count < 15)
            {
                int randomNumber = Random.Range(0, 15);
                if (!ReelImgIndexList.Contains(randomNumber))
                {
                    ReelImgIndexList.Add(randomNumber);
                }
            }
            if (ReelIndex < GameManager.Instance.WinItemsCount)
            {
                //swapping of required item 
                int IndexOfRequiredWinItemIndex = ReelImgIndexList.IndexOf(GameManager.Instance.WinItemIndex);
                int IndexOfLineOfThisReel = ReelItemsList.IndexOf(GameManager.Instance.lineInfos[GameManager.Instance.WinLineIndex].ReelItemsList[ReelIndex]);
                int valueOfReqWinItemIndex = ReelImgIndexList[IndexOfRequiredWinItemIndex];
                int valueOfLineOfReelItem = ReelImgIndexList[IndexOfLineOfThisReel];
                ReelImgIndexList[IndexOfRequiredWinItemIndex] = valueOfLineOfReelItem;
                ReelImgIndexList[IndexOfLineOfThisReel] = valueOfReqWinItemIndex;
            }
        }
        void SetItemImgs()
        {
            for (int i = 0; i < ReelItemsList.Count; i++)
            {
                ReelItemsList[i].ItemIndex = ReelImgIndexList[i];
                ReelItemsList[i].SetItemImg();
            }
        }

        void SlowSpeed()
        {
            speed *= 0.5f;
        }
        void SetFinalItem()
        {
            Debug.LogError("------ SetfinalItem name="+gameObject.name);
            StopAtFinalItem = true;
           // Time.timeScale = 0;
        }
        bool IsSlowDownActivated = false;
        private void Update()
        {
            if (!IsStartMachine)
                return;

            for (int i = 0; i < ReelItemsList.Count; i++)
            {
                ReelItemsList[i].transform.localPosition -= new Vector3(0, speed, 0);
            }
            foreach (ReelItem obj in ReelItemsList)
            {
                // obj.transform.localPosition -= new Vector3(0, speed, 0);
                if (obj.transform.localPosition.y <= (GameManager.Instance.TotalReelItems - 2) * -200f)//down last point
                {
                    obj.transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
                    LastTopObj = obj.gameObject;
                }
            }
            if(StopAtFinalItem && !IsSlowDownActivated && ReelItemsList[WinItemIndex].transform.localPosition.y <= -1600)
            {
                IsSlowDownActivated = true;
                SlowSpeed();
            }
            if (StopAtFinalItem && (ReelIndex==0 || (ReelIndex>0 && !GameManager.Instance.reelHandlersList[ReelIndex-1].IsStartMachine))&& 
                ReelItemsList[WinItemIndex].transform.localPosition.y == 200)
            {
                IsStartMachine = false;
                GameManager.Instance.ReelFinishedAction();
                //UIHandler.Instance.StartBtn.interactable = true;
            }
            
        }
    }
}
