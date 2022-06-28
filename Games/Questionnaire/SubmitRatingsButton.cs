/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    public class SubmitRatingsButton : Button {

        public GameObject m_EndObject;
        public AudioClip m_FinishSound;

        // On selecting this button.
        public override void Activate() {
            QuestionnaireDataTracker.QuestionnaireInstance.CollectRatings();
            QuestionnaireDataSender.SendQuestionnaireData();
            
            m_EndObject.SetActive(true);
            QuestionnaireUI.MainQuestionnaireUI.gameObject.SetActive(false);
            SoundManager.PlaySound(m_FinishSound);
        }

    }
}