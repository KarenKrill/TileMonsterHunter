using UnityEngine;

namespace TileMonsterHunter
{
    using Abstractions;

    public class PlayerProfileProvider : IPlayerProfileProvider
    {
        public PlayerProfile CurrentProfile => _currentProfile;
        public PlayerProfileProvider()
        {
            LoadData();
            Application.quitting += OnApplicationQuitting;
        }

        private PlayerProfile _currentProfile;
        private void OnApplicationQuitting()
        {
            SaveData();
        }
        private void SaveData()
        {
            var serializedPlayerProfile = JsonUtility.ToJson(_currentProfile);
            PlayerPrefs.SetString("PlayerProfileData", serializedPlayerProfile);
        }
        private void LoadData()
        {
            var serializedPlayerProfile = PlayerPrefs.GetString("PlayerProfileData", string.Empty);
            if (string.IsNullOrEmpty(serializedPlayerProfile))
            {
                _currentProfile = new();
            }
            else
            {
                _currentProfile = JsonUtility.FromJson<PlayerProfile>(serializedPlayerProfile);
            }
        }
    }
}
