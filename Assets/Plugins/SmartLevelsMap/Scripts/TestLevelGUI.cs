using UnityEngine;

namespace Assets.Plugins.SmartLevelsMap.Scripts
{
    public class TestLevelGUI : MonoBehaviour
    {
        public int LevelNumber;
        public void OnGUI()
        {
            GUILayout.BeginVertical();

            if (GUILayout.Button("Complete with 1 star"))
            {
                LevelsMap.CompleteLevel(LevelNumber, 1);
                GoBack();
            }

            if (GUILayout.Button("Complete with 2 star"))
            {
                LevelsMap.CompleteLevel(LevelNumber, 2);
                GoBack();
            }

            if (GUILayout.Button("Complete with 3 star"))
            {
                LevelsMap.CompleteLevel(LevelNumber, 3);
                GoBack();
            }

            if (GUILayout.Button("Back"))
            {
                GoBack();
            }

            GUILayout.EndVertical();
        }

        private void GoBack()
        {
            Application.LoadLevel("demo");
        }
    }
}
