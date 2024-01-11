using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionTextUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionText;

    // Start is called before the first frame update
    void Start()
    {
        versionText.text = "Version: " + Application.version.ToString();
    }
}
