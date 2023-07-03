using UnityEngine;

public class DestroyAfterDead : MonoBehaviour
{
    [SerializeField] private Health Health;
    public float CoolDown = 3f;
    public float coolDownTimer;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Health.currentHealth == 0)
        {
            if (CoolDown > 0)
            {
                CoolDown -= Time.deltaTime;
            }
            else
            {
                GameObject.Destroy(this.gameObject);
            }

        }
    }
}
