/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    [System.Serializable]
    public class ChatDataTracker : DataTracker {

        /* --- Variables --- */
        #region Variables

        // Singleton.
        public static ChatDataTracker ChatInstance;

        // Data.
        [SerializeField] private List<Response> m_Responses;

        // Final twine.
        [SerializeField] private Twine m_FinalTwine;
        public Twine FinalTwine => m_FinalTwine;
        
        #endregion

        void Awake() {
            ChatInstance = this;
        }

        public void AddResponseData(ResponseButton responseButton) {
            m_Responses.Add(responseButton.StoredResponse);
        }


    }

}