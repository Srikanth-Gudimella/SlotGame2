using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelHandler : MonoBehaviour
{
    public int TotalReelItems;
    public float speed = 1;
    public GameObject LastTopObj;
    public bool IsStartMachine = false;
    public ReelItem[] ReelItemsList;
    public float TotalAnimTime;
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
    void SetStartingFixedIndexsRandom()//This is temporary code , first shuffling ReelimgIndexList then adding first 3 in StartingFixedIndexs
    {
        StartingFixedIndexs.Clear();
        while (StartingFixedIndexs.Count < 3)
        {
            int randomNumber = UnityEngine.Random.Range(0, 10);
            if(!StartingFixedIndexs.Contains(randomNumber))
            {
                StartingFixedIndexs.Add(randomNumber);
            }
        }
    }
    void ShuffleReelImgIndexList()
    {
        SetStartingFixedIndexsRandom();//this we need to be in our control, we need to handle this, for now this also random

        ReelImgIndexList.Clear();
        for (int i = 0; i < TotalReelItems; i++)
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

        for (int i = 3; i < ReelImgIndexList.Count-1; i++)
        {
            int k = UnityEngine.Random.Range(i + 1, ReelImgIndexList.Count);
            int value = ReelImgIndexList[k];
            ReelImgIndexList[k] = ReelImgIndexList[i];
            ReelImgIndexList[i] = value;
        }
    }
    void SetItemImgs()
    {
        for(int i=0;i< ReelItemsList.Length;i++)
        {
            ReelItemsList[i].ItemIndex = ReelImgIndexList[i];
            ReelItemsList[i].SetItemImg();
        }
    }
    
    private void OnStartClick()
    {
        ShuffleReelImgIndexList();
        SetItemImgs();
        // Need to change first 3 images after they move out of screen, becuase at this time this is in visible area,
        // so if we change now then it will be like sudden change/jerk
        StartGame();
    }
    

    public void StartGame()
    {
        speed = 10;
        IsStartMachine = true;
        StopAtFinalItem = false;
        IsSetFinalSpeed = false;
        //Invoke(nameof(StartSlotMachine), 1);
        Invoke(nameof(SetFinalItem), TotalAnimTime);
        Invoke(nameof(SlowSpeed), 3 * TotalAnimTime / 4);
    }
   
    //void StartSlotMachine()
    //{
    //    IsStartMachine = true;
    //}
    void SlowSpeed()
    {
        speed = speed * 0.5f;
    }
    void SetFinalItem()
    {
        StopAtFinalItem = true;
    }
    private void Update()
    {
        if (!IsStartMachine)
            return;
        //SlotPosY -= speed * Time.deltaTime;
        //if (SlotPosY <= -200)
        //    SlotPosY = 0;


        //Debug.Log("-----localpositiony 0000=" + ItemHandlersList[0].transform.localPosition.y);
        //Debug.Log("-----localpositiony 1111=" + ItemHandlersList[1].transform.localPosition.y);
        //Debug.Log("-----localpositiony 2222=" + ItemHandlersList[2].transform.localPosition.y);
        //Debug.Log("-----localpositiony 3333=" + ItemHandlersList[3].transform.localPosition.y);
        //if(StopAtFinalItem && !IsSetFinalSpeed && ItemHandlersList[WinItemIndex].transform.localPosition.y>100 && ItemHandlersList[WinItemIndex].transform.localPosition.y<400)
        //{
        //    IsSetFinalSpeed = true;
        //    speed = speed * 0.5f;
        //}

        for (int i = 0; i < ReelItemsList.Length; i++)
        {
            ReelItemsList[i].transform.localPosition -= new Vector3(0, speed, 0);

            //if (obj.transform.localPosition.y <= -400f)
            //{
            //    //Debug.Log("-----localpositiony=" + ItemHandlersList[0].transform.localPosition.y + "   lasttopobjposy=" + LastTopObj.transform.localPosition.y);
            //    obj.transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
            //    LastTopObj = obj.gameObject;
            //}
        }
        foreach (ReelItem obj in ReelItemsList)
        {
            // obj.transform.localPosition -= new Vector3(0, speed, 0);
            if (obj.transform.localPosition.y <= (TotalReelItems-2)*-200f)
            {
                //Debug.Log("-----localpositiony=" + ItemHandlersList[0].transform.localPosition.y + "   lasttopobjposy=" + LastTopObj.transform.localPosition.y);
                obj.transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
                LastTopObj = obj.gameObject;
            }
        }
        if (StopAtFinalItem && ReelItemsList[WinItemIndex].transform.localPosition.y == 200)
        {
            IsStartMachine = false;
            GameManager.Instance.ReelFinishedAction();
            //UIHandler.Instance.StartBtn.interactable = true;
        }
        //return;
        //ItemHandlersList[0].transform.localPosition -= new Vector3(0, speed, 0);

        //ItemHandlersList[1].transform.localPosition -= new Vector3(0, speed , 0);



        //ItemHandlersList[2].transform.localPosition -= new Vector3(0, speed , 0);



        //ItemHandlersList[3].transform.localPosition -= new Vector3(0, speed , 0);


        //if (ItemHandlersList[0].transform.localPosition.y <= -400f)
        //{
        //    Debug.Log("-----localpositiony=" + ItemHandlersList[0].transform.localPosition.y + "   lasttopobjposy=" + LastTopObj.transform.localPosition.y);
        //    ItemHandlersList[0].transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
        //    LastTopObj = ItemHandlersList[0].gameObject;
        //}
        //if (ItemHandlersList[1].transform.localPosition.y <= -400f)
        //{
        //    ItemHandlersList[1].transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
        //    LastTopObj = ItemHandlersList[1].gameObject;
        //}
        //if (ItemHandlersList[2].transform.localPosition.y <= -400f)
        //{
        //    ItemHandlersList[2].transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
        //    LastTopObj = ItemHandlersList[2].gameObject;
        //}
        //if (ItemHandlersList[3].transform.localPosition.y <= -400f)
        //{
        //    ItemHandlersList[3].transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
        //    LastTopObj = ItemHandlersList[3].gameObject;
        //}
    }
}
