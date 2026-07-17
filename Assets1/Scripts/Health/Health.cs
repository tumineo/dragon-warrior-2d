using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // Player hurt
            anim.SetTrigger("hurt");
            if (SoundManager.instance != null && hurtSound != null)
                SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            // Player dead
            if (!dead)
            {
                anim.SetTrigger("die");

                if (SoundManager.instance != null && deathSound != null)
                    SoundManager.instance.PlaySound(deathSound);

                GetComponent<PlayerMovement>().enabled = false;
                dead = true;

                // Show Game Over screen
                if (GameManager.instance != null)
                    GameManager.instance.GameOver();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}