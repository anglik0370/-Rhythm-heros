using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float mx, my;
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        mx += mouseX * StaticManager.Instance.rotateSpeed * Time.deltaTime;
        my += mouseY * StaticManager.Instance.rotateSpeed * Time.deltaTime;

        mx = Mathf.Clamp(mx, -38, 38);
        my = Mathf.Clamp(my, -90, 90);

        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
