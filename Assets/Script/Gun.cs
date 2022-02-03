using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GUNSTATE
    {
        WAIT,
        SHOT,
        RELOADING
    }

    public GUNSTATE m_GunState;

    [Header("GunObject")]
    public GameObject g_Bullet;
    public GameObject MuzzleOffset;

    [Header ("State")]
    public float Damage;       //데미지
    public int MaxArmor;     //최대 총알

    [Header ("FireProperty")]
    public int ShotCount;         //한번에 발사하는 총알의 수
    public float Bullet_Speed;    //총알 속도
    public float Rebound;         //반동 (각도)
    public float MaxFireCoolTime; //다시 발사하는 쿨타임
    public float MaxReloadingCool;//재장전시간

    private Animator anim;

    private float FireCool;
    private float ReloadingCool;
    public int CurArmor;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        m_GunState = GUNSTATE.WAIT;
        CurArmor = MaxArmor;
    }

    private void FixedUpdate()
    {
        Cool();
    }

    private void SetState(GUNSTATE GunState)
    {
        switch (GunState)
        {
            case GUNSTATE.WAIT:
                anim.SetBool("OnShot", false);
                break;
            case GUNSTATE.SHOT:
                anim.SetBool("OnShot", true);
                break;
            case GUNSTATE.RELOADING:
                GUIManager.Instance.OnCoolTime(MaxReloadingCool,"Reloading");
                StartCoroutine(Reloading());
                break;
        }
        m_GunState = GunState;
    }
    private void Update()
    {
        switch (m_GunState)
        {
            case GUNSTATE.WAIT:
                {
                    if (Input.GetKey(KeyCode.Mouse0))
                        SetState(GUNSTATE.SHOT);

                    if (Input.GetKeyDown(KeyCode.R))
                        SetState(GUNSTATE.RELOADING);

                    break;
                }
            case GUNSTATE.SHOT:
                {
                    Fire();

                    if (!Input.GetKey(KeyCode.Mouse0))
                        SetState(GUNSTATE.WAIT);

                    if (CurArmor == 0)
                        SetState(GUNSTATE.RELOADING);

                    break;
                }
            case GUNSTATE.RELOADING:
                {
                    break;
                }
        }
    }
    public void Fire()
    {
        if (FireCool >= MaxFireCoolTime && CurArmor>0)
        {
            for (int i = 0; i < ShotCount; i++)
            {
                //Rebound에 맞춰 랜덤 각도지정
                float Angle = transform.eulerAngles.y + Random.RandomRange(-Rebound, Rebound);
                Quaternion rot = Quaternion.Euler(0, Angle, 0);

                //투사체 생성
                ProjectileMover g_NewBullet = Instantiate(g_Bullet, MuzzleOffset.transform.position, rot).GetComponent<ProjectileMover>();
                g_NewBullet.Damage = Damage;
                Rigidbody rigid = g_NewBullet.gameObject.GetComponent<Rigidbody>();

                //투사체 발사
                rigid.velocity = g_NewBullet.transform.forward * Bullet_Speed;

                //총알 삭제
                CurArmor--;
            }
            FireCool = 0;
        }
    }
    void Cool()
    {
        if (FireCool <= MaxFireCoolTime)
            FireCool += Time.deltaTime;
    }
    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(MaxReloadingCool);
        CurArmor = MaxArmor;
        m_GunState = GUNSTATE.WAIT;
    }
}
