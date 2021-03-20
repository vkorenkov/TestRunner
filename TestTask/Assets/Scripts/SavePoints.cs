using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SavePoints
{
    /// <summary>
    /// Поле сериалайзера
    /// </summary>
    XmlSerializer serializer = new XmlSerializer(typeof(int));

    /// <summary>
    /// Поле пути к файлу сохранения количества очков игрока
    /// </summary>
    string createFilePath;

    /// <summary>
    /// Метод сохранения очков игрока
    /// </summary>
    /// <param name="player"></param>
    public void SavePoint(PlayerModel player)
    {
        CreatePathString();

        // Создание папки для файла лучшего счета при ее отсутствии
        if (!Directory.Exists(createFilePath))
        {
            Directory.CreateDirectory(createFilePath);
        }

        // Сериализациия счета игрока в файл XML
        using (var fs = new FileStream($"{createFilePath}/best.xml", FileMode.OpenOrCreate))
        {
            serializer.Serialize(fs, player.BestCount);
        }
    }

    /// <summary>
    /// Метод считывания файла очков игрока
    /// </summary>
    /// <returns></returns>
    public int GetPoint()
    {
        int bestPoint = 0;

        CreatePathString();

        if (File.Exists($"{createFilePath}/best.xml"))
        {
            // Десериализация файла счета игрока
            using (var reader = new FileStream($"{createFilePath}/best.xml", FileMode.OpenOrCreate))
            {
                bestPoint = (int)serializer.Deserialize(reader);
            }
        }

        return bestPoint;
    }

    /// <summary>
    /// Метод получения пути к файлу
    /// </summary>
    void CreatePathString()
    {
        createFilePath = $"{Application.dataPath}/Resources/xml/bestPoints";
    }
}
