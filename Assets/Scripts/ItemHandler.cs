using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    //public Vector3 targetPosition;
    //public SlotSetHandler _ThisSLotSetHandler;
    // Start is called before the first frame update


    //void FixedUpdate()
    //{
    //    if (!_ThisSLotSetHandler.IsStartMachine)
    //        return;
    //    // Move the object towards the target position
    //    transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, _ThisSLotSetHandler.speed * Time.deltaTime);

    //    // Check if the object has reached the target position
    //    if (transform.localPosition.y <= targetPosition.y)
    //    {
    //        transform.localPosition = _ThisSLotSetHandler.LastTopObj.transform.localPosition-new Vector3(0,-200f,0);
    //        _ThisSLotSetHandler.LastTopObj = this.gameObject;

    //        // If the object has reached the target position, swap the target position
    //        //targetPosition = (targetPosition == topPosition) ? bottomPosition : topPosition;
    //    }
    //}


    //public Transform targetPositionTr; // Target position for the object to move to
    //public float movementSpeed = 5f; // Speed of the movement

    //private Vector3 startPosition;
    //private float journeyLength;
    //private float startTime;

    //void Start()
    //{
    //    // Store the start position of the object
    //    startPosition = transform.position;

    //    // Calculate the distance between start and target positions
    //    journeyLength = Vector3.Distance(startPosition, targetPositionTr.position);

    //    // Record the time when movement starts
    //    startTime = Time.time;
    //}
    //void Update()
    //{
    //    if (!_ThisSLotSetHandler.IsStartMachine)
    //        return;
    //    // Calculate the fraction of journey completed
    //    //float journeyFraction = (Time.time - startTime) * movementSpeed / journeyLength;

    //    // Interpolate between the start and target positions
    //    transform.localPosition = Vector3.Lerp(transform.localPosition, targetPositionTr.localPosition, _ThisSLotSetHandler.speed * Time.deltaTime);

    //    // Check if the object has reached the target position
    //    if (transform.localPosition.y <= targetPosition.y)
    //    {
    //        Debug.Log("----- reached target position");
    //        transform.localPosition = new Vector3(0, 400, 0);
    //        startTime = Time.time;
    //        startPosition = transform.position;

    //        // Object has reached the target position
    //        // You can add additional logic here if needed
    //    }
    //}
    public RectTransform targetPosition,StartPositionTr; // Target position for the canvas image to move to
    public float movementSpeed = 5f; // Speed of the movement

    private RectTransform rectTransform;
    public int Index;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Invoke(nameof(MoveToTarget),1);
    }

    public void MoveToTarget()
    {
        // Start a coroutine to smoothly move the canvas image to the target position
        StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        Vector3 startPosition = rectTransform.position;
        Vector3 targetPosition = this.targetPosition.position;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float elapsedTime = 0f;

        while (elapsedTime < movementSpeed)
        {
            elapsedTime += Time.deltaTime;
            float fraction = Mathf.Clamp01(elapsedTime / movementSpeed)* Index;
            rectTransform.position = Vector3.Lerp(startPosition, targetPosition, fraction);
            yield return null; // Wait for the end of the frame
        }

        // Ensure the object is precisely at the target position
        rectTransform.position = StartPositionTr.position;
        StartCoroutine(MoveToTargetCoroutine());
            
    }
}
