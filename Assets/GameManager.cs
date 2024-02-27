using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Sprite[] ReelItemImgs;
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
}
