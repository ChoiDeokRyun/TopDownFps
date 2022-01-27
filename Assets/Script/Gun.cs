using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("GunObject")]
    public GameObject g_Bullet;
    public GameObject MuzzleOffset;

    [Header ("State")]
    public float Damage;       //데미지
    public float MaxArmor;     //최대 총알

    [Header ("FireProperty")]
    public int ShotCount;         //한번에 발사하는 총알의 수
    public float Bullet_Speed;    //총알 속도
    public float Rebound;         //반동 (각도)
    public float MaxFireCoolTime; //다시 발사하는 쿨타임
    public float MaxReloadingCool;//재장전시간

    private Animator anim;

    private float FireCool;
    private float ReloadingCool;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        ReloadCool();

        if (Input.GetKey(KeyCode.Mouse0))
            Fire();
    }
    public void Fire()
    {
        if (FireCool >= MaxFireCoolTime)
        {
            for (int i = 0; i < ShotCount; i++)
            {
                //Rebound에 맞춰 랜덤 각도지정
                float Angle = transform.eulerAngles.y + Random.RandomRange(-Rebound,Rebound);
                Quaternion rot = Quaternion.Euler(0, Angle, 0);

                //투사체 생성
                GameObject g_NewBullet = Instantiate(g_Bullet, MuzzleOffset.transform.position, rot);
                Rigidbody rigid = g_NewBullet.GetComponent<Rigidbody>();

                //투사체 발사
                rigid.velocity = g_NewBullet.transform.forward * Bullet_Speed;
            }
            FireCool = 0;
        }
    }
    void ReloadCool()
    {
        if (FireCool <= MaxFireCoolTime)
            FireCool += Time.deltaTime;
    }
}
