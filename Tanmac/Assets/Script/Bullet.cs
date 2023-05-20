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
    private BulletPoolManager _bulletPoolManager;
    private float _speed = 0;
    private float _dietime = 10f;
    private Coroutine _distroyer;

    public void SetBullet(float bulletSpeed, float bulletdietime)
    {
        _speed = bulletSpeed;
        _dietime = bulletdietime;
    }

    public void SetPoolManager(BulletPoolManager bulletPoolManager)
    { 
        _bulletPoolManager = bulletPoolManager;
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
            _bulletPoolManager.AddBullet2List(this.gameObject);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator DieOnTime()
    {
        yield return new WaitForSeconds(_dietime);
        _bulletPoolManager.AddBullet2List(this.gameObject);
    }
}
