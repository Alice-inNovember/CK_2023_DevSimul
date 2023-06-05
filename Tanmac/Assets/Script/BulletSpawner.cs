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
        //난이도 변수 (발사되는 총알 수)
        [SerializeField] private float spawnDelay;
        //발사 간격
        [SerializeField] private float BulletSpeed;
        //총알 속도
        [SerializeField] private float BulletDieTime;
        //총알 수명
        [SerializeField] private GameObject BulletPoolObj;
        //총알들의 부모 오브젝트가 될 오브젝트
        private BulletPoolManager _bulletPoolManager;
        //오브젝트 풀을 관리하는 Class
        private WaitForSeconds _wait;
        //반복하여 발사시 부하를 줄이기 위한 코루틴 시간
        private float _gametime;
        //게임 진행 시간

        void Start()
        {
            _gametime = 0;
            //시간 초기화
            _bulletPoolManager = BulletPoolObj.GetComponent<BulletPoolManager>();
            //BulletPoolManager 클레스 가져오기
            _wait = new WaitForSeconds(spawnDelay);
            //매번 new 를 쓰면서 낭비되는 메모리 절약을 위해 한번만 생성
            StartCoroutine(SpawnBullet());
            //반복생성 시작
        }
        
        IEnumerator SpawnBullet()
        {
            while (true)
            {
                if (Dificalty <= 0)
                    Dificalty = 30;
                //난이도가 0이하가 될 수 없도록 설정
                if (Dificalty > 30)
                    Dificalty = 1;
                //난이도가 30을 초과할 수 없도록 설정
                for (float i = 0; i <= 180; i += 180 / (float)Dificalty)
                {
                    setBullet(i);
                    //총알 발사
                }
                //총알을 난이도 개수만큼 방향으로 나누어서 발사
                BulletSpeed += 0.01f;
                yield return _wait;
            }
        }
        //총알 발사하는 코루틴
        
        private void setBullet(float angle)
        {
            GameObject spawnObject = _bulletPoolManager.BulletFind();
            //오브젝트 풀 내부에서 한 총알을 받아오기
            spawnObject.SetActive(true);
            //활성화
            spawnObject.GetComponent<Bullet>().SetBullet(BulletSpeed, BulletDieTime);
            //총알 초기화
            spawnObject.transform.position = this.transform.position;
            //위치 초기화
            spawnObject.transform.rotation = this.transform.rotation;
            //회전 초기화
            spawnObject.transform.Rotate(Vector3.forward * angle);
            //인자값으로 넘어온 만큼 회전
            spawnObject.GetComponent<Bullet>().TimeLimit();
            //수명 활성화
        }

        public void AddDificalty(int deficalty)
        {
            Dificalty += deficalty;
        }
        //난이도 추가 (UI버튼 등)
        
        private void Update()
        {
            _gametime += Time.deltaTime;
            if (_gametime >= 30)
            {
                _gametime = 0;
                Dificalty += 1;
            }
        }
    }
}
