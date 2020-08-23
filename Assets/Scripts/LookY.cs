using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float vsens = 5f,mouseY,xRotate;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void VerticalLook()
    {
        mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * vsens;
        xRotate -= mouseY;
        xRotate = Mathf.Clamp(xRotate, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotate, 0f, 0f);
    }
}
