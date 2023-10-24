using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBoyLogic : MonoBehaviour
{
    [Header("Attack Stats")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    
    // [Header("Bullet Attack")]
    // [SerializeField] private Transform firepoint;
    // [SerializeField] private GameObject[] bullets;

    [Header("Collider Info")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldown_timer = Mathf.Infinity;


    private Animator anim;

    private bool hasAttacked = false;
    private bool charge_done = true;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }



    void Update()
    {

        // cooldown_timer += Time.deltaTime;
        //Transition from idle to attack
        if(PlayerNearby() && !hasAttacked){ //first shot from enemy
            anim.SetTrigger("playerInSight"); 
            anim.SetBool("firstShot",true);
            hasAttacked = true;
            charge_done = false;
        }

        else if(hasAttacked){
            anim.SetBool("firstShot",false);
            
            cooldown_timer += Time.deltaTime;
            if(cooldown_timer >= attackCooldown){
                cooldown_timer = 0;
                charge_done = true;
            }

            //Transition from Attack to recharge
            if(PlayerNearby() && !charge_done){
                anim.SetTrigger("playerInSight"); 
                anim.SetBool("charged",false);
            }
            //Transition from recharge to attack
            else if(PlayerNearby() && charge_done){
                anim.SetTrigger("playerInSight"); 
                anim.SetBool("charged",true);
                charge_done = false;
            }
            //Transition from recharge to idle
            else if(!PlayerNearby()){
                anim.SetTrigger("playerOutOfSight");
                hasAttacked = false;
                charge_done = true;
            }
        }

    }


    private bool PlayerNearby(){
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }



    // private void bulletAttack(){
    //     cooldown_timer = 0;
    //     bullets[FindBullet()].transform.position = firepoint.position;
    //     bullets[FindBullet()].GetComponent<EnemyProjectile>().ActivateProjectile();
    // }

    // private int FindBullet(){
    //     for(int i = 0; i < bullets.Length; i++){
    //         if(!bullets[i].activeInHierarchy) return i;
    //     }
    //     return 0;
    // }
    
}
