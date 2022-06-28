/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    public class SpotDataTracker : DataTracker {

        /* --- Variables --- */
        #region Variables

        [System.Serializable]
        public class SpotData : Data {

            // Id.
            [SerializeField] public string m_Name;

            // Settings.
            [SerializeField] public float m_PositionX;
            [SerializeField] public float m_PositionY;
            [SerializeField] public float m_Radius;

            // Finding.
            [SerializeField] public bool m_Found;
            [SerializeField] public float m_TimeFound;

            // Construct the spot data packet.
            public SpotData(Spot spot) {
                m_Name = spot.SpotName;
                m_PositionX = spot.Position.x;
                m_PositionY = spot.Position.y;
                m_Radius = spot.Radius;
                m_Found = spot.Selected;
                m_TimeFound = spot.TimeFound;
            }

        }
        
        public static SpotDataTracker SpotInstance;

        [SerializeField] List<SpotData> m_SpotData = new List<SpotData>();
        
        #endregion

        void Awake() {
            SpotInstance = this;
        }

        public void CollectSpots() {
            Spot[] spots = (Spot[])GameObject.FindObjectsOfType(typeof(Spot));
            for (int i = 0; i < spots.Length; i++) {
                SpotData spotData = new SpotData(spots[i]);
                m_SpotData.Add(spotData);
            }
        }

    }
}