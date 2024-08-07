using UnityEngine;
public class HealthPickup : MonoBehaviour
{
    public float bonusHealth = 5;

    public bool isEnabled = true;

    public SpriteRenderer sr;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthState>())
        {
            other.GetComponent<HealthState>().health += bonusHealth;

            isEnabled = false;
            sr.color = Color.red;
            Invoke("Respawn", 3.0f);
        }
    }

    void Respawn()
    {
        isEnabled = true;
        sr.color = Color.green;
    }

}
