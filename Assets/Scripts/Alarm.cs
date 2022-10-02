using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AlarmControl _alarmControl;

    private Coroutine _coroutine;
    private float _maxVolume = 1f;
    private float _setStep = 0.1f;

    private void Awake()
    {
        _audioSource.volume = 0;
    }

    private void OnEnable()
    {
        _alarmControl.Entered += StartAlarm;
        _alarmControl.Leaving += StopAlarm;
    }

    private void OnDisable()
    {
        _alarmControl.Entered -= StartAlarm;
        _alarmControl.Leaving -= StopAlarm;
    }

    private void StartAlarm()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _audioSource.volume = 0;
        _maxVolume = 1;

        _coroutine = StartCoroutine(JobAlarm());
    }

    private void StopAlarm()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _maxVolume = 0;

        _coroutine = StartCoroutine(JobAlarm());
    }

    private IEnumerator JobAlarm()
    {
        float rateStep = 1f;
        _audioSource.Play();
        var waitForSeconds = new WaitForSeconds(rateStep);

        while (_audioSource.volume != _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _setStep);
            
            yield return waitForSeconds;
        }
    }

}
