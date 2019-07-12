using UnityEngine;
using System.Collections;
using Advent.Player;

public class Enemy : MonoBehaviour,IDamageable
{
    public int Health = 25;
    public float knockBackDistance = 2f;
    public float stunTime = 1f;

    public SpriteRenderer spriteRenderer;

    private Vector2 targetDirection;

    private Rigidbody2D rb2d;
    private Animator anim;

    [SerializeField]
    private CircleCollider2D myCollider = null;
    [SerializeField]
    private LayerMask blockingLayer = 0;
    private RaycastHit2D hit;

    Shader hitShader;
    Shader defaultShader;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        hitShader = Shader.Find("GUI/Text Shader");
        defaultShader = Shader.Find("Sprites/Default");
    }

    // Update is called once per frame
    void Update()
    {
        targetDirection = PlayerControlsScript.instance.gameObject.transform.position - transform.position;
        hit = Physics2D.CircleCast(transform.TransformPoint(myCollider.offset), myCollider.radius, transform.position, myCollider.radius, blockingLayer);
    }
    private void FixedUpdate()
    {
        if(hit.collider != null)
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    public void DamageEffect()
    {
        spriteRenderer.material.shader = hitShader;
        spriteRenderer.material.color = Color.white;

        FindObjectOfType<HitStop>().Stop(0.1f);
    }

    public void TakeDamage(float damage)
    {
        Health -= (int)damage;
        DamageEffect();
        StartCoroutine(WaitForSpawn());

        if(Health <= 0)
        {
            StartCoroutine(DeathRoutine());
        } 
    }
    IEnumerator WaitForSpawn()
    {
        while (Time.timeScale != 1.0f)
        {
            yield return null;//wait for hit stop to end
        }
        if(hit.collider == null)
        {
            rb2d.velocity = new Vector2(targetDirection.x * -knockBackDistance, targetDirection.y * -knockBackDistance); //knockback
        }
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.material.shader = defaultShader;
        rb2d.velocity = Vector2.zero;
    }
    IEnumerator DeathRoutine()
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    public SpriteRenderer GetSpriteRenderer
    {
        get
        {
            return spriteRenderer;
        }
    }
}
