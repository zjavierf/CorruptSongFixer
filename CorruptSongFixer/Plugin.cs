using IPA;

namespace CorruptSongFixer
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static IPA.Logging.Logger Log { get; set; }

        [Init]
        public Plugin(IPA.Logging.Logger logger)
        {
            Log = logger;
            Log.Debug("CorruptSongFixer initialized.");
        }

        [OnStart]
        public void OnStart()
        {
            SongDeleter.DeleteCorruptSongs();
        }

        [OnExit]
        public void OnExit()
        {
        }
    }
}