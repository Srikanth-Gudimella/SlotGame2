using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance;
    public static Action StartAction;
    public Button StartBtn;
    private void Awake()
    {
        Instance = this;
    }
    public void StartClick()
    {
        StartBtn.interactable = false;
        StartAction?.Invoke();
    }
}
