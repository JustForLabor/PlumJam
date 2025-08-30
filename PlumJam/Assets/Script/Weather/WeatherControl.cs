using System;
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
    public event EventHandler OnWeatherChanged;


    [Header("Rain Settings")]
    [SerializeField] private ParticleSystem lightRainParticle;
    [SerializeField] private ParticleSystem heavyRainParticle;
    [SerializeField] private float initialWaitTime; 
    public int heavyRainDuration;                             //폭우 시간
    public int heavyRainInterval;                             //폭우 간격
    public WeatherType weatherType = WeatherType.LightRain;     //현재 날씨

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
    }

    private void Start()
    {
        StartCoroutine(RainSequence());
    }

    private void OnDestroy()
    {
        OnWeatherChanged = null;
    }

    public IEnumerator RainSequence()
    {
        yield return new WaitForSeconds(initialWaitTime);

        while (true)
        {
            lightRain();
            yield return new WaitForSeconds(heavyRainInterval);
            HeavyRain();
            yield return new WaitForSeconds(heavyRainDuration);
        }
    }

    private void lightRain()
    {
        lightRainParticle.Play();
        heavyRainParticle.Stop();

        weatherType = WeatherType.LightRain;
        OnWeatherChanged?.Invoke(this, EventArgs.Empty);

        SoundManager.instance.PlaySound(SoundType.LightRain);
    }

    private void HeavyRain()
    {
        lightRainParticle.Stop();
        heavyRainParticle.Play();

        weatherType = WeatherType.HeavyRain;
        OnWeatherChanged?.Invoke(this, EventArgs.Empty);

        SoundManager.instance.PlaySound(SoundType.HeavyRain);
    }
}
