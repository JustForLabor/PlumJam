using System.Collections;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [Header("Rain Settings")]
    [SerializeField] private ParticleSystem lightRainParticle;
    [SerializeField] private ParticleSystem heavyRainParticle;
    [SerializeField] private float heavyRainDuration;            //폭우 시간
    [SerializeField] private float heavyRainInterval;            //폭우 간격

    private void Awake()
    {
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
    }

    private void HeavyRain()
    {
        lightRainParticle.Stop();
        heavyRainParticle.Play();
    }
}
