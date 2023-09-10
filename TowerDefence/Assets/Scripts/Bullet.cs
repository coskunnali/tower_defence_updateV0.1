using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;

    public float speed = 50f;
    public GameObject impactEffect; //Hedefe çarpma efekti
    //Hedefi ayarlamak için kullanýlan fonksiyon
    public void Seek(Transform _target)
    {
        target = _target; //Verilen hedefi atanýr.
    }

    private void Update()
    {
        //Eðer hedef yoksa, bu füze oyun nesnesini yok eder ve iþlemi sonlandýrýr.
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //Füzenin hedefe doðru hareket etmesi için yön vektörü hesaplanýr.
        Vector3 direction = target.position - transform.position;

        //Füzenin gideceði mesafeyi hesaplar.
        float distanceThisFrame = speed * Time.deltaTime;

        //Eðer füze, hedefe ulaþacaksa, HitTarget fonksiyonunu çaðýrýr ve iþlemi sonlandýrýr.
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            PlayerState.Money += 50;
            return;
        }

        //Füze, hedefe ulaþmadýysa, belirtilen hýz ve yönde ilerlemeye devam eder.
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        //Hedefe çarpma efektini oluþturur.
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Oluþturulan efekti 2 saniye sonra yok edelim.
        Destroy(effectIns, 2f);

        //Hedefi yok edelim.
        Destroy(target.gameObject);

        //Mermiyi yok edelim.
        Destroy(gameObject);
    }
}
