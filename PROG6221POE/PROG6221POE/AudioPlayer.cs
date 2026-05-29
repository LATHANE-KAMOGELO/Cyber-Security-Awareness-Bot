using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace PROG6221POE
{
    public static class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                string filePath = Path.Combine(Application.StartupPath, "C:\\Users\\shong\\Downloads\\PROG6221POE\\PROG6221POE\\PROG6221POE\\greeting.wav");

                if (!File.Exists(filePath))
                {
                    MessageBox.Show(
                        
                        Application.StartupPath);
                    return;
                }

                SoundPlayer player = new SoundPlayer(filePath);
                player.Load();
                player.Play();
            }
            catch (Exception ex)
            {
              
            }
        }
    }
}