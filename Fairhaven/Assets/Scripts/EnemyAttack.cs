using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float globalCoolDown;
    public float timeDelay;

    public Transform position;
    public LayerMask enemies;
    public float range;
    public int dmg;
    public GameObject player;
    public GameObject enemy;
          
            
        void Update()
        {
            if (globalCoolDown <= 0)
            {
            float dist = Vector3.Distance(player.transform.position, enemy.transform.position);
           
            if (dist < range)

            {
                Debug.Log("damaged");
                Collider2D collision = Physics2D.OverlapCircle(position.position, range, enemies);
                collision.GetComponent<Player>().TakeDamage(dmg);
                Player.playerHP -= dmg;
                //Debug.Log(Player.playerHP);

            }
                globalCoolDown = timeDelay;
            }
            else
            {
                globalCoolDown -= Time.deltaTime;
            }


        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position.position, range);
        }
        }
