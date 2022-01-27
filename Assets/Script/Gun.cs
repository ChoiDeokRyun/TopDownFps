using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("GunObject")]
    public GameObject g_Bullet;
    public GameObject MuzzleOffset;

    [Header ("State")]
    public float Damage;       //������
    public float MaxArmor;     //�ִ� �Ѿ�

    [Header ("FireProperty")]
    public int ShotCount;         //�ѹ��� �߻��ϴ� �Ѿ��� ��
    public float Bullet_Speed;    //�Ѿ� �ӵ�
    public float Rebound;         //�ݵ� (����)
    public float MaxFireCoolTime; //�ٽ� �߻��ϴ� ��Ÿ��
    public float MaxReloadingCool;//�������ð�

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
                //Rebound�� ���� ���� ��������
                float Angle = transform.eulerAngles.y + Random.RandomRange(-Rebound,Rebound);
                Quaternion rot = Quaternion.Euler(0, Angle, 0);

                //����ü ����
                GameObject g_NewBullet = Instantiate(g_Bullet, MuzzleOffset.transform.position, rot);
                Rigidbody rigid = g_NewBullet.GetComponent<Rigidbody>();

                //����ü �߻�
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
