using UnityEngine;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    // Movement
    public float m_baseThrust = 5f;
    public float m_forwardThrustSpeed = 2.5f;
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
    [SerializeField] private bool p_usesVertCtrl;

    void Start()
    {
        p_fireMode = 0;
        p_firingCooldown = 0.250f;
        p_canFire = true;
        p_usesVertCtrl = false;
        p_navSource.resource = p_sfx[10];
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && p_canFire)
        {
            FireState();
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            p_usesVertCtrl = true;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            LateralThrust();
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

        // Triple forward speed for as long as 'Space' is held down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_forwardThrustSpeed *= 3f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_forwardThrustSpeed /= 3f;
        }
    }

    private void FixedUpdate()
    {
        Thrust(p_usesVertCtrl);    // Keep this function in 'FixedUpdate()' due to Unity not reading key presses while busy with 'ForwardThrust()'!
        CheckCooldown();
    }

    private void Thrust(bool vertCtrlActive)
    {
        // TODO: Either make the world move to save maths for enemy/projectile movement OR sanitise this to run cleaner
        if (!vertCtrlActive)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + m_baseThrust * Time.deltaTime);
        }
        if (vertCtrlActive)
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + (m_baseThrust + m_forwardThrustSpeed) * Input.GetAxisRaw("Vertical") * Time.deltaTime);
        }
    }

    private void LateralThrust()
    {
        float p_lateralImpulse = m_lateralThrustSpeed * Input.GetAxis("Horizontal");
        this.transform.position = new Vector2(this.transform.position.x + p_lateralImpulse * Time.deltaTime, this.transform.position.y);
    }

    private void FireState()
    {
        /* TODO:
        *   - Spawn according particles
        */
        switch (p_fireMode)
        {
            case 0: // Laser Slugs
                {
                    Fire();
                    break;
                }
            case 1: // Missiles
                {
                    Fire();
                    break;
                }
            case 2: // Laser Beam
                {
                    Fire();
                    break;
                }
        }
    }

    private void CheckCooldown()
    {
        if (Time.time >= p_timeNextFire)
        {
            p_canFire = true;
        }
    }

    private void Fire()
    {
        Instantiate(p_ammoTypes[p_fireMode], p_firePoints[0].position, Quaternion.identity);
        Instantiate(p_ammoTypes[p_fireMode], p_firePoints[1].position, Quaternion.identity);
        p_sfxSource.Play();
        p_canFire = false;
        p_timeLastFire = Time.time;
        p_timeNextFire = p_timeLastFire + p_firingCooldown;
    }
}
