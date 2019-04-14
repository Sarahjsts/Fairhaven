using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float globalCoolDown;
    public float timeDelay;

    public Transform position;
    public LayerMask enemies;
    public float range;
    public int dmg = Player.Att;

    // Update is called once per frame
    void Update()
    {
        if(globalCoolDown <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Collider2D[] dmgEnemies = Physics2D.OverlapCircleAll(position.position, range, enemies);
                for(int i = 0; i < dmgEnemies.Length; i++)
                {
                    dmgEnemies[i].GetComponent<Enemy>().TakeDamage(dmg);
                    Debug.Log(dmgEnemies[i].GetComponent<Enemy>().health);
                }
            }
            globalCoolDown = timeDelay;
        }  else
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
