using UnityEngine;
using Custom.Utils;

namespace SlotGame
{
    public class SlotSetHandler : MonoBehaviour
    {
        public float speed = 1;
        public GameObject LastTopObj;
        public bool IsStartMachine = false;
        public float SlotPosY = 0;
        public ReelItem[] ReelItemsList;
        public float TotalAnimTime;
        public bool StopAtFinalItem = false;
        public int WinItemIndex = 0;
        bool IsSetFinalSpeed = false;
        public ReelHandler ThisReelHandlerRef;

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
            StopAtFinalItem = false;
            IsSetFinalSpeed = false;
            //Invoke(nameof(StartSlotMachine), 1);
            //Invoke(nameof(SetFinalItem), TotalAnimTime);
            //Invoke(nameof(SlowSpeed), 3 * TotalAnimTime / 4);

            CoroutineUtils.instance.WaitUntillGivenTime(TotalAnimTime, () =>
            {
                SetFinalItem();
            });
            CoroutineUtils.instance.WaitUntillGivenTime(3 * TotalAnimTime / 4, () =>
            {
                SlowSpeed();
            });
        }
        
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
                if (obj.transform.localPosition.y <= -400f)
                {
                    //Debug.Log("-----localpositiony=" + ItemHandlersList[0].transform.localPosition.y + "   lasttopobjposy=" + LastTopObj.transform.localPosition.y);
                    obj.transform.localPosition = new Vector3(0, LastTopObj.transform.localPosition.y + 200, 0);
                    LastTopObj = obj.gameObject;
                }
            }
            if (StopAtFinalItem && ReelItemsList[WinItemIndex].transform.localPosition.y == 0)
            {
                IsStartMachine = false;
                UIHandler.Instance.StartBtn.interactable = true;
            }
        }
    }
}
