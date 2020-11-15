using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger : MonoBehaviour
{
    [Tooltip("DeathWall to get enabled")]
    public GameObject DeathWall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DeathWall.SetActive(true);
        Destroy(this.gameObject);
    }
}
