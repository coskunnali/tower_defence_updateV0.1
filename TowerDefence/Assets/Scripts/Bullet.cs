using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;

    public float speed = 50f;
    public GameObject impactEffect; //Hedefe �arpma efekti
    //Hedefi ayarlamak i�in kullan�lan fonksiyon
    public void Seek(Transform _target)
    {
        target = _target; //Verilen hedefi atan�r.
    }

    private void Update()
    {
        //E�er hedef yoksa, bu f�ze oyun nesnesini yok eder ve i�lemi sonland�r�r.
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //F�zenin hedefe do�ru hareket etmesi i�in y�n vekt�r� hesaplan�r.
        Vector3 direction = target.position - transform.position;

        //F�zenin gidece�i mesafeyi hesaplar.
        float distanceThisFrame = speed * Time.deltaTime;

        //E�er f�ze, hedefe ula�acaksa, HitTarget fonksiyonunu �a��r�r ve i�lemi sonland�r�r.
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            PlayerState.Money += 50;
            return;
        }

        //F�ze, hedefe ula�mad�ysa, belirtilen h�z ve y�nde ilerlemeye devam eder.
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        //Hedefe �arpma efektini olu�turur.
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Olu�turulan efekti 2 saniye sonra yok edelim.
        Destroy(effectIns, 2f);

        //Hedefi yok edelim.
        Destroy(target.gameObject);

        //Mermiyi yok edelim.
        Destroy(gameObject);
    }
}
