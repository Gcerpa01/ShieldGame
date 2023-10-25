using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] private float reflectSpeed;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private int hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = 0;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit == 1 || hit == 2) return;

        float movementSpeed;

        movementSpeed = speed * Time.deltaTime;

        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        hit = base.OnTriggerEnter2D(collision);
        
        if(hit == 0){
            speed *= -reflectSpeed;
            return;
        }

        coll.enabled = false;
        
        if(anim != null) anim.SetTrigger("boom");
        else gameObject.SetActive(false); 
        
    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false);
        if(hit == 2) speed = speed / -reflectSpeed;
    }

}