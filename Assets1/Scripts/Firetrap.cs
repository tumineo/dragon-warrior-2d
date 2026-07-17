using UnityEngine;
using System.Collections;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float activateDelay;
    [SerializeField] private float activeTime;

    private Animator anim;
    private SpriteRenderer spriteRend;
    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private bool triggered;
    private bool active;

    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
                StartCoroutine(ActivateFiretrap());

            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        //turn the sprite red to indicate it's about to activate
        triggered = true;
        spriteRend.color = Color.red;

        //wait for the delay, then activate the trap
        yield return new WaitForSeconds(activateDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        //wait for the active time, then deactivate the trap
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", true);
    }
}