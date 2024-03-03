using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTest : MonoBehaviour
{
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
        //Debug.LogError("RandomNumber="+Random.Range(0,10));
    }
    }
