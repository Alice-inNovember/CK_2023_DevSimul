using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private int bullet_cnt = 500;
    [SerializeField] private List<GameObject> _bulletObjects;

    // Start is called before the first frame update
    void Start()
    {
        _bulletObjects = new List<GameObject>();
        for (int i = 0; i < bullet_cnt; i++)
        {
            var newBullet = Instantiate(bullet, this.transform, true);
            _bulletObjects.Add(newBullet);
            newBullet.GetComponent<Bullet>().SetPoolManager(this);
            newBullet.SetActive(false);
        }
    }
    public GameObject BulletFind()
    {
        if (_bulletObjects.Count == 0)
        {
            var newBullet = Instantiate(bullet, this.transform, true);
            newBullet.GetComponent<Bullet>().SetPoolManager(this);
            return newBullet;
        }
        GameObject returnObj = _bulletObjects[0];
        _bulletObjects.Remove(returnObj);
        return returnObj;
    }
    public void AddBullet2List(GameObject obj)
    {
        obj.SetActive(false);
        _bulletObjects.Add(obj);
    }
}
