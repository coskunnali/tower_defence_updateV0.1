using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class BuildManager : MonoBehaviour
{
    //Bu s�n�f�n tek bir �rne�ine eri�im sa�layacak bir "instance" adl� statik bir �zellik (property) tan�mlan�r
    public static BuildManager instance;

    private void Awake()
    {
        //E�er daha �nce bir BuildManager �rne�i olu�turulmu�sa hata verir.
        if (instance != null)
        {
            Debug.Log("Sahnede birden fazla BuildManager bulunuyor.");
            return;
        }
        instance = this;
    }

    public GameObject firstTurretPrefab; //1. kule

    private TurretBluePrint turretToBuild; // �n�a edilecek kule bilgisi.

    public bool CanBuild { get { return turretToBuild != null; } } // Kule in�a edilebilir mi?
    public bool HasMoney { get { return PlayerState.Money >= turretToBuild.cost; } } // Kule in�a edilebilir mi?

    public void BuildTurretOn(Node node)
    {
        //Oyuncunun paras�, se�ilen kuleyi in�a etmek i�in yeterli mi?
        if (PlayerState.Money < turretToBuild.cost)
        {
            Debug.Log("Yeterli Paran�z Yok");
            return;
        }
        //Kuleyi in�a etmek i�in gerekli paray� oyuncunun paras�ndan ��kar.
        PlayerState.Money -= turretToBuild.cost;

        //�n�a edildi�ini ve kalan para miktar�n� bildir.
        Debug.Log("Kule in�a edildi. Kalan Paran�z:" + PlayerState.Money);

        //Se�ilen kuleyi k�p�n konumuna olu�turma
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret; //K�p�n �zerine in�a edilen kuleyi atar.
        //return;
    }

    //�n�a edilecek kuleyi se�mek i�in kullan�lan fonksiyon
    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret; //Se�ilen kuleyi ayarlar.
    }

}
