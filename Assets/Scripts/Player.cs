using UnityEngine;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    // Movement
    [SerializeField] private Rigidbody2D playerRigidbody;
    public float m_forwardThrustSpeed = 5f;
    public float m_lateralThrustSpeed = 5f;

    // Auxiliary
    [SerializeField] private GameObject[] p_ammoTypes;
    [SerializeField] private Transform[] p_firePoints;
    [SerializeField] private AudioSource p_sfxSource;
    [SerializeField] private AudioSource p_navSource;
    [SerializeField] private AudioResource[] p_sfx;

    // Management
    [SerializeField] private int p_fireMode = 0;
    [SerializeField] private float p_firingCooldown;
    [SerializeField] private float p_timeLastFire;
    [SerializeField] private float p_timeNextFire;
    [SerializeField] private bool p_canFire;

    private void Awake()
    {
        p_fireMode = 0;
        p_sfxSource.resource = p_sfx[0];
        p_firingCooldown = 0.250f;
        p_canFire = true;
        p_navSource.resource = p_sfx[10];
    }

    void Start()
    {
        p_fireMode = 0;
        p_sfxSource.resource = p_sfx[0];
        p_firingCooldown = 0.250f;
        p_canFire = true;
        p_navSource.resource = p_sfx[10];
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && p_canFire)
        {
            Fire(p_fireMode);
        }

        // Apply 'fireMode' value to access different ammo types from 'ammoTypes' array via index
        if (Input.GetKeyDown(KeyCode.Alpha1) && p_fireMode != 0)
        {
            p_fireMode = 0;
            p_navSource.Play();
            p_sfxSource.resource = p_sfx[p_fireMode];
            p_firingCooldown = 0.250f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && p_fireMode != 1)
        {
            p_fireMode = 1;
            p_navSource.Play();
            p_sfxSource.resource = p_sfx[p_fireMode];
            p_firingCooldown = 0.750f;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            Thrust();
        }
        CheckCooldown();
    }

    private void Thrust()
    {
        float latSpeed = m_lateralThrustSpeed * Input.GetAxis("Horizontal");
        float vertSpeed = m_forwardThrustSpeed * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            vertSpeed *= 1.5f;
            playerRigidbody.MovePosition(new Vector2(playerRigidbody.position.x + latSpeed * Time.fixedDeltaTime, playerRigidbody.position.y + vertSpeed * Time.fixedDeltaTime));

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            vertSpeed /= 1.5f;
            playerRigidbody.MovePosition(new Vector2(playerRigidbody.position.x + latSpeed * Time.fixedDeltaTime, playerRigidbody.position.y + vertSpeed * Time.fixedDeltaTime));
        }
        else
        {
            playerRigidbody.MovePosition(new Vector2(playerRigidbody.position.x + latSpeed * Time.fixedDeltaTime, playerRigidbody.position.y + vertSpeed * Time.fixedDeltaTime));
        }

    }

    private void CheckCooldown()
    {
        if (Time.time >= p_timeNextFire)
        {
            p_canFire = true;
        }
    }

    private void Fire(int fireMode)
    {
        Instantiate(p_ammoTypes[fireMode], p_firePoints[0].position, Quaternion.identity);
        Instantiate(p_ammoTypes[fireMode], p_firePoints[1].position, Quaternion.identity);
        p_sfxSource.Play();
        p_canFire = false;
        p_timeLastFire = Time.time;
        p_timeNextFire = p_timeLastFire + p_firingCooldown;
    }
}