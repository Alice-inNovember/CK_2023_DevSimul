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
        public GameObject bullet;
        public GameObject BulletPool;
        public int bullet_cnt = 100;
        
        [SerializeField] private int Dificalty = 6;
        [SerializeField] private List<GameObject> _bulletObjects;
        [SerializeField] private float spawnDelay;
        [SerializeField] private float BulletSpeed;
        [SerializeField] private float BulletDieTime;
        private WaitForSeconds _wait;
        void Start()
        {
            _bulletObjects = new List<GameObject>();
            _wait = new WaitForSeconds(spawnDelay);
            for (int i = 0; i < bullet_cnt; i++)
            {
                var newBullet = Instantiate(bullet, BulletPool.transform, true);
                _bulletObjects.Add(newBullet);
                newBullet.GetComponent<Bullet>().SetBullet(this, BulletSpeed, BulletDieTime);
                newBullet.SetActive(false);
            }
            StartCoroutine(SpawnBullet());
        }

        public void AddBullet2List(GameObject obj)
        {
            obj.SetActive(false);
            _bulletObjects.Add(obj);
        }
        private GameObject BulletFind()
        {
            if (_bulletObjects.Count == 0)
            {
                var newBullet = Instantiate(bullet, BulletPool.transform, true);
                newBullet.GetComponent<Bullet>().SetBullet(this, BulletSpeed, BulletDieTime);
                return newBullet;
            }
            GameObject returnObj = _bulletObjects[0];
            _bulletObjects.Remove(returnObj);
            return returnObj;
        }

        private void setBullet(float angle)
        {
            GameObject spawnObject = BulletFind();
            spawnObject.SetActive(true);
            spawnObject.transform.position = this.transform.position;
            spawnObject.transform.rotation = this.transform.rotation;
            spawnObject.transform.Rotate(Vector3.forward * angle);
            spawnObject.GetComponent<Bullet>().TimeLimit();
        }
        
        IEnumerator SpawnBullet()
        {
            while (true)
            {
                for (int i = 0; i <= 180; i += 180 / Dificalty)
                {
                    setBullet(i);
                }
                yield return _wait;
            }
        }
    }
}
