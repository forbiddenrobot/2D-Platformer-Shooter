using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    [SerializeField] private AIPath aIPath;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (aIPath.desiredVelocity.x >= 0.01)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
