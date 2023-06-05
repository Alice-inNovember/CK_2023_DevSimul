using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletPoolManager _bulletPoolManager;
    //오브젝트 풀
    private float _speed = 0;
    //속도
    private float _dietime = 10f;
    //수명
    private Coroutine _distroyer;
    //코루틴

    public void SetBullet(float bulletSpeed, float bulletdietime)
    {
        _speed = bulletSpeed;
        _dietime = bulletdietime;
    }
    //총알 초기화 (속도, 수명)
    
    void Update()
    {
        transform.Translate(transform.right * (_speed * Time.deltaTime));
    }
    //속도 * 델타타임 만큼 앞으로 총알 이동
    
    public void SetPoolManager(BulletPoolManager bulletPoolManager)
    { 
        _bulletPoolManager = bulletPoolManager;
    }
    // 수명을 다한 후 다시 오브젝트 풀로 돌아가기 위해 생성시 BulletPoolManager Class를 받아오기

    public void TimeLimit()
    {
        _distroyer = StartCoroutine(DieOnTime());
    }
    //오브젝트 풀에서 반환 된 후 수명 타이머를 활성화

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Wall") || col.CompareTag("Player"))
        {
            StopCoroutine(_distroyer);
            //두번 추가되지 않도록 코루틴 비활성화
            _bulletPoolManager.AddBullet2List(this.gameObject);
            //풀에 추가
        }
    }
    //벽 또는 Player에 충돌 한 후 비활성화 및 오브젝트 풀로 돌아가기

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator DieOnTime()
    {
        yield return new WaitForSeconds(_dietime);
        _bulletPoolManager.AddBullet2List(this.gameObject);
    }
    //수명 타이머 (코루틴), 수명이 다하면 비활성화 및 오브젝트 풀로 돌아가기
}
