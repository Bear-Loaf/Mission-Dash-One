using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject g_playerObject;
    [SerializeField] private float m_offsetY = 1.5f;

    void Update()
    {
        this.transform.position = new Vector3(0, g_playerObject.transform.position.y + m_offsetY, -5f);
    }
}
