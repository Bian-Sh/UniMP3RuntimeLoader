using mp3infos;
using NLayer;
using System.IO;
using UnityEngine;

public static class Mp3Loader
{



    public static AudioClip LoadMp3(string filePath)
    {
        string filename = System.IO.Path.GetFileNameWithoutExtension(filePath);

        MpegFile mpegFile = new MpegFile(filePath);

        // assign samples into AudioClip
        AudioClip ac = AudioClip.Create(filename,
                                        (int)(mpegFile.Length / sizeof(float) / mpegFile.Channels),
                                        mpegFile.Channels,
                                        mpegFile.SampleRate,
                                        true,
                                       data => { int actualReadCount = mpegFile.ReadSamples(data, 0, data.Length); }
                                        //p=> { Debug.Log(p.ToString()); }//position => { mpegFile = new MpegFile(filePath);                                     }
                                        );

        return ac;
    }

    public static AudioClip LoadMp3(Stream stream)
    {

        MP3Info info = MP3Helper.ReadMP3Info(stream);
        string musicName = "DefaultName";
        if (!string.IsNullOrEmpty(info.Title))
        {
            musicName = info.Title;
        }
        MpegFile mpegFile = new MpegFile(stream);

        // assign samples into AudioClip
        AudioClip ac = AudioClip.Create(musicName,
                                        (int)(mpegFile.Length / sizeof(float) / mpegFile.Channels),
                                        mpegFile.Channels,
                                        mpegFile.SampleRate,
                                        true,
                                       data => { int actualReadCount = mpegFile.ReadSamples(data, 0, data.Length); }
                                        //p=> { Debug.Log(p.ToString()); }//position => { mpegFile = new MpegFile(filePath);                                     }
                                        );

        return ac;
    }

}