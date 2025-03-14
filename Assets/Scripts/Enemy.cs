using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int p_currentHP;
    [SerializeField] private int p_maxHP;
    public int p_currentShield;
    [SerializeField] private int p_maxShield;
    public bool p_hasShield;
    public string p_weakness;

    void Start()
    {
        p_currentHP = p_maxHP;
        if (!p_hasShield)
        {
            p_currentShield = 0;
            p_maxShield = 0;
        }
        else
        {
            p_currentShield = p_maxShield;
        }
    }

    void FixedUpdate()
    {
        if (p_currentShield <= 0)
        {
            p_currentShield = 0;
            p_hasShield = false;
        }
        if (p_currentHP <= 0)
        {
            p_currentHP = 0;
            Destroy(gameObject);
        }
    }
}
