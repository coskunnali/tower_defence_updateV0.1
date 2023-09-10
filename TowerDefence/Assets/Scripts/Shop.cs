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

    //Birinci kuleyi satýn almak için çaðýrýlan fonksiyon
    public void FirstPurchaseTurret()
    {
        Debug.Log("Birinci Kule satýn alýndý.");

        //BuildManager üzerinden seçilen kuleyi ayarlar.
        buildManager.SelectTurretToBuild(firstTurret);
    }
    public void SecondPurchaseTurret()
    {
        Debug.Log("Ýkinci Kule satýn alýndý.");
        buildManager.SelectTurretToBuild(secondTurret);
    }
    public void ThirdPurchaseTurret()
    {
        Debug.Log("Ýkinci Kule satýn alýndý.");
        buildManager.SelectTurretToBuild(thirdTurret);
    }

    
}
