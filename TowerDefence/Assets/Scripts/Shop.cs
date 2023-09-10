using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint firstTurret;
    public TurretBluePrint secondTurret;
    public TurretBluePrint thirdTurret;

    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    //Birinci kuleyi sat�n almak i�in �a��r�lan fonksiyon
    public void FirstPurchaseTurret()
    {
        Debug.Log("Birinci Kule sat�n al�nd�.");

        //BuildManager �zerinden se�ilen kuleyi ayarlar.
        buildManager.SelectTurretToBuild(firstTurret);
    }
    public void SecondPurchaseTurret()
    {
        Debug.Log("�kinci Kule sat�n al�nd�.");
        buildManager.SelectTurretToBuild(secondTurret);
    }
    public void ThirdPurchaseTurret()
    {
        Debug.Log("�kinci Kule sat�n al�nd�.");
        buildManager.SelectTurretToBuild(thirdTurret);
    }

    
}
