using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float reachDistance = 3f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform holdPosition;

    private GameObject heldObject = null;
    private Shot heldWeapon = null;
    private Camera cam;

    private PlayerInputAction controls;

    [Header("UI")]
    public TextMeshProUGUI ammoText;

    public Shot HeldWeapon => heldWeapon;

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        controls = new PlayerInputAction();
    }

    void Start()
    {
        UpdateAmmoUI();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Player.Interactions.performed += OnInteract; 
        controls.Player.Shoot.performed += OnShoot;
        controls.Player.Reload.performed += OnReload;
        controls.Player.Drop.performed += OnDrop; 
    }

    void OnDisable()
    {
        controls.Player.Interactions.performed -= OnInteract;
        controls.Player.Shoot.performed -= OnShoot;
        controls.Player.Reload.performed -= OnReload;
        controls.Player.Drop.performed -= OnDrop;
        controls.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, reachDistance, interactableLayer))
        {
           
            if (heldObject == null)
            {
                if (hit.collider.CompareTag("Weapon") || hit.collider.CompareTag("Fuel"))
                {
                    TryPickUp(hit.collider.gameObject);
                }
            }
            else
            {
                
                if (hit.collider.CompareTag("Generator") && heldObject.CompareTag("Fuel"))
                {
                    Generador gen = hit.collider.GetComponent<Generador>();
                    if (gen != null)
                    {
                        gen.AddFuel(heldObject);
                        DropObject(); 
                    }
                }
            }
        }
        UpdateAmmoUI();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (heldWeapon != null)
        {
            heldWeapon.Shoot();
            UpdateAmmoUI();
        }
    }

    private void OnReload(InputAction.CallbackContext context)
    {
        if (heldWeapon != null)
        {
            heldWeapon.StartReload();
            UpdateAmmoUI();
        }
    }

    private void OnDrop(InputAction.CallbackContext context)
    {
        if (heldObject != null)
            DropObject();
    }

    void TryPickUp(GameObject target)
    {
        heldObject = target;
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        heldObject.transform.SetParent(holdPosition);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;

        heldWeapon = heldObject.GetComponent<Shot>();
        UpdateAmmoUI();
    }

    void DropObject()
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        heldObject.transform.SetParent(null);
        heldObject = null;
        heldWeapon = null;
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            if (heldWeapon != null)
                ammoText.text = heldWeapon.currentAmmo + " / " + heldWeapon.totalAmmo;
            else
                ammoText.text = "";
        }
    }
}
