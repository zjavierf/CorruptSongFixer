using System;
using System.IO;
using IPA.Utilities;

namespace CorruptSongFixer
{
    public static class SongDeleter
    {
        public static void DeleteCorruptSongs()
        {
            try
            {
                string customLevelsPath = Path.Combine(UnityGame.UserDataPath, "..", "Beat Saber_Data", "CustomLevels");

                CleanDirectory(customLevelsPath);
            }
            catch (Exception ex)
            {
                Plugin.Log?.Error($"Error while deleting corrupt song folders: {ex}");
            }
        }

        private static void CleanDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Plugin.Log?.Warn($"CustomLevels directory not found at: {directoryPath}");
                return;
            }

            string[] subDirs = Directory.GetDirectories(directoryPath);
            foreach (string dir in subDirs)
            {
                string dirName = Path.GetFileName(dir);

                bool hasInfoFile = File.Exists(Path.Combine(dir, "Info.dat")) || 
                                   File.Exists(Path.Combine(dir, "info.dat"));

                if (!hasInfoFile)
                {
                    try
                    {
                        Directory.Delete(dir, true);
                        Plugin.Log?.Info($"Deleted corrupt song folder missing Info.dat: {dirName}");
                    }
                    catch (Exception ex)
                    {
                        Plugin.Log?.Error($"Failed to delete folder {dir}: {ex.Message}");
                    }
                }
            }
        }
    }
}