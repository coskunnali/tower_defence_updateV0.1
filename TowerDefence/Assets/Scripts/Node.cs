using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //oyun nesnelerini ve kullanýcý etkileþimi arasýnda ki olaylarý iþlemek için kullanýlýr.

public class Node : MonoBehaviour
{
    public Color hoverColor; //Fare üzerine gelindiðinde küp rengi
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset; //Kule yerleþtirme konumunun küpün merkezinden ne kadar kaydýrýlacaðýný belirler.

    [Header("Optional")]
    public GameObject turret; //Bu küpe yerleþtirilmiþ kule oyun nesnesi

    public Renderer rend; // Küpün render bileþeni
    public Color startColor; //Küpün baþlangýç rengi

    public TurretBluePrint turretBlueprint;
    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    //Kule inþa konumunu hesaplamak için kullanýlacak fonksiyon
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset; //Küpün merkezine pozisyon ofsetini ekler.
    }

    private void OnMouseDown()
    {
        //Eðer fare üzerinde bir UI öðesi varsa iþlemi iptal etsin.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //Eðer inþa yapma yeteneði yoksa iþlemi iptal eder.
        if (!buildManager.CanBuild)
        {
            return;
        }

        //Eðer küpte zaten bir kule varsa iþlemi iptal eder ve bir hata mesajý gösterilir.
        if (turret != null)
        {

            Debug.Log("Ýnþa edilemez alan");
            return;
        }

        //Kule inþa yöneticisi aracýlýðýyla kule inþa iþlemi baþlatýlýr.
        buildManager.BuildTurretOn(this);
    }

    public void SellTurret()
    {
        PlayerState.Money += turretBlueprint.GetSellAmount();
        Destroy(turret);
        turretBlueprint = null;
    }

    private void OnMouseEnter()
    {
        //Eðer fare üzerinde bir UI öðesi varsa iþlemi iptal etsin.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //Eðer inþa yapma yeteneði yoksa iþlemi iptal eder.
        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            //Küpün rengini fare üzerine gelindiðinde rengi deðiþsin.
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }
    private void OnMouseExit()
    {
        //Küpün rengini baþlangýç rengine geri döndür.
        rend.material.color = startColor;
    }
}
