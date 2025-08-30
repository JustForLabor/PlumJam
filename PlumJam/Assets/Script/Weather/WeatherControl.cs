using System.Collections;
using UnityEngine;

public enum WeatherType
{
    LightRain,
    HeavyRain,
}

public class WeatherManager : MonoBehaviour
{
    public static WeatherManager instance { get; private set; }

    [Header("Rain Settings")]
    [SerializeField] private ParticleSystem lightRainParticle;
    [SerializeField] private ParticleSystem heavyRainParticle;
    [SerializeField] private float heavyRainDuration;            //폭우 시간
    [SerializeField] private float heavyRainInterval;            //폭우 간격

    public WeatherType weatherType = WeatherType.LightRain; //현재 날씨

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        StartCoroutine(RainSequence());
    }

    public IEnumerator RainSequence()
    {
        while (true)
        {
            lightRain();
            yield return new WaitForSeconds(heavyRainDuration);
            HeavyRain();
            yield return new WaitForSeconds(heavyRainInterval);
        }
    }

    private void lightRain()
    {
        lightRainParticle.Play();
        heavyRainParticle.Stop();

        weatherType = WeatherType.LightRain;
    }

    private void HeavyRain()
    {
        lightRainParticle.Stop();
        heavyRainParticle.Play();

        weatherType = WeatherType.HeavyRain;
    }
}
