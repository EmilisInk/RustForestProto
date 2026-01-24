using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public Vector3 swingRotation = new Vector3(-40f, 20f, 0f);
    public float swingTime = 0.08f;

    public int selectedHotbarIndex = 0;

    private Coroutine routine;


    public bool IsPlaying { get; private set; }
    public void Play()
    {
        if (!gameObject.activeInHierarchy) return;
        if (IsPlaying) return;
        if(InventoryManager.Instance == null) return;

        Item item = InventoryManager.Instance.GetHotbarItem(selectedHotbarIndex);
        if(item != null && item.isWeapon)
            return;

        routine = StartCoroutine(Swings());
    }

    IEnumerator Swings()
    {
        IsPlaying = true;

        Quaternion baseRot = transform.localRotation;
        Quaternion targetRot = Quaternion.Euler(swingRotation) * baseRot;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / swingTime;
            transform.localRotation = Quaternion.Slerp(baseRot, targetRot, t);
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / swingTime;
            transform.localRotation = Quaternion.Slerp(targetRot, baseRot, t);
            yield return null;
        }

        IsPlaying = false;
        routine = null;
    }
}
