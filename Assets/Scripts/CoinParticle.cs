using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinParticle : MonoBehaviour
{
    private float speed = 5f;
    private Vector2 target;
    private Vector2 position;
    private Camera cam;

    void Start()
    {
        target = UIManager.instance.coinPos;
        position = gameObject.transform.position;

        cam = Camera.main;
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }
}
