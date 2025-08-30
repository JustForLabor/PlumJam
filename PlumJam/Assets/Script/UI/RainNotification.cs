using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class RainNotification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI timer;

    private void Start()
    {
        WeatherManager.instance.OnWeatherChanged += WeatherManager_OnWeatherChanged;
    }

    private void WeatherManager_OnWeatherChanged(object sender, EventArgs e)
    {
        Debug.Log("WeatherManager_OnWeatherChanged");
        RefreshUI();
    }

    private void RefreshUI()
    {
        switch (WeatherManager.instance.weatherType)
        {
            case WeatherType.LightRain:
                title.text = "폭우까지 남은시간";
                StartCoroutine(StartTimer(WeatherManager.instance.heavyRainInterval));
                break;
            case WeatherType.HeavyRain:
                title.text = "가랑비까지 남은시간";
                StartCoroutine(StartTimer(WeatherManager.instance.heavyRainDuration));
                break;
            default:
                break;
        }
    }

    private IEnumerator StartTimer(int second)
    {
        int secs = second;

        while (true)
        {
            string min = (secs / 60).ToString();
            string sec = (secs % 60).ToString();

            //숫자가 한자리면 앞에 0 붙여서 2자리로 만들기
            if (min.Length == 1)
            {
                min = $"0{min}";
            }
            if (sec.Length == 1)
            {
                sec = $"0{sec}";
            }

            //타이머 표시
            timer.text = $"{min}:{sec}";

            //1초 대기
            yield return new WaitForSeconds(1f);
            secs--;

            //시간이 0이 되면 타이머 종료
            if (secs <= 0)
            {
                break;
            }
        }
    }
}
