using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class BuildManager : MonoBehaviour
{
    //Bu sýnýfýn tek bir örneðine eriþim saðlayacak bir "instance" adlý statik bir özellik (property) tanýmlanýr
    public static BuildManager instance;

    private void Awake()
    {
        //Eðer daha önce bir BuildManager örneði oluþturulmuþsa hata verir.
        if (instance != null)
        {
            Debug.Log("Sahnede birden fazla BuildManager bulunuyor.");
            return;
        }
        instance = this;
    }

    public GameObject firstTurretPrefab; //1. kule

    private TurretBluePrint turretToBuild; // Ýnþa edilecek kule bilgisi.

    public bool CanBuild { get { return turretToBuild != null; } } // Kule inþa edilebilir mi?
    public bool HasMoney { get { return PlayerState.Money >= turretToBuild.cost; } } // Kule inþa edilebilir mi?

    public void BuildTurretOn(Node node)
    {
        //Oyuncunun parasý, seçilen kuleyi inþa etmek için yeterli mi?
        if (PlayerState.Money < turretToBuild.cost)
        {
            Debug.Log("Yeterli Paranýz Yok");
            return;
        }
        //Kuleyi inþa etmek için gerekli parayý oyuncunun parasýndan çýkar.
        PlayerState.Money -= turretToBuild.cost;

        //Ýnþa edildiðini ve kalan para miktarýný bildir.
        Debug.Log("Kule inþa edildi. Kalan Paranýz:" + PlayerState.Money);

        //Seçilen kuleyi küpün konumuna oluþturma
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret; //Küpün üzerine inþa edilen kuleyi atar.
        //return;
    }

    //Ýnþa edilecek kuleyi seçmek için kullanýlan fonksiyon
    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret; //Seçilen kuleyi ayarlar.
    }

}
