using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float speed = 5;

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }
}
