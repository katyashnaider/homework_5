using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private Coroutine _coroutine;
    private float _maxVolume = 1f;
    private float _setStep = 0.1f;

    private void Awake()
    {
        _audioSource.volume = 0;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Player>(out Player player))
        {
            _coroutine = StartCoroutine(StartAlarm());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<Player>(out Player player))
        {
            StopAlarm();
        }
    }

    private IEnumerator StartAlarm()
    {
        float waitForSeconds = 1f;
        _audioSource.Play();

        while (_audioSource.volume <= _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _setStep);

            yield return new WaitForSeconds(waitForSeconds);
        }
    }

    private void StopAlarm()
    {
        StopCoroutine(_coroutine);

        _audioSource.Stop();
        _audioSource.volume = 0;
    }
}
