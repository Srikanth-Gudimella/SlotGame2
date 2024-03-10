using UnityEngine;
using SlotGame;
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
