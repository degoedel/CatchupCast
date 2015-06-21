using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using PodCatchup.Events;
using PodCatchup.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PodCatchup.Services
{
  public class CSCoreStreamPlayer : IStreamPlayer
  {

    public volatile bool _play;

   public void StreamFromUrl(string surl, int starttime)
    {
      _play = true;
      Thread thread = new Thread(() => performPlay(surl, starttime));
      thread.Start();
    }

    public void PauseStream()
    {
      throw new NotImplementedException();
    }

    private ISoundOut GetSoundOut()
    {
      if (WasapiOut.IsSupportedOnCurrentPlatform)
        return new WasapiOut();
      else
        return new DirectSoundOut();
    }

    public void performPlay(String surl, int starttime)
    {
      IWaveSource soundsource = CodecFactory.Instance.GetCodec(new Uri(surl));
      using (ISoundOut soundOut = GetSoundOut())
      {
        soundOut.Initialize(soundsource);
        TimeSpan span = new TimeSpan(0, 0, 0, 0, starttime);
        soundsource.SetPosition(span);
        soundOut.Play();
        while (_play && (soundOut.PlaybackState != PlaybackState.Stopped))
        {
          Thread.Sleep(100);
          ApplicationService.Instance.EventAggregator.GetEvent<StreamProgressEvent>().Publish(soundsource.GetPosition());
        }
        soundOut.Stop();
        ApplicationService.Instance.EventAggregator.GetEvent<StreamCompletedEvent>().Publish(true);
      }
 
    }

  }
}
