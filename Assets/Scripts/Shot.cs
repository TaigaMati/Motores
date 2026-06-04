using UnityEngine;
using UnityEngine.UI; 
using TMPro;  
using System.Collections;


public enum AmmoType
{
    Pistol,
    Shotgun
}

public class Shot : MonoBehaviour
{
    [Header("Referencias")]
    public Transform shootSpawn;
    public GameObject bulletPrefab;
    public Camera cam;

    [Header("Parámetros de disparo")]
    public float shotForce = 50f;
    public float shotRate = 0.5f;

    [Header("Munición")]
    public int maxAmmo = 6;        
    public int currentAmmo = 3;    
    public int totalAmmo = 30;     
    public float reloadTime = 2f;
    private bool isReloading = false;

    private float shotRateTime = 0f;

    [Header("Tipo de munición")]
    public AmmoType weaponAmmoType;

    [Header("Audio")]
    public AudioClip shootClip;       
    private AudioSource audioSource;  

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (isReloading) return;
        if (currentAmmo <= 0) return;

        if (Time.time > shotRateTime)
        {
            GameObject newBullet = Instantiate(
                bulletPrefab,
                shootSpawn.position,
                Quaternion.LookRotation(cam.transform.forward)
            );

            newBullet.transform.parent = null;            

            Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.isKinematic = false;
                bulletRb.linearVelocity = Vector3.zero;
                bulletRb.AddForce(cam.transform.forward * shotForce, ForceMode.Impulse);
            }

            Destroy(newBullet, 5f);

            
            if (shootClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(shootClip);
            }

            currentAmmo--;
            shotRateTime = Time.time + shotRate;
        }
    }

    public void AddAmmo(int amount)
    {
        totalAmmo += amount; 
    }

    public void StartReload()
    {
        if (!isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        if (totalAmmo <= 0) yield break;

        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        int neededAmmo = maxAmmo - currentAmmo;
        int ammoToLoad = Mathf.Min(neededAmmo, totalAmmo);

        currentAmmo += ammoToLoad;
        totalAmmo -= ammoToLoad;
        
        isReloading = false;

    }

}
