namespace HazzardGameworks.ProjectInfrastructure.Game
{
    public struct GameSettingsData
    {
        //This class is a struct to hold the game-wide settings values.
        public float MasterVolume;
        public float MusicVolume;

        public GameSettingsData(float masterVolume, float musicVolume)
        {
            MasterVolume = masterVolume;
            MusicVolume = musicVolume;
        }
    }
}
