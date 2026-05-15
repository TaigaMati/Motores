using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Header("Configuración")]
    public AmmoType ammoType;
    public int ammoAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerInteraction player = other.GetComponent<PlayerInteraction>();
        if (player == null) player = other.GetComponentInParent<PlayerInteraction>();

        if (player != null && player.HeldWeapon != null)
        {
            Shot weapon = player.HeldWeapon;

            if (weapon.weaponAmmoType == ammoType)
            {
                
                int neededAmmo = weapon.maxAmmo - weapon.currentAmmo;

                if (neededAmmo > 0)
                {
                    
                    int ammoToLoad = Mathf.Min(ammoAmount, neededAmmo);
                    weapon.currentAmmo += ammoToLoad;
                    ammoAmount -= ammoToLoad;
                }

                
                if (ammoAmount > 0)
                {
                    weapon.totalAmmo += ammoAmount;
                }

                
                player.UpdateAmmoUI();

                Destroy(gameObject);

                Debug.Log("Munición recogida: cargador " + weapon.currentAmmo + "/" + weapon.maxAmmo +
                          " | reserva " + weapon.totalAmmo);
            }
        }
    }
}
