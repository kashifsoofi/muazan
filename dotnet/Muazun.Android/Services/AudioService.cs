using Android.Media;
using Muazun.Droid.Audio;
using Muazun.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioService))]
namespace Muazun.Droid.Audio
{
    public class AudioService : IAudioService
    {
        public void PlayAudioFile(string fileName)
        {
            var player = new MediaPlayer();
            var fd = global::Android.App.Application.Context.Assets.OpenFd(fileName);
            player.Prepared += (s, e) =>
            {
                player.Start();
            };
            player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
            player.Prepare();
        }
    }
}
