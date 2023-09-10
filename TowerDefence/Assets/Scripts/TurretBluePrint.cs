using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Bu sýnýfýn özellikleri, Unity Editöründe düzenlenebilir ve bu nesnenin verileri kolayca serileþtirilebilir.

public class TurretBluePrint
{
    public GameObject prefab;
    public int cost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
