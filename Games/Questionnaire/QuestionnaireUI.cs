/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    public class QuestionnaireUI : UI {

        /* --- Variables --- */
        #region Variables

        // Singleton.
        public static QuestionnaireUI MainQuestionnaireUI;
        public static int MaxRating = 10;

        // The list of questions.
        [SerializeField] private string[] m_Questions;

        // Temporary reference object till this is ported to pure script.
        [SerializeField] private GameObject questionObject;

        #endregion
        
        /* --- Unity --- */
        #region Unity

        // Runs once on instantiation.
        public override void Awake() {
            base.Awake();
            MainQuestionnaireUI = this;
        }
        
        // Runs once before the first frame.
        void Start() {
            CreateQuestionnaire();
        }
        
        private void CreateQuestionnaire() {
            for (int i = 0; i < m_Questions.Length; i++) {
                Vector3 position = UI.PositionOnScreen(i, m_Questions.Length, true);
                GameObject obj = CreateRating(questionObject, position, m_Questions[i]);
            }
        }

        public static GameObject CreateRating(GameObject obj, Vector3 position, string question) {
            // Create the gameObject.
            GameObject gameObject = Instantiate(obj, position, Quaternion.identity, MainQuestionnaireUI.transform);
            gameObject.GetComponent<Rating>().Init(question, MaxRating);

            UI.SetText(gameObject.GetComponent<Rating>().RatingText);
            UI.SetText(gameObject.GetComponent<Rating>().ValueDisplay);

            return gameObject;
        }
        
        #endregion

    }
}