/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    public class SoundManager : MonoBehaviour {

        // The background music.
        [SerializeField] private AudioClip m_MSC;

        // The sound effects.
        private static AudioSource MSCPlayer;
        private static List<AudioSource> SFXPlayers;

        void Start() {
            CreateMusicPlayer(m_MSC);
            CreateSoundEffectPlayers();

        }

        private static void CreateMusicPlayer(AudioClip music) {
            MSCPlayer = new GameObject("Music Player", typeof(AudioSource)).GetComponent<AudioSource>();
            MSCPlayer.transform.SetParent(Camera.main.transform);
            MSCPlayer.transform.position = Vector3.zero;
            MSCPlayer.clip = music;
            MSCPlayer.volume = 0.25f;
            MSCPlayer.loop = true;
            MSCPlayer.Play();
        }

        private static void CreateSoundEffectPlayers() {
            SFXPlayers = new List<AudioSource>();
            for (int i = 0; i < 10; i++) {
                SFXPlayers.Add(new GameObject("SFX Player " + i.ToString(), typeof(AudioSource)).GetComponent<AudioSource>());
                SFXPlayers[i].transform.SetParent(Screen.MainCamera.transform);
                SFXPlayers[i].transform.position = Vector3.zero;
            }
        }

        public static void PlaySound(AudioClip audioClip, float volume = 0.45f) {

            for (int i = 0; i < SFXPlayers.Count; i++) {
                if (!SFXPlayers[i].isPlaying) {
                    SFXPlayers[i].clip = audioClip;
                    SFXPlayers[i].volume = volume;
                    SFXPlayers[i].Play();
                    return;
                }
            }

            SFXPlayers.Add(new GameObject("SFX Player " + (SFXPlayers.Count - 1).ToString(), typeof(AudioSource)).GetComponent<AudioSource>());
            SFXPlayers[SFXPlayers.Count - 1].transform.SetParent(Screen.MainCamera.transform);
            SFXPlayers[SFXPlayers.Count - 1].transform.position = Vector3.zero;
            SFXPlayers[SFXPlayers.Count - 1].clip = audioClip;
            SFXPlayers[SFXPlayers.Count - 1].volume = volume;
            SFXPlayers[SFXPlayers.Count - 1].Play();

        }

    }
}