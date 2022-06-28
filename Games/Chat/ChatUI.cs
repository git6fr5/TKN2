/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    public class ChatUI : UI {

        /* --- Variables --- */
        #region Variables
        
        // Singleton.
        public static ChatUI MainChatUI;

        // The textbox where the prompt is read out to.
        [SerializeField] private Text m_PromptTextUI;

        // The current active components.
        [HideInInspector] private Twine m_ActiveTwine;
        [SerializeField, ReadOnly] private List<GameObject> m_ActiveResponses;

        // Sounds.
        [SerializeField] public AudioClip NextLineSound;
        [SerializeField] public AudioClip PrintLetterSoundA;
        [SerializeField] public AudioClip PrintLetterSoundB;
        
        #endregion

        // Runs once on instantiation.
        public override void Awake() {
            base.Awake();
            MainChatUI = this;
            m_PromptTextUI.font = m_Font;
            m_PromptTextUI.fontSize = m_FontSize;
            m_PromptTextUI.color = m_FontColor;
        }

        // Runs once every frame.
        void Update() {
            m_PromptTextUI.text = m_ActiveTwine.PrintText.ToUpper();
        }
        
        // Set the current active prompt
        public static void SetTwine(Twine twine) {
            MainChatUI.m_ActiveTwine = twine;
        }

        // Create a responnse.
        public static void CreateResponseButton(Response response, int optionNumber, int optionCount) {
            // Create the button.
            Vector3 position = UI.PositionOnScreen(optionNumber, optionCount, false);
            GameObject responseObject = UI.CreateClickable(response.Text, position, ResponseButton.DefaultSize);
            
            // Add the text for the response.
            UI.AddImage(responseObject, MainChatUI.m_ButtonBackgroundSprite, ResponseButton.DefaultSize);
            UI.AddText(responseObject, response.Text, ResponseButton.DefaultSize);
            
            // Add the response functionality.
            responseObject.AddComponent<ResponseButton>(); // Just add the component in the inspector and remove this line.
            responseObject.GetComponent<ResponseButton>().SetNextTwine(response.NextTwine, response);
            responseObject.SetActive(true);
            
            // Add this response to the list of currently active responses.
            MainChatUI.m_ActiveResponses.Add(responseObject);
        }

        // Reset the displayed responses.
        public static void Reset() {

            // Itterate through and destroy the response objects.
            List<GameObject> responses = MainChatUI.m_ActiveResponses;
            if (responses != null) {
                for (int i = 0; i < responses.Count; i++) {
                    if (responses[i] != null) {
                        Destroy(responses[i]);
                    }
                }
            }

            // Re-initialize the active response list.
            MainChatUI.m_ActiveResponses = new List<GameObject>();
        }

    }

}