using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet; // Bullet prefab with Bullet script attached

    // Bullet force
    public float shootForce, upwardForce;

    // Gun stats
    public float timeBetweenShoot, spread, reloadTime, timeBetweenShot;
    public int magazineSize, bullestPerTap;  // Typo: "bullestPerTap" should probably be "bulletsPerTap"
    public bool allowButtonShot;

    int bulletLeft, bulletShot;

    // Bools
    bool shooting, readyToShoot, reloading;

    // Reference
    public Camera fpsCam;
    public Transform attackPoint;

    // Graphics
    public TextMeshProUGUI ammunitionDisplay;

    public bool allowInvoke = true;

    void Awake()
    {
        // Magazine is full
        bulletLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        // Set ammo display
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletLeft / bullestPerTap + " / " + magazineSize / bullestPerTap);  // Typo in "bullestPerTap"
    }

    void MyInput()
    {
        // Check if allowed to hold down button and take corresponding input
        if (allowButtonShot) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletLeft < magazineSize && !reloading) Reload();

        // Reload automatically when trying to shoot without ammo
        if (readyToShoot && shooting && !reloading && bulletLeft <= 0) Reload();

        // Shooting
        if (readyToShoot && shooting && !reloading && bulletLeft > 0)
        {
            // Set bullets shot to 0
            bulletShot = 0;

            Shoot();
        }
    }

    void Shoot()
    {
        readyToShoot = false;

        // Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); // Just a point far away from the player

        // Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        // Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        // Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        // Rotate bullet to shoot direction
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        bulletLeft--;
        bulletShot++;

        // Invoke resetShot function
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShoot);
            allowInvoke = false;
        }

        // If more than one bulletPerTap
        if (bulletShot < bullestPerTap && bulletLeft > 0)
            Invoke("Shoot", timeBetweenShot);
    }

    private void ResetShot()
    {
        // Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletLeft = magazineSize;
        reloading = false;
    }
}
