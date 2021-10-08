﻿using Android.Media;
using Muazun.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioPlayer))]
namespace Muazun.Droid
{
    public class AudioPlayer : IAudioPlayer
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
