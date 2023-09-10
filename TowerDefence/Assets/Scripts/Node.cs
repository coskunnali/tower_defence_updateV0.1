using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //oyun nesnelerini ve kullan�c� etkile�imi aras�nda ki olaylar� i�lemek i�in kullan�l�r.

public class Node : MonoBehaviour
{
    public Color hoverColor; //Fare �zerine gelindi�inde k�p rengi
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset; //Kule yerle�tirme konumunun k�p�n merkezinden ne kadar kayd�r�laca��n� belirler.

    [Header("Optional")]
    public GameObject turret; //Bu k�pe yerle�tirilmi� kule oyun nesnesi

    public Renderer rend; // K�p�n render bile�eni
    public Color startColor; //K�p�n ba�lang�� rengi

    public TurretBluePrint turretBlueprint;
    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    //Kule in�a konumunu hesaplamak i�in kullan�lacak fonksiyon
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset; //K�p�n merkezine pozisyon ofsetini ekler.
    }

    private void OnMouseDown()
    {
        //E�er fare �zerinde bir UI ��esi varsa i�lemi iptal etsin.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //E�er in�a yapma yetene�i yoksa i�lemi iptal eder.
        if (!buildManager.CanBuild)
        {
            return;
        }

        //E�er k�pte zaten bir kule varsa i�lemi iptal eder ve bir hata mesaj� g�sterilir.
        if (turret != null)
        {

            Debug.Log("�n�a edilemez alan");
            return;
        }

        //Kule in�a y�neticisi arac�l���yla kule in�a i�lemi ba�lat�l�r.
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
        //E�er fare �zerinde bir UI ��esi varsa i�lemi iptal etsin.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //E�er in�a yapma yetene�i yoksa i�lemi iptal eder.
        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            //K�p�n rengini fare �zerine gelindi�inde rengi de�i�sin.
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }
    private void OnMouseExit()
    {
        //K�p�n rengini ba�lang�� rengine geri d�nd�r.
        rend.material.color = startColor;
    }
}
