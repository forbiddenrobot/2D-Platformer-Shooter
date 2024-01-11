using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualAnimationEvents : MonoBehaviour
{
    public void DeathAnimationEnd()
    {
        GameManager.instance.PlayerDied(transform.parent.gameObject);
    }
}
