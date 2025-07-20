using UnityEngine;

public static class BulletShotType
{
    public static void Shot(Vector3 origin, Vector3 velocity, float speed)
    {
        Bullet bullet = BulletPool.Instance.bulletPool.Get();
        bullet.transform.position = origin;
        bullet.velocity = velocity;
        bullet.speed = speed;
    }

    public static void RadialShot(Vector3 origin, Vector3 velocity, RadialShotSettings settings)
    {
        float angleBetweenBullets = 360f / settings.NumberOfBullets;
        for(int i = 0; i < settings.NumberOfBullets; i++)
        {
            float bulletDirectionAngle = angleBetweenBullets * i; //90, 180, 270, etc.
            Vector3 bulletDirection = velocity.WorldRotate(bulletDirectionAngle);
            Shot(origin, bulletDirection, settings.BulletSpeed);
        }
    }

    public static void FixedRadiusShot(Vector3 origin, Vector3 velocity, RadialShotSettings settings)
    {
        float angleStep = (settings.maxRotation - settings.minRotation) / settings.NumberOfBullets;
        float angleStart = settings.minRotation;
        for (int i = 0; i < settings.NumberOfBullets; i++)
        {
            Vector3 bulletDirection = velocity.Rotate(angleStart);
            Shot(origin, bulletDirection, settings.BulletSpeed);
            angleStart += angleStep;
        }
    }
    public static void SphericalShot(Vector3 origin, Vector3 velocity, RadialShotSettings settings)
    {
        float angleBetweenBullets = 360f / settings.NumberOfBullets;
        float sphereAngleStep = (settings.maxRotation - settings.minRotation) / settings.NumberOfRings;
        float sphereAngleStart = settings.minRotation;
        for (int j = 0; j < settings.NumberOfRings; j++)
        {
            for (int i = 0; i < settings.NumberOfBullets; i++)
            {
                float bulletDirectionAngle = angleBetweenBullets * i;
                Vector3 bulletDirection = velocity.SphereRotate(bulletDirectionAngle, sphereAngleStart);

                Shot(origin, bulletDirection, settings.BulletSpeed);
            }
            sphereAngleStart += sphereAngleStep;
        }

    }

    // Maybe put these in extension methods for later
    private static Vector3 Rotate(this Vector3 originalVector, float rotateAngleInDegrees)
    {
        Quaternion rotation = Quaternion.AngleAxis(rotateAngleInDegrees, Vector3.up);
        return rotation * originalVector;
    }
    private static Vector3 WorldRotate(this Vector3 originalVector, float rotateAngleInDegrees)
    {
        Vector3 rotation = Quaternion.Euler(0, rotateAngleInDegrees, 0) * originalVector;
        return rotation;
    }
    private static Vector3 SphereRotate(this Vector3 originalVector, float rotateAngleInDegrees, float ringAngle)
    {
        Vector3 rotation = Quaternion.Euler(ringAngle, rotateAngleInDegrees, 0) * originalVector;
        return rotation;
    }
}
