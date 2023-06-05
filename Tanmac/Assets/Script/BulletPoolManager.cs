using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    //총알 프리펩
    [SerializeField] private int bullet_cnt = 100;
    //미리 만들어 놓을 총알
    [SerializeField] private List<GameObject> _bulletObjects;
    //오브젝트 풀 (리스트)

    // Start is called before the first frame update
    void Start()
    {
        _bulletObjects = new List<GameObject>();
        //리스트 생성
        for (int i = 0; i < bullet_cnt; i++)
        {
            var newBullet = Instantiate(bullet, this.transform, true);
            //인스턴스화 (총알 생성)
            _bulletObjects.Add(newBullet);
            //오브젝트풀에 추가
            newBullet.GetComponent<Bullet>().SetPoolManager(this);
            //총알에 돌아올 풀(자신)을 전달
            newBullet.SetActive(false);
            //총알 비활성화
        }
        //처음 생성할 수많큼 총알 생성
    }
    //시작시 초기화
    
    public GameObject BulletFind()
    {
        if (_bulletObjects.Count == 0)
        {
            var newBullet = Instantiate(bullet, this.transform, true);
            //인스턴스화 (총알 생성)
            newBullet.GetComponent<Bullet>().SetPoolManager(this);
            //총알에 돌아올 풀(자신)을 전달
            return newBullet;
        }
        // 풀 내부에 남아있는 총알이 없을 시 새로 생성 및 반환
        GameObject returnObj = _bulletObjects[0];
        //남아있는 총알중 첫 총알을 가져옴
        _bulletObjects.Remove(returnObj);
        //오브젝트 풀에서 제거
        return returnObj;
        //반환
    }
    //대기중인 총알을 반환
    
    public void AddBullet2List(GameObject obj)
    {
        obj.SetActive(false);
        //총알 비활성화
        _bulletObjects.Add(obj);
        //오브젝트 풀에 다시 추가
    }
    //총알의 수명이 다하거나 충돌 시 오브젝트 풀로 돌아가기
}
