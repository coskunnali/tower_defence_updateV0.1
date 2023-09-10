using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;

    [Header("�znitelik")]
    public float range = 10f;
    public float fireRate = 1f;
    public float fireCountDown = 0f;
    public float turnSpeed = 5f;

    [Header("Unity Kurulum Alanlar�")]
    public string enemyTag = "Enemy";
    public Transform ToRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePoint1;

    Animator animator;

    void Start()
    {
        //Turret'�n hedefini d�zenli olarak g�ncellemek i�in UpdateTarget fonksiyonunu belirli bir s�kl�kla �a��r�r�z.
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    //Hedefi g�ncellemek i�in kullan�lacak fonskiyondur.
    void UpdateTarget()
    {
        //Enemy tag'�n� ta��yan t�m GameObjectleri bulur ve al�r.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //En k�sa mesafeyi s�f�ra ayarlar.
        float shortestDistance = Mathf.Infinity;
        //En yak�n d��man� saklamak i�in bir de�i�ken
        GameObject nearstEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            //Turret ile d��man aras�ndaki mesafeyi hesaplar.
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            //E�er bu d��man, bir �nceki d��mandan daha yak�nsa, bu d��man� en yak�n d��man olarak i�aretle.
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearstEnemy = enemy;
            }
        }

        //E�er en yak�n d��man bulunmu�sa ve mesafe belirlenen menzil i�indeyse, bu d��man� hedef olarak belirle.
        if (nearstEnemy != null && shortestDistance <= range)
        {
            target = nearstEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        //E�er bir hedef yoksa, d�nmeye gerek yoktur, bu y�zden fonksiyonu terk etmelidir.
        if (target == null)
        {
            return;
        }

        //Turret'�n hedefe do�ru d�nmesini hesaplar.
        //1) hedef nesnesinin pozisyonu ve d�nd�r�lmek istenen nesnenin pozisyonu aras�ndaki fark� bul.Bu fark hedefe do�ru olan y�n� g�sterir.
        Vector3 direction = target.position - transform.position;
        //2) direction vekt�r�n� kullanarak d�nd�rme i�lemi i�in bir rotasyon olu�turur.
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        //3) Mevcut nesnenin rotasyonu ile hedefe do�ru bakma rotasyonu aras�nda yumu�ak ge�i� yap.
        Vector3 rotation = Quaternion.Lerp(ToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;

        //4) Mevcut nesnenin yaln�zca y ekseni etraf�ndaki rotasyounu ayarla.
        ToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);

        if (fireCountDown <= 0f)
        {
            Shoot(); //E�er ate�leme s�resi s�f�rsa,ate� eder;
            fireCountDown = 1f / fireRate; //Bir sonraki at���n bekleme s�resini ayarlar.
        }
        fireCountDown -= Time.deltaTime; //At�� bekleme s�resini azalt�r, zamanlay�c�y� g�nceller.
    }

    void Shoot()
    {
        if (firePoint1 == null)
        {
            Debug.Log("Ate� edildi");
            //Kur�unun nesnesini olu�turur ve ate�leme noktas�na konumland�r�r.
            GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            //Olu�turulan kur�un nesnesinin Bullet bile�enini al�r.
            Bullet bullet = bulletGameObject.GetComponent<Bullet>();

            //Bullet bile�eni varsa, hedefi belirlemek i�in hedefi atar.
            if (bullet != null)
            {
                bullet.Seek(target);//Kur�unun hedefi belirlemesi i�in Seek fonksiyonunu �a��r�r.
            }
        }

        if (firePoint1 != null)
        {
            Debug.Log("Ate� edildi");
            //Kur�unun nesnesini olu�turur ve ate�leme noktas�na konumland�r�r.
            GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            GameObject bullet1GameObject = (GameObject)Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
            //Olu�turulan kur�un nesnesinin Bullet bile�enini al�r.
            Bullet bullet = bulletGameObject.GetComponent<Bullet>();
            Bullet bullet1 = bullet1GameObject.GetComponent<Bullet>();

            //Bullet bile�eni varsa, hedefi belirlemek i�in hedefi atar.
            if (bullet != null)
            {
                bullet.Seek(target);//Kur�unun hedefi belirlemesi i�in Seek fonksiyonunu �a��r�r.
            }

            if (bullet1 != null)
            {
                bullet1.Seek(target);//Kur�unun hedefi belirlemesi i�in Seek fonksiyonunu �a��r�r.
            }
        }

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }



}
