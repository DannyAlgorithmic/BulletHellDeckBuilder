using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public KeyCode fireKey = KeyCode.Mouse1;
    public float bulletSpeed = 10f;
    public Transform[] muzzles;
    public Collider2D[] selfColliders = null;
    public Rigidbody2D bulletPrefab = null;

    public float cooldown = 0f;

    void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            int nuzzleCount = muzzles.Length;
            for (int i = 0; i < nuzzleCount; i++)
            {
                if (muzzles[i] == null) continue;
                Transform muzzle = muzzles[i];
                Rigidbody2D bulletBody = GameObject.Instantiate(bulletPrefab);
                Transform bulletTrans = bulletBody.transform;

                Collider2D bulletCollider = bulletBody.GetComponent<Collider2D>();
                foreach (Collider2D selfCollider in selfColliders)
                    Physics2D.IgnoreCollision(selfCollider, bulletCollider);

                PositionProjectile(bulletBody, bulletTrans, muzzle);
                // OrientProjectile(bulletBody, bulletTrans, muzzle);
                PropelProjectile(bulletBody, bulletTrans, muzzle);
            }
        }
    }

    public void PositionProjectile(Rigidbody2D _bulletBody, Transform _bulletTrans, Transform _muzzle)
    {
        _bulletTrans.position = _muzzle.position;
    }
    public void OrientProjectile(Rigidbody2D _bulletBody, Transform _bulletTrans, Transform _muzzle)
    {
        _bulletTrans.rotation = _muzzle.rotation;
    }
    public void PropelProjectile(Rigidbody2D _bulletBody, Transform _bulletTrans, Transform _muzzle)
    {
        _bulletBody.velocity = -_muzzle.up.normalized * bulletSpeed;
    }
}