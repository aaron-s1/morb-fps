using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForPlayer : MonoBehaviour
{
    [SerializeField] bool isRanged;

    [Space(10)]
    [SerializeField] Transform playerPos;
    [SerializeField] float playerPosYOffset;
    [SerializeField] float viewDistance;
    [SerializeField] float viewAngle;


    List<GameObject> bulletPool;

    MonoBehaviour attackScript;


    void Start()
    {
        playerPos = GameObject.Find("Player").transform.GetChild(0);
        SendMessage("PrepareAttack");
    }


    bool CanSearch()
    {
        if (!isRanged)
            return GetComponent<EnemyMeleeAttack>().CanAttack();
        else
            return GetComponent<EnemyFiresBullet>().CanAttack();
    }


    void FixedUpdate() =>
        FindPlayer();


    void FindPlayer()
    {
        if (!CanSearch())
            return;

        playerPos.position = new Vector3 (playerPos.position.x, playerPos.position.y + playerPosYOffset, playerPos.position.z);
        // playerPos.position = playerPos.position + new Vector3(playerPosOffset.x, playerPosOffset.y, playerPosOffset.z);

        if (Vector3.Distance(transform.position, playerPos.position) < viewDistance)
        {
            Vector3 directionToPlayer = (playerPos.position - transform.position).normalized;
            float angleBetweenSelfAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleBetweenSelfAndPlayer < viewAngle / 2)
            {
                if (Physics.Linecast(transform.position, playerPos.position))
                    SendMessage("BeginAttack", playerPos);
            }
        }
    }
}
