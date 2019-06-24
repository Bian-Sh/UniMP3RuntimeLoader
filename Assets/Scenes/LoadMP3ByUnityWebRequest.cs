using mp3infos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class LoadMP3ByUnityWebRequest : MonoBehaviour
{

    public string url = @"http://www.ytmp3.cn/down/47037.mp3"; //this mp3 info such as title data was hided by some reason.

    AudioSource audioSource; //by which we can play music.
    Slider slider; //Show Progress of the music.
    Text title; // show the title of the ..
    Text msg;
    Text detail;
    Button button; // play button
    void Start()
    {
        #region InitComponent
        audioSource = GetComponent<AudioSource>();
        slider = FindObjectOfType<Slider>(); // what a sample demo that makes me use this way to link a reference.
        title = FindObjectsOfType<Text>().Where(v => v.name == "TitleText").First();
        msg = FindObjectsOfType<Text>().Where(v => v.name == "InfoText").First();
        detail = FindObjectsOfType<Text>().Where(v => v.name == "DetailText").First();
        button = FindObjectOfType<Button>();
        #endregion
        button.onClick.AddListener(OnPlayMusicRequired);
        button.interactable = false;

        #region Load MP3 File
        LoadMp3File();
        #endregion
    }

    private void LoadMp3File()
    {
        msg.text = "Start Load MP3 ..";
        StartCoroutine(GetAudioClipAsStream());

    }

    IEnumerator GetAudioClipAsStream()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.error != null)
            {
                Debug.Log(www.error);
                yield break;
            }
            Stream stream = new MemoryStream(www.downloadHandler.data); 

            MP3Info info = MP3Helper.ReadMP3Info(stream);
            audioSource.clip = Mp3Loader.LoadMp3(stream);
            if (audioSource.clip.length > 1) // check if the clip is loaded, mybe not that clever,ha..
            {
                msg.text = "MP3 has been Loaded .. ";
                button.interactable = true;
            }
            if (!string.IsNullOrEmpty(info.Title)) // fill the title text
            {
                Debug.Log(info.ToString());
                string name = info.Title.Trim();
                title.text = name;
                detail.text = info.ToString();
            }
        }
    }

    private void OnPlayMusicRequired()
    {
        if (null != audioSource && null != audioSource.clip)
        {
            if (!audioSource.isPlaying)
            {
                msg.text = "Playing .. ";
                audioSource.Play();
            }
            else
            {
                msg.text = "Already on the go ";
            }
        }
    }
}
