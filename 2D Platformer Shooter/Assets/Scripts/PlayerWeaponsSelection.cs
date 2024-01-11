using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponsSelection : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;
    [SerializeField] private GameObject weaponHolder;

    private int currentWeapon;

    private void Awake()
    {
        weapons = new List<GameObject>();
        

        foreach (Transform weapon in weaponHolder.transform)
        {
            weapons.Add(weapon.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = 0;
        UpdateSelectedWeapon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSelectedWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetSelectedWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetSelectedWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetSelectedWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetSelectedWeapon(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetSelectedWeapon(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SetSelectedWeapon(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SetSelectedWeapon(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SetSelectedWeapon(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetSelectedWeapon(9);
        }
    }

    public void NextWeapon()
    {
        if (currentWeapon < weapons.Count - 1)
        {
            currentWeapon++;
        }
        else
        {
            currentWeapon = 0;
        }

        UpdateSelectedWeapon();
    }

    public void PreviousWeapon()
    {
        if (currentWeapon > 0)
        {
            currentWeapon--;
        }
        else
        {
            currentWeapon = weapons.Count - 1;
        }

        UpdateSelectedWeapon();
    }

    private void SetSelectedWeapon(int index)
    {
        if (index <= weapons.Count - 1)
        {
            currentWeapon = index;

            UpdateSelectedWeapon();

        }
    }

    private void UpdateSelectedWeapon()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        weapons[currentWeapon].SetActive(true);
    }
}
