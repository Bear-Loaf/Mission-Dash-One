using UnityEngine;

public class Speedometer : MonoBehaviour
{
    private float p_firstTimeStamp;
    private float p_secondTimeStamp;
    private float p_timeDelta;
    private Vector2 p_firstPos;
    private Vector2 p_secondPos;
    private Vector2 p_posDelta;
    public float m_speed;

    private float manageStamp1;
    [SerializeField] float intervalSeconds;
    private float manageStamp2;

    void Update()
    {
        if (manageStamp1! < manageStamp2)
        {
            SetStampSet1();
        }

        if (Time.time >= manageStamp2)
        {
            SetStampSet2();
        }
        m_speed = p_posDelta.y / p_timeDelta;
    }

    void SetStampSet1()
    {
        p_firstTimeStamp = Time.time;
        p_firstPos = this.transform.position;
        manageStamp1 = p_firstTimeStamp;
        manageStamp2 = manageStamp1 + intervalSeconds; ;
    }

    void SetStampSet2()
    {
        p_secondPos = this.transform.position;
        p_secondTimeStamp = Time.time;
        p_timeDelta = p_secondTimeStamp - p_firstTimeStamp;
        p_posDelta = p_secondPos - p_firstPos;
    }
}
