using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Player g_player;
    [SerializeField] private float p_velocity = 5f;
    [SerializeField] private float p_lifetime = 5f;
    [SerializeField] private int p_damage;

    void Start()
    {
        g_player = GameObject.FindFirstObjectByType<Player>();
        p_velocity += g_player.m_forwardThrustSpeed;
    }

    void FixedUpdate()
    {
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + p_velocity * Time.deltaTime);
        Destroy(this.gameObject, p_lifetime);
    }
}
