using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;

    [Header("Öznitelik")]
    public float range = 10f;
    public float fireRate = 1f;
    public float fireCountDown = 0f;
    public float turnSpeed = 5f;

    [Header("Unity Kurulum Alanlarý")]
    public string enemyTag = "Enemy";
    public Transform ToRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform firePoint1;

    Animator animator;

    void Start()
    {
        //Turret'ýn hedefini düzenli olarak güncellemek için UpdateTarget fonksiyonunu belirli bir sýklýkla çaðýrýrýz.
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    //Hedefi güncellemek için kullanýlacak fonskiyondur.
    void UpdateTarget()
    {
        //Enemy tag'ýný taþýyan tüm GameObjectleri bulur ve alýr.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //En kýsa mesafeyi sýfýra ayarlar.
        float shortestDistance = Mathf.Infinity;
        //En yakýn düþmaný saklamak için bir deðiþken
        GameObject nearstEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            //Turret ile düþman arasýndaki mesafeyi hesaplar.
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            //Eðer bu düþman, bir önceki düþmandan daha yakýnsa, bu düþmaný en yakýn düþman olarak iþaretle.
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearstEnemy = enemy;
            }
        }

        //Eðer en yakýn düþman bulunmuþsa ve mesafe belirlenen menzil içindeyse, bu düþmaný hedef olarak belirle.
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
        //Eðer bir hedef yoksa, dönmeye gerek yoktur, bu yüzden fonksiyonu terk etmelidir.
        if (target == null)
        {
            return;
        }

        //Turret'ýn hedefe doðru dönmesini hesaplar.
        //1) hedef nesnesinin pozisyonu ve döndürülmek istenen nesnenin pozisyonu arasýndaki farký bul.Bu fark hedefe doðru olan yönü gösterir.
        Vector3 direction = target.position - transform.position;
        //2) direction vektörünü kullanarak döndürme iþlemi için bir rotasyon oluþturur.
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        //3) Mevcut nesnenin rotasyonu ile hedefe doðru bakma rotasyonu arasýnda yumuþak geçiþ yap.
        Vector3 rotation = Quaternion.Lerp(ToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;

        //4) Mevcut nesnenin yalnýzca y ekseni etrafýndaki rotasyounu ayarla.
        ToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);

        if (fireCountDown <= 0f)
        {
            Shoot(); //Eðer ateþleme süresi sýfýrsa,ateþ eder;
            fireCountDown = 1f / fireRate; //Bir sonraki atýþýn bekleme süresini ayarlar.
        }
        fireCountDown -= Time.deltaTime; //Atýþ bekleme süresini azaltýr, zamanlayýcýyý günceller.
    }

    void Shoot()
    {
        if (firePoint1 == null)
        {
            Debug.Log("Ateþ edildi");
            //Kurþunun nesnesini oluþturur ve ateþleme noktasýna konumlandýrýr.
            GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            //Oluþturulan kurþun nesnesinin Bullet bileþenini alýr.
            Bullet bullet = bulletGameObject.GetComponent<Bullet>();

            //Bullet bileþeni varsa, hedefi belirlemek için hedefi atar.
            if (bullet != null)
            {
                bullet.Seek(target);//Kurþunun hedefi belirlemesi için Seek fonksiyonunu çaðýrýr.
            }
        }

        if (firePoint1 != null)
        {
            Debug.Log("Ateþ edildi");
            //Kurþunun nesnesini oluþturur ve ateþleme noktasýna konumlandýrýr.
            GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            GameObject bullet1GameObject = (GameObject)Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
            //Oluþturulan kurþun nesnesinin Bullet bileþenini alýr.
            Bullet bullet = bulletGameObject.GetComponent<Bullet>();
            Bullet bullet1 = bullet1GameObject.GetComponent<Bullet>();

            //Bullet bileþeni varsa, hedefi belirlemek için hedefi atar.
            if (bullet != null)
            {
                bullet.Seek(target);//Kurþunun hedefi belirlemesi için Seek fonksiyonunu çaðýrýr.
            }

            if (bullet1 != null)
            {
                bullet1.Seek(target);//Kurþunun hedefi belirlemesi için Seek fonksiyonunu çaðýrýr.
            }
        }

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }



}
