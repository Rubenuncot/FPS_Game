using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeWeapon : MonoBehaviour
{
    private Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        float xLookImput = Input.GetAxisRaw("Mouse X");
        float yLookImput = Input.GetAxisRaw("Mouse Y");

        Quaternion xAngleAdjust = Quaternion.AngleAxis(-xLookImput * 1.75f, Vector3.up);
        Quaternion yAngleAdjust = Quaternion.AngleAxis(yLookImput * 1.75f, Vector3.right);
        Quaternion targetFinalLocation = originalRotation * xAngleAdjust * yAngleAdjust;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetFinalLocation, Time.deltaTime * 10f);
    }
}
