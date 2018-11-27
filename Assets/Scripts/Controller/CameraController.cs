using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject _Target;
    public Vector3 nextPosition;

    [Range(1, 10)]
    public float _Distance, _Height;

    private Coroutine moveCoroutine;

    private void FixedUpdate()
    {
        //プレイヤーの後ろ向きのベクトル
        var v = _Target.transform.forward*-1;
        v = v + new Vector3(0, _Height, _Distance);
        nextPosition = _Target.transform.position + v;
        if (moveCoroutine != null) moveCoroutine = StartCoroutine(move());
    }

    IEnumerator move()
    {
        var value = 0.0f;
        while(value < 1.0f)
        {
            transform.position = Vector3.Lerp(transform.position, nextPosition, value);
            value += 0.1f;
            yield return null;
        }

        moveCoroutine = null;
    }
}
