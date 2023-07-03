using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider healthslider;
    //farbänderung
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(float maxHealth)
    {
        healthslider.maxValue = maxHealth;
        healthslider.value = maxHealth;

        //farbänderung
        fill.color = gradient.Evaluate(1f);

    }
    public void SetHealth(float health)
    {
        healthslider.value = health;
        fill.color = gradient.Evaluate(healthslider.normalizedValue);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
