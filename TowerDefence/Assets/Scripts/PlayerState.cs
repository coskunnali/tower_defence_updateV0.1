using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static int Money;
    public static int Live;

    public int startMoney = 400;
    public int startLive = 1;
    void Start()
    {
        Money = startMoney;
        Live = startLive;
    }


}
