/* --- Libraries --- */
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    [System.Serializable]
    public class DataTracker : MonoBehaviour {

        /* --- Variables --- */
        #region Variables

        // Singleton.
        public static DataTracker Instance;
        
        // The number of frames that have passed since the game started.
        [SerializeField, ReadOnly] private int m_Frames;
        public int Frames => m_Frames;

        // The time that has passed since the game started.
        [SerializeField, ReadOnly] private float m_Ticks;
        public static float Ticks => Instance.m_Ticks;

        // Tracks the position of the cursor.
        [HideInInspector] private Heatmap m_HoverMap;
        public Heatmap HoverMap => m_HoverMap;

        // Tracks the position of the clicks.
        [HideInInspector] private Heatmap m_ClickMap;
        public Heatmap ClickMap => m_ClickMap;

        // Checks if the user has clicked this frame.
        private bool Click => Input.GetMouseButtonDown(0);

        #endregion

        /* --- Unity --- */
        #region Unity
        
        // Runs once on instantiation.
        void Start() {
            // Set up the singleton.
            Instance = this;
            // Reset these variables.
            m_Frames = 0;
            m_Ticks = 0f;
            // Initialize the heatmaps.
            m_HoverMap = new Heatmap(
                Heatmap.DefaultRows, 
                Heatmap.DefaultCols, 
                Screen.ScreenSize,
                Screen.Origin
                );
            m_ClickMap = new Heatmap(
                Heatmap.DefaultRows, 
                Heatmap.DefaultCols, 
                Screen.ScreenSize,
                Screen.Origin
                );
        }
        
        // Runs once every frame.
        void Update() {
            // Increment the frames.
            m_Frames += 1;

            // Gather the data.
            ProcessCursorMovement();
            if (Click) { ProcessClick(); }

        }
        
        // Runs once every fixed interval.
        void FixedUpdate() {
            float deltaTime = Time.fixedDeltaTime;
            // Increment the time.
            m_Ticks += deltaTime;
        }
        
        #endregion

        /* --- Data --- */
        #region Data

        // The data to be stored every frame.
        protected virtual Vector2 ProcessCursorMovement() {
            // Append this data to heat map.
            Vector2 position = Screen.MousePosition;
            // m_HoverMap.Add(position);
            return position;
        }
        
        // The data to be stored every click.
        protected virtual Vector2 ProcessClick() {
            // Append this data to the heat map.
            Vector2 position = Screen.MousePosition;
            // m_ClickMap.Add(position);
            return position;
        }
        
        #endregion

    }

}


