using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private int Dificalty = 6;
        [SerializeField] private float spawnDelay;
        [SerializeField] private float BulletSpeed;
        [SerializeField] private float BulletDieTime;
        [SerializeField] private GameObject BulletPoolObj;
        private BulletPoolManager _bulletPoolManager;
        private WaitForSeconds _wait;
        private float _gametime;

        void Start()
        {
            _gametime = 0;
            _bulletPoolManager = BulletPoolObj.GetComponent<BulletPoolManager>();
            _wait = new WaitForSeconds(spawnDelay);
            StartCoroutine(SpawnBullet());
        }

        private void setBullet(float angle)
        {
            GameObject spawnObject = _bulletPoolManager.BulletFind();
            spawnObject.SetActive(true);
            spawnObject.GetComponent<Bullet>().SetBullet(BulletSpeed, BulletDieTime);
            spawnObject.transform.position = this.transform.position;
            spawnObject.transform.rotation = this.transform.rotation;
            spawnObject.transform.Rotate(Vector3.forward * angle);
            spawnObject.GetComponent<Bullet>().TimeLimit();
        }

        public void AddDificalty(int deficalty)
        {
            Dificalty += deficalty;
        }
        private void Update()
        {
            _gametime += Time.deltaTime;
            if (_gametime >= 60)
            {
                _gametime = 0;
                Dificalty += 1;
            }
        }
        IEnumerator SpawnBullet()
        {
            while (true)
            {
                if (Dificalty <= 0)
                    Dificalty = 60;
                for (float i = 0; i <= 180; i += 180 / (float)Dificalty)
                {
                    setBullet(i);
                }
                BulletSpeed += 0.01f;
                yield return _wait;
            }
        }
    }
}
