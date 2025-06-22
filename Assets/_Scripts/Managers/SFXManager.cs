using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoSingleton<SFXManager>   
{
    [SerializeField] private AudioSource _bubblesSFXPrefab;
    [SerializeField] private float _bubblesPlayTime = 2f;

    private float _stopBubblesTimeLeft;
    private AudioSource _bubblesSFX;
    private Coroutine _bubblesSFXCoroutine;

    public void PlayBubbles()
    {
        if (_bubblesSFX == null)
        {
            _bubblesSFX = Instantiate(_bubblesSFXPrefab, transform);
        }

        if (_bubblesSFXCoroutine == null)
        {
            _bubblesSFXCoroutine = StartCoroutine(PlayBubblesCoroutine());
        }
        else
        {
            _stopBubblesTimeLeft = _bubblesPlayTime;
        }
    }

    private IEnumerator PlayBubblesCoroutine()
    {
        if (_stopBubblesTimeLeft <= 0f)
        {
            _bubblesSFX.Play();
            _stopBubblesTimeLeft = _bubblesPlayTime;
        }

        while (_stopBubblesTimeLeft > 0)
        {
            _stopBubblesTimeLeft -= Time.deltaTime;
            yield return null;
        }

        _bubblesSFX.Pause();
        _bubblesSFXCoroutine = null;
        yield return null;
    }
}
