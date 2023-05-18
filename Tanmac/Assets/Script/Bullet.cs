using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.WSA;

public class Bullet : MonoBehaviour
{
    private BulletSpawner _bulletSpawner;
    private float _speed;
    private float _dietime = 5f;
    private Coroutine _distroyer;

    public void SetBullet(BulletSpawner spawner, float bulletSpeed, float bulletdietime)
    {
        _bulletSpawner = spawner;
        _speed = bulletSpeed;
        _dietime = bulletdietime;
    }

    public void TimeLimit()
    {
        _distroyer = StartCoroutine(DieOnTime());
    }

    void Update()
    {
        transform.Translate(transform.right * (_speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Wall") || col.CompareTag("Player"))
        {
            StopCoroutine(_distroyer);
            _bulletSpawner.AddBullet2List(this.gameObject);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator DieOnTime()
    {
        yield return new WaitForSeconds(_dietime);
        _bulletSpawner.AddBullet2List(this.gameObject);
    }
}
