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
    public class QuestionnaireDataTracker : DataTracker {

        /* --- Variables --- */
        #region Variables

        // Singleton.
        public static QuestionnaireDataTracker QuestionnaireInstance;

        // Data.
        [System.Serializable] 
        public class RatingData {
            public string Question;
            public int Value;
            public int MaxValue;

            public RatingData(string question, int value, int maxValue) {
                Question = question;
                Value = value;
                MaxValue = maxValue;
            }
        }

        [SerializeField] private List<RatingData> m_Ratings;

        #endregion

        void Awake() {
            QuestionnaireInstance = this;
        }

        public void CollectRatings() {
            print("Collecting");
            Rating[] ratings = (Rating[])GameObject.FindObjectsOfType(typeof(Rating));
            for (int i = 0; i < ratings.Length; i++) {
                RatingData ratingData = new RatingData(ratings[i].RatingText.text, 
                                            (int)ratings[i].RatingSlider.value,
                                            (int)ratings[i].RatingSlider.maxValue);
                m_Ratings.Add(ratingData);
            }
        }


    }

}