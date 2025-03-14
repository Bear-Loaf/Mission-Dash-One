using UnityEngine;

public class AsteroidSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    [SerializeField] private int spinDirDeterminator;
    [SerializeField] private float spinDirection;
    [SerializeField] private float spin;

    void Start()
    {
        spinDirDeterminator = Random.Range(0, 101);
        if (spinDirDeterminator <= 50)
        {
            spinDirection = 1;
        }
        else
        {
            spinDirection = -1;
        }
        spinSpeed = Random.Range(1f, 5f);

        spin = spinSpeed * spinDirection * Time.deltaTime;
    }

    void Update()
    {
        this.transform.Rotate(0f, 0f, spin);
    }
}
