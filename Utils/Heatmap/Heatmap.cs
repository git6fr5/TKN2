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
    public class Heatmap : Data {

        // Default values.
        public static int DefaultCols = 135;
        public static int DefaultRows = 90;

        // Data needed for the heatmap.
        private int[][] m_Grid;
        public int[][] Grid => m_Grid;
        private Vector2 m_Dimensions;
        private Vector2 m_Center;

        // Useful values calculated from the data.
        public int Cols => m_Grid[0].Length;
        public int Rows => m_Grid.Length;

        // Constructor that initializes the heatmap.
        public Heatmap(int rows, int columns, Vector2 dimensions, Vector2 center) {
            // Initialize the array.
            m_Grid = new int[rows][];
            for (int i = 0; i < rows; i++) {
                m_Grid[i] = new int[columns];
            }

            // Reset the array to zeros.
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    m_Grid[i][j] = 0;
                }
            }

            // Cache the dimensions and offset.
            m_Dimensions = dimensions;
            m_Center = center;
        }

        // Add a data point to the heat map.
        public void Add(Vector2 position) {
            int j = GetCol(position.x);
            int i = GetRow(position.y);
            m_Grid[i][j] += 1;
        }

        private int GetCol(float x) {
            float ratio = (x - m_Center.x) / m_Dimensions.x;
            ratio += 0.5f;
            ratio = ratio < 0 ? 0 : ratio > 1 ? 1 : ratio;
            return (int)Mathf.Floor((Cols-1) * ratio);
        }

        private int GetRow(float y) {
            float ratio = (y - m_Center.y) / m_Dimensions.y;
            ratio += 0.5f;
            ratio = ratio < 0 ? 0 : ratio > 1 ? 1 : ratio;
            return (int)Mathf.Floor((Rows-1) * ratio);
        }

    }
}