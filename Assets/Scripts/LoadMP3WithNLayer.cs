using mp3infos;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadMP3WithNLayer : MonoBehaviour {
    public string Path = @"D:\Unity\WebApplicationData\my.mp3";
    public string url = "http://www.test.com/my.mp3";
    AudioSource audioSource;
    void Start () {
        audioSource = GetComponent<AudioSource>();
        MP3Info info = MP3Helper.ReadMP3Info(Path);
        audioSource.clip = Mp3Loader.LoadMp3(Path);
        if (!string.IsNullOrEmpty(info.Title))
        {
            Debug.Log(info.ToString());
        }
        //StartCoroutine(GetAudioClipAsStream());
    }

    IEnumerator GetAudioClipAsStream()
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
        }

        Stream stream = new MemoryStream(www.bytes);
        audioSource.clip = Mp3Loader.LoadMp3(stream);
    }


    public void Play()
    {
        if (enabled && null !=audioSource&&null!=audioSource.clip)
        {
            if(!audioSource.isPlaying)audioSource.Play();
        }
    }

	void Update () {
		
	}
}
