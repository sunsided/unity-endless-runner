using JetBrains.Annotations;

namespace Project.Scripts
{
    /// <summary>
    /// The keys for <see cref="UnityEngine.PlayerPrefs"/> values.
    /// </summary>
    internal static class PlayerPrefKeys
    {
        // TODO: Might as well make these actual properties.

        [NotNull]
        public static readonly string SoundVolume = "sfxVolume";

        [NotNull]
        public static readonly string MusicVolume = "musicVolume";

        [NotNull]
        public static readonly string Lives = "lives";

        [NotNull]
        public static readonly string Score = "score";

        [NotNull]
        public static readonly string LastScore = "lastScore";

        [NotNull]
        public static readonly string Highscore = "highscore";
    }
}
