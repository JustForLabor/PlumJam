using UnityEngine;

public class Flood : MonoBehaviour
{
    [SerializeField] private float lightRainFloodSpeed; //약한 비일 때 수위 상승 속도
    [SerializeField] private float heavyRainFloodSpeed; //폭우일 때 수위 상승 속도

    private void Update()
    {
        RaiseFlood();
    }

    //수위 상승
    private void RaiseFlood()
    {
        switch (WeatherManager.instance.weatherType)
        {
            case WeatherType.LightRain:
                {
                    transform.position += Vector3.up * lightRainFloodSpeed * Time.deltaTime;
                    break;
                }
            case WeatherType.HeavyRain:
                {
                    transform.position += Vector3.up * heavyRainFloodSpeed * Time.deltaTime;
                    break;
                }
            default:
                {
                    break;
                }

        }
    }
}