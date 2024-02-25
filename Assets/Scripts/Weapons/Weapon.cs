using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public abstract class Weapon : MonoBehaviour
{

    [Header("Bullet Settings")]
    public GameObject bullet;
    public int bulletsPerShot;

    [Header("Weapon Settings")]
    public int damage;
    public int reloadSpeed;
    public int fireRate;
    public int magazineSize;
    public bool isAutomatic;
    public int bulletsInMagazine;
    public bool isReloading;
    public Sprite reloadingCrosshair;

    [Header("Target Settings")]
    public LayerMask enemyLayerMask;

    public Player player;
    public event Action OnShotFired;
    public event Action OnEnemyHit;
    public event Action<int> OnEnemyKilled;
    public event Action<int> OnBonusAdded;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    public void Move(InputValue value)
    {
        // Convert mouse position to position within camera, set our crosshair to that position. Set mouse z location to 1, otherwise it is 0 and meshes with background.
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(value.Get<Vector2>().x, value.Get<Vector2>().y, 10) );
    }

    public virtual void Shoot()
    {
    }

    public virtual void Reload()
    {
        if(isReloading)
        {
            return;
        }
        else
        {
            isReloading = true;
            StartCoroutine(DoReload());
        }
    }

    public void SpawnBullet(Vector3 location)
    {
        GameObject bulletInstance = Instantiate(bullet, location, Quaternion.identity);
        Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
        bulletScript.weapon = this;
        bulletInstance.transform.localScale = new Vector3(bullet.transform.localScale.x * player.bulletSizeModifer, bullet.transform.localScale.y * player.bulletSizeModifer, 1);
    }

    IEnumerator DoReload()
    {
        Sprite crosshair = GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().sprite = reloadingCrosshair;
        yield return new WaitForSeconds(reloadSpeed);
        GetComponent<SpriteRenderer>().sprite = crosshair;
        bulletsInMagazine = magazineSize;
        isReloading = false;
    }

    public virtual void UpdateBulletInfo(bool enemyHit, int pointsGained)
    {
    }

    public void ShakeCrosshair(int force)
    {
        StartCoroutine(DoCrosshairShake(force));
    }

    IEnumerator DoCrosshairShake(int force)
    {

        // Get initial position
        float xpos = this.transform.position.x;
        float ypos = this.transform.position.y;

        // Move up and right
        this.transform.position = new Vector3(xpos + (0.01f * force), ypos + (0.01f * force), 0);
        yield return new WaitForSeconds(0.01f);

        // Move down and left
        this.transform.position = new Vector3(xpos - (0.01f * force), ypos - (0.01f * force), 0);
        yield return new WaitForSeconds(0.01f);

        this.transform.position = new Vector3(xpos, ypos, 0);
    }

    public void ShakeCamera(int force)
    {
        StartCoroutine(DoCameraShake(force));
    }

    IEnumerator DoCameraShake(int force)
    {

        // Move up and right
        Camera.main.transform.position = new Vector3(0 + (0.01f * force), 0 + (0.01f * force), -10);
        yield return new WaitForSeconds(0.01f);

        // Move down and left
        Camera.main.transform.position = new Vector3(0 - (0.01f * force), 0 - (0.01f * force), -10);
        yield return new WaitForSeconds(0.01f);

        Camera.main.transform.position = new Vector3(0, 0, -10);

    }

    public void ShootWeaponEvent()
    {
        OnShotFired?.Invoke();
    }

    public void EnemyHitEvent()
    {
        OnEnemyHit?.Invoke();
    }

    public void EnemyKilledEvent(int points)
    {
        OnEnemyKilled?.Invoke(points);
    }

    public void AddBonus(int bonusPoints)
    {
        OnBonusAdded?.Invoke(bonusPoints);
    }

    public void ChangeCrosshairColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
