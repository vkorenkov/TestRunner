using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    /// <summary>
    /// Поле коллекция звукового оформления
    /// </summary>
    [SerializeField] List<AudioSource> sounds;

    /// <summary>
    /// Метод запуска звукового файла
    /// </summary>
    /// <param name="whatSound"></param>
    public void PlaySound(string whatSound)
    {
        // Поиск файла с подходящим именем
        sounds.Where(x => x.name.ToLower() == whatSound).FirstOrDefault().Play();
    }

    /// <summary>
    /// Метод остановки звукового файла
    /// </summary>
    /// <param name="whatSound"></param>
    public void StopSound(string whatSound)
    {
        // Поиск файла с подходящим именем
        sounds.Where(x => x.name.ToLower() == whatSound).FirstOrDefault().Stop();
    }

    /// <summary>
    /// Метод проверки воспроизводится ли сейчас звуковой файл
    /// </summary>
    /// <param name="whatSound"></param>
    /// <returns></returns>
    public bool IsPlaySound(string whatSound)
    {
        return sounds.Where(x => x.name.ToLower() == whatSound).FirstOrDefault().isPlaying;
    }
}
