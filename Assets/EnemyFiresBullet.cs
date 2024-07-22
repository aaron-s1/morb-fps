using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiresBullet : MonoBehaviour, IEnemyAttack
{
    [SerializeField] GameObject numberBulletPrefab;
    [SerializeField] float fireRate = 1f;
    [SerializeField] int bulletPoolSize = 10;

    [Space(10)]
    [SerializeField] Transform playerPos;
    [SerializeField] Vector3 playerPosOffset;

    List<GameObject> bulletPool;

    bool canAttack = true;

    
    public void PrepareAttack() =>
        CreateNewBulletPool();


    public IEnumerator BeginAttack(Transform playerPos)
    {
        canAttack = false;
        // playerPos = GameObject.FindWithTag("Player").transform;

        GameObject bullet = GetBulletFromPool();


        if (bullet != null)
        {
            var bulletTextPosition = bullet.transform.GetChild(0).position;
            bullet.transform.position = transform.position;
            bullet.transform.GetChild(0).position = transform.position;

            bullet.SetActive(true);
        }

        else
            Debug.Log("Bullet pool returned no bullet.");


        yield return new WaitForSeconds(fireRate);
        
        canAttack = true;
    }


    GameObject GetBulletFromPool()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
                return bulletPool[i];
        }

        return null;
    }


    void CreateNewBulletPool()
    {
        Debug.Log("ranged enemy attacked");
        bulletPool = new List<GameObject>();

        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(numberBulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public bool CanAttack() => canAttack;
}
