using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Bu s�n�f�n �zellikleri, Unity Edit�r�nde d�zenlenebilir ve bu nesnenin verileri kolayca serile�tirilebilir.

public class TurretBluePrint
{
    public GameObject prefab;
    public int cost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
