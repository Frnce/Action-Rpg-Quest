using UnityEngine;
using System.Collections;
using Advent.Player;

public class Enemy : MonoBehaviour,IDamageable
{
    public int Health = 25;
    public float knockBackDistance = 2f;
    public float stunTime = 1f;

    private Vector2 targetDirection;

    private Rigidbody2D rb2d;
    private Animator anim;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        targetDirection = PlayerControlsScript.instance.gameObject.transform.position - transform.position;
    }

    public void DamageEffect()
    {
        //KNockback
    }

    public void TakeDamage(float damage)
    {
        Health -= (int)damage;
        StartCoroutine(TakeDamageRoutine());
    }
    private IEnumerator TakeDamageRoutine()
    {
        rb2d.AddForce(new Vector2(targetDirection.x * -knockBackDistance, targetDirection.y * -knockBackDistance),ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.05f);
        rb2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.3f);
    }
}
