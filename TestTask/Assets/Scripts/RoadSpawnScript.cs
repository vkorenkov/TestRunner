using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawnScript : MonoBehaviour
{
    /// <summary>
    /// Массив префабов дороги
    /// </summary>
    [SerializeField] GameObject[] prefabs;
    /// <summary>
    /// Стартовая пазиция первого перфаба
    /// </summary>
    [SerializeField] GameObject startRoadpart;
    /// <summary>
    /// Позиция установки следующего блока дороги
    /// </summary>
    float _roadPartPosition = 0;
    /// <summary>
    /// Длина блока дороги 
    /// </summary>
    float _roadPartLenght = 0;
    /// <summary>
    /// Количество отображаемых блоков
    /// </summary>
    [SerializeField] int _roadPartsCount = 15;
    /// <summary>
    /// Коллекция текущих отображаемых блоков дороги
    /// </summary>
    public static List<GameObject> currentRoad;

    void Start()
    {
        // Получение позиции блока дороги по координате z
        _roadPartPosition = startRoadpart.transform.position.z;
        // Длина блока дороги
        _roadPartLenght = startRoadpart.GetComponent<BoxCollider>().bounds.size.z;
        // Инициализация коллекции
        currentRoad = new List<GameObject>();
        // Добавление первого блока дороги в коллекцию
        currentRoad.Add(startRoadpart);
        // Цикл создания блоков дороги
        for (var i = 0; i < _roadPartsCount; i++)
        {
            SetRoadPart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentRoad.Count < _roadPartsCount)
        {
            SetRoadPart();
        }
    }

    /// <summary>
    /// Метод создания блока дороги
    /// </summary>
    void SetRoadPart()
    {
        // Создание случайного префаба 
        GameObject roadPart = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
        // Вычисление позиции блока дороги в конце предыдущего блока
        var temp = currentRoad.Last().transform.position.z - 0.1f;
        temp += _roadPartLenght;
        //_roadPartPosition += _roadPartLenght;
        // Установка позиции блока дороги в конце предыдущего блока
        roadPart.transform.position = new Vector3(0, 0, temp /* _roadPartPosition*/);
        // Добавление блока дороги в коллекцию отображаемых блоков дороги
        currentRoad.Add(roadPart);
    }
}
