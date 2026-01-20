using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADS : MonoBehaviour
{
    [Header("Cameras")]
    public Camera mainCam;
    public Camera weaponCam;

    [Header("FOV")]
    public float speed = 12f;
    public float adsFOV = 55f;

    [Header("Weapon")]
    public Transform weaponHolder;
    public GameObject weaponModel;
    public Vector3 hipPos = new Vector3(0.25f, -0.25f, 0.6f);
    public Vector3 adsPos = new Vector3(-1.05f, 0.5f, -1.5f);

    private float normalFOV;


    private void Start()
    {
        mainCam = Camera.main;
        normalFOV = mainCam.fieldOfView;
    }

    private void Update()
    {
        if(ToggleInventory.isOpen) return;

        bool aiming = Input.GetMouseButton(1);

        if(weaponModel != null && !weaponModel.activeInHierarchy)
        {
            aiming = false;
        }
        float targetFOV = aiming ? adsFOV : normalFOV;

        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, targetFOV, speed * Time.deltaTime);
        weaponCam.fieldOfView = Mathf.Lerp(weaponCam.fieldOfView, targetFOV, speed * Time.deltaTime);

        Vector3 targetPos = aiming ? adsPos : hipPos;
        weaponHolder.localPosition = Vector3.Lerp(weaponHolder.localPosition, targetPos, speed * Time.deltaTime);
    }
}
