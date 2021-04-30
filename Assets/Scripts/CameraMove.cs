using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offsetZ;

    private void LateUpdate()
    {
        transform.position = transform.position.ChangeZ(target.position.z + offsetZ);
    }
}
