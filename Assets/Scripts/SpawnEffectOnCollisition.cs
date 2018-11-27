using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffectOnCollisition : MonoBehaviour {
    public GameObject _Prefab;
    Coroutine isPlaying;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var isAttack = other.gameObject.GetComponent<PlayerController>().isAttack;
            if (isAttack && isPlaying == null)isPlaying = StartCoroutine(PlayEffect());
        }
    }

    IEnumerator PlayEffect()
    {
        var effect = Instantiate(_Prefab, transform);

        var startTime = Time.time;
        while (Time.time - startTime < 0.6f) { yield return null; }
        Destroy(effect.gameObject);
        isPlaying = null;
    }
}
