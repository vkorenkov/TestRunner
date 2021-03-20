using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    /// <summary>
    /// Объект слежения камеры
    /// </summary>
    [SerializeField] Transform cameraTarget;
    /// <summary>
    /// Дистанция до объекта слежения камеры
    /// </summary>
    [Range(5, 20)] public float distanceFromTarget;
    /// <summary>
    /// Свойство позиции камеры
    /// </summary>
    Vector3 _CameraPosition
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    [SerializeField] bool isStaticCamera = true;

    private void Start()
    {
        // Первоначальное положение камеры
        Camera.main.transform.position = cameraTarget.position;
    }

    private void LateUpdate()
    {
        // Условие прикотором камера остается сттатичной или перемещается за персонажем в стороны
        if (isStaticCamera)
        {
            // Получение координаты z для передвижения камеры за персонажем
            var z = (cameraTarget.position - transform.forward * distanceFromTarget).z;

            // Расчет позиции камеры
            _CameraPosition = new Vector3(_CameraPosition.x, _CameraPosition.y, z);
        }
        else
            _CameraPosition = cameraTarget.position - transform.forward * distanceFromTarget;
    }
}
