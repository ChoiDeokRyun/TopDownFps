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
    public float Damage;       //������
    public int MaxArmor;     //�ִ� �Ѿ�

    [Header ("FireProperty")]
    public int ShotCount;         //�ѹ��� �߻��ϴ� �Ѿ��� ��
    public float Bullet_Speed;    //�Ѿ� �ӵ�
    public float Rebound;         //�ݵ� (����)
    public float MaxFireCoolTime; //�ٽ� �߻��ϴ� ��Ÿ��
    public float MaxReloadingCool;//�������ð�

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
        ReloadCool();
    }
    private void Update()
    {
        switch (m_GunState)
        {
            case GUNSTATE.WAIT:

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    m_GunState = GUNSTATE.SHOT;
                    anim.SetBool("OnShot", true);
                }

                break;

            case GUNSTATE.SHOT:
                Fire();

                if (!Input.GetKey(KeyCode.Mouse0) || CurArmor == 0)
                {
                    anim.SetBool("OnShot", false);
                    m_GunState = GUNSTATE.WAIT;
                }

                break;

            case GUNSTATE.RELOADING:
                break;
        }
    }
    public void Fire()
    {
        if (FireCool >= MaxFireCoolTime && CurArmor>0)
        {
            for (int i = 0; i < ShotCount; i++)
            {
                //Rebound�� ���� ���� ��������
                float Angle = transform.eulerAngles.y + Random.RandomRange(-Rebound, Rebound);
                Quaternion rot = Quaternion.Euler(0, Angle, 0);

                //����ü ����
                GameObject g_NewBullet = Instantiate(g_Bullet, MuzzleOffset.transform.position, rot);
                Rigidbody rigid = g_NewBullet.GetComponent<Rigidbody>();

                //����ü �߻�
                rigid.velocity = g_NewBullet.transform.forward * Bullet_Speed;

                //�Ѿ� ����
                CurArmor--;
            }
            FireCool = 0;
        }
    }
    void ReloadCool()
    {
        if (FireCool <= MaxFireCoolTime)
            FireCool += Time.deltaTime;
    }
    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(MaxReloadingCool);
        CurArmor = MaxArmor;
    }
}
