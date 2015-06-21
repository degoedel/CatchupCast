using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
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

   public void StreamFromUrl(string surl, int starttime)
    {
      IWaveSource soundsource = CodecFactory.Instance.GetCodec(new Uri(surl));
      using (ISoundOut soundOut = GetSoundOut())
      {
        //Tell the SoundOut which sound it has to play
        soundOut.Initialize(soundsource);
        TimeSpan span = new TimeSpan(0, 0, 0, 0, starttime);
        soundsource.SetPosition(span);
        //Play the sound
        soundOut.Play();

        Thread.Sleep(60000);

        //Stop the playback
        soundOut.Stop();
      }
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

  }
}
