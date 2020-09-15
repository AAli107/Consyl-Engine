using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Consyl_Engine.EngineContents
{
    class Audio
    {
        
        string fileName;
        static NAudio.Wave.BlockAlignReductionStream stream = null;
        static NAudio.Wave.DirectSoundOut output = null;

        public Audio(string _fileName)
        {
            fileName = _fileName;
        }

        public void PlaySound()
        {
            if (fileName.EndsWith(".mp3"))
            {
                NAudio.Wave.WaveStream pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(new NAudio.Wave.Mp3FileReader(fileName));
                stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
            }
            else if (fileName.EndsWith(".wav"))
            {
                NAudio.Wave.WaveStream pcm = new NAudio.Wave.WaveChannel32(new NAudio.Wave.WaveFileReader(fileName));
                stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
            }
            else throw new InvalidOperationException("Not a correct audio file type.");

            if (output == null)
            {
                output = new NAudio.Wave.DirectSoundOut();
            }
            output.Init(stream);
            output.Play();
        }

        public void StopSound()
        {
            if (output != null)
            {
                output.Stop();
            }
        }

        public void PauseSound()
        {
            if (output != null)
            {
                output.Pause();
            }
        }
        public void UnpauseSound()
        {
            if (output != null)
            {
                output.Play();
            }
        }
    }
}

