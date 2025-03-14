using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private AudioSource p_sfxSource;
    [SerializeField] private float p_velocity = 5f;
    [SerializeField] private float p_lifetime = 5f;
    [SerializeField] private int p_damage;
    [SerializeField] private string p_damageType;
    private Enemy p_hitEnemy;

    void FixedUpdate()
    {
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + p_velocity * Time.deltaTime);
        Destroy(this.gameObject, p_lifetime);
    }

    private bool CheckShield(Enemy enemy)
    {
        if (enemy.p_hasShield)
        {
            if (enemy.p_currentShield >= p_damage)
            {
                return true;
            }
            else
            {
                enemy.p_currentShield = 0;
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private bool CheckWeakness(Enemy enemy)
    {
        if (enemy.p_weakness == p_damageType)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ApplyDamage(Enemy enemy)
    {
        float appliedDamage;

        if (!CheckShield(enemy))
        { 
            if (!CheckWeakness(enemy))
            {
                appliedDamage = p_damage;
                enemy.p_currentHP -= (int) appliedDamage;
            }
            else
            {
                appliedDamage = p_damage * 1.15f;
                enemy.p_currentHP -= Mathf.RoundToInt(appliedDamage);
            }
        }
        else if (CheckShield(enemy))
        {
            appliedDamage = p_damage;
            enemy.p_currentShield -= (int) appliedDamage;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            p_hitEnemy = collision.gameObject.GetComponent<Enemy>();
            p_sfxSource.Play();

            if (p_hitEnemy != null)
            {
                ApplyDamage(p_hitEnemy);
            }
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            Destroy(gameObject.GetComponent<CapsuleCollider2D>());
        }
    }
}