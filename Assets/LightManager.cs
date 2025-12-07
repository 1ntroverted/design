using UnityEngine;
using UnityEngine.UI;
public class LightManager : MonoBehaviour
{

    [SerializeField] private float battery;
    [SerializeField] GameObject light;
    [SerializeField] Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battery = 1;
    }

    // Update is called once per frame
    public void Light()
    {
        battery -= (Time.deltaTime * (1 / 60f));
        slider.value = battery;

        if (battery <= 0f)
        {
            battery = 0f;
            light.SetActive(false);
        }
        else
        {
            light.SetActive(true);
        }

    }

    public void LightOff()
    {
        light.SetActive(false);






    }

    public void LightOn()
    {
        light.SetActive(true);




    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Battery"))
        {
            battery += 1f;
            Destroy(collision.gameObject);
        }
    }
}
