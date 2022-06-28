/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    public class SubmitSpotsButton : Button {

        public GameObject m_EndObject;
        public GameObject m_Image;
        public AudioClip m_FinishSound;

        // On selecting this button.
        public override void Activate() {
            SpotDataTracker.SpotInstance.CollectSpots();
            SpotDataSender.SendSpotData();

            m_EndObject.SetActive(true);
            m_Image.SetActive(false);
            SpotUI.MainSpotUI.gameObject.SetActive(false);
            SoundManager.PlaySound(m_FinishSound);
        }

    }
}