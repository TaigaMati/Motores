using UnityEngine;
using FMODUnity;

public class EnemyGiant : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int maxHealth = 50;
    private int currentHealth;
    public bool isDead = false;

    [Header("Ataque")]
    [SerializeField] private int danioAtaque = 20;
    [SerializeField] private float rangoAtaque = 2.0f;
    [SerializeField] private string tagJugador = "Player";
    [SerializeField] private Transform puntoAtaque;

    [Header("Persecución")]
    public float visionRange = 10f;
    public float visionAngle = 90f;
    public float speed = 3.5f;

    [Header("Referencias")]
    public Transform player;
    public Animator animator;
    public Rigidbody rb;

    public StudioEventEmitter ZombieHurt;
    public StudioEventEmitter ZombieRun;
    public StudioEventEmitter ZombieAttack;
    public StudioEventEmitter ZombieDed;

    private bool isChasing = false;
    private bool isAttacking = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (puntoAtaque == null) puntoAtaque = transform;
    }

    private void Update()
    {
        if (isDead) return;
        DetectPlayer();
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        HandleEnemyState();
    }

    // 👉 El gigante recibe daño (balas, golpes, etc.)
    public void RecibirDanio(int cantidad)
    {
        if (isDead) return;
        ZombieHurt.Play();

        currentHealth -= cantidad;
        Debug.Log($"⚔️ {gameObject.name} recibió {cantidad} de daño. Vida restante: {currentHealth}");

        if (currentHealth <= 0) Die();
    }

    // 👉 El gigante ataca al jugador (se llama desde animación)
    public void EventoGolpeEnemigo()
    {
        ZombieAttack.Play();
        Collider[] objetosImpactados = Physics.OverlapSphere(puntoAtaque.position, rangoAtaque);

        foreach (Collider hit in objetosImpactados)
        {
            if (hit.CompareTag(tagJugador))
            {
                PlayerDaño vidaJugador = hit.GetComponent<PlayerDaño>();
                if (vidaJugador != null)
                {
                    vidaJugador.RecibirDanio(danioAtaque);
                    Debug.Log($"💥 Enemigo golpeó al Player. Daño: {danioAtaque}");
                    break;
                }
            }
        }
    }

    private void DetectPlayer()
    {
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (distance < visionRange)
        {
            Vector3 directionFlat = new Vector3(direction.x, 0, direction.z);
            if (Vector3.Angle(transform.forward, directionFlat) < visionAngle / 2f)
            {
                isChasing = true;
                return;
            }
        }

        if (distance > visionRange * 1.2f)
        {
            isChasing = false;
            isAttacking = false;
        }
    }

    private void HandleEnemyState()
    {
        if (!isChasing)
        {
            SetAnimationStates(false, false);
            rb.linearVelocity = Vector3.zero;
            if (ZombieRun != null && ZombieRun.IsPlaying())
                ZombieRun.Stop();
            return;
        }

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance > rangoAtaque)
        {
            if (ZombieRun != null && !ZombieRun.IsPlaying())
            {
                ZombieRun.Play();
            }
            // Persecución
            isAttacking = false;
            Vector3 dir = (player.position - transform.position).normalized;
            dir.y = 0;

            rb.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5f * Time.fixedDeltaTime));
            rb.MovePosition(transform.position + dir * speed * Time.fixedDeltaTime);

            SetAnimationStates(true, false);
        }
        else
        {
            // Ataque
            rb.linearVelocity = Vector3.zero;
            if (!isAttacking)
            {
                isAttacking = true;
                SetAnimationStates(true, true);
            }
        }
    }

    public void Die()
{
    if (isDead) return;

    isDead = true;

    if (ZombieRun != null)
        ZombieRun.Stop();

    if (ZombieDed != null)
        ZombieDed.Play();
    animator.SetBool("isChasing", false);
    animator.SetBool("isAttacking", false);
    animator.SetBool("isDead", true);

    rb.linearVelocity = Vector3.zero;
    Debug.Log($"☠️ {gameObject.name} murió");

    
    gameObject.SetActive(false);
}

    private void SetAnimationStates(bool chasing, bool attacking)
    {
        animator.SetBool("isChasing", chasing);
        animator.SetBool("isAttacking", attacking);
    }

    private void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
    }
}
