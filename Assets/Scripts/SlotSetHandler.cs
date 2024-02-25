using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSetHandler : MonoBehaviour
{
    public float speed = 1;
    public GameObject LastTopObj;
    public bool IsStartMachine = false;
    public float SlotPosY = 0;
    public GameObject[] ItemHandlersList;
    public float TotalAnimTime;
    public bool StopAtFinalItem = false;
    public int WinItemIndex = 0;
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
        StartGame();
    }
   
    public void StartGame()
    {
        speed = 10;
        IsStartMachine = true;
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
        speed =speed*0.5f;
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

        for (int i=0;i < ItemHandlersList.Length;i++)
        {
            ItemHandlersList[i].transform.localPosition -= new Vector3(0, speed, 0);

            //if (obj.transform.localPosition.y <= -400f)
            //{
            //    //Debug.Log("-----localpositiony=" + ItemHandlersList[0].transform.localPosition.y + "   lasttopobjposy=" + LastTopObj.transform.localPosition.y);
            //    obj.transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
            //    LastTopObj = obj.gameObject;
            //}
        }
        foreach (GameObject obj in ItemHandlersList)
        {
            // obj.transform.localPosition -= new Vector3(0, speed, 0);
            if (obj.transform.localPosition.y <= -400f)
            {
                //Debug.Log("-----localpositiony=" + ItemHandlersList[0].transform.localPosition.y + "   lasttopobjposy=" + LastTopObj.transform.localPosition.y);
                obj.transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
                LastTopObj = obj.gameObject;
            }
        }
        if (StopAtFinalItem && ItemHandlersList[WinItemIndex].transform.localPosition.y == 0)
        {
            IsStartMachine = false;
            UIHandler.Instance.StartBtn.interactable = true;
        }
        return;
        ItemHandlersList[0].transform.localPosition -= new Vector3(0, speed, 0);

        ItemHandlersList[1].transform.localPosition -= new Vector3(0, speed , 0);
       


        ItemHandlersList[2].transform.localPosition -= new Vector3(0, speed , 0);
        


        ItemHandlersList[3].transform.localPosition -= new Vector3(0, speed , 0);
       

        if (ItemHandlersList[0].transform.localPosition.y <= -400f)
        {
            Debug.Log("-----localpositiony=" + ItemHandlersList[0].transform.localPosition.y + "   lasttopobjposy=" + LastTopObj.transform.localPosition.y);
            ItemHandlersList[0].transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
            LastTopObj = ItemHandlersList[0].gameObject;
        }
        if (ItemHandlersList[1].transform.localPosition.y <= -400f)
        {
            ItemHandlersList[1].transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
            LastTopObj = ItemHandlersList[1].gameObject;
        }
        if (ItemHandlersList[2].transform.localPosition.y <= -400f)
        {
            ItemHandlersList[2].transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
            LastTopObj = ItemHandlersList[2].gameObject;
        }
        if (ItemHandlersList[3].transform.localPosition.y <= -400f)
        {
            ItemHandlersList[3].transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
            LastTopObj = ItemHandlersList[3].gameObject;
        }
    }
}
