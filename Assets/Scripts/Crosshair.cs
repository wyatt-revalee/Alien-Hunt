using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Crosshair : MonoBehaviour
{

    public GameObject bullet;
    public LayerMask enemyLayerMask;
    public Collider2D enemy;

    public event Action<bool> OnShotFired;
    public event Action<int> OnEnemyKilled;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Cursor.visible)
        {
            Cursor.visible = false;
        }
    }

    public void OnMove(InputValue value)
    {
        // Convert mouse position to position within camera, set our crosshair to that position. Set mouse z location to 1, otherwise it is 0 and meshes with background.
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(value.Get<Vector2>().x, value.Get<Vector2>().y, 10) );
    }

    public void OnShoot()
    {
        // Place new bullet
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().crosshair = this;

        ShakeCrosshair(1);
        ShakeCamera(1);

    }

    public void UpdateBulletInfo(bool enemyHit, int pointsGained)
    {
        OnShotFired?.Invoke(enemyHit);
        if (pointsGained > 0)
        {
            OnEnemyKilled?.Invoke(pointsGained);
        }
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

        // Get initial position
        float xpos = Camera.main.transform.position.x;
        float ypos = Camera.main.transform.position.y;
        float zpos = Camera.main.transform.position.z;

        // Move up and right
        Camera.main.transform.position = new Vector3(xpos + (0.01f * force), ypos + (0.01f * force), zpos);
        yield return new WaitForSeconds(0.01f);

        // Move down and left
        Camera.main.transform.position = new Vector3(xpos - (0.01f * force), ypos - (0.01f * force), zpos);
        yield return new WaitForSeconds(0.01f);

        Camera.main.transform.position = new Vector3(xpos, ypos, zpos);

    }
}
