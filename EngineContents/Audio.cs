using System;

namespace Consyl_Engine.EngineContents
{
    class Audio
    {
        string fileName; // Stores the Sound File's name
        static NAudio.Wave.BlockAlignReductionStream stream = null; // Sound Data
        static NAudio.Wave.DirectSoundOut output = null; // The Output Sound

        public Audio(string _fileName) // constructor for inputting the sound file's name (Supports only mp3 and wav files)
        {
            fileName = _fileName;
        }

        public void PlaySound() // Plays sound from start
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

        public void StopSound() // Stops the sound
        {
            if (output != null)
            {
                output.Stop();
            }
        }

        public void PauseSound() // Pauses the sound
        {
            if (output != null)
            {
                output.Pause();
            }
        }
        public void UnpauseSound() // Un-pauses the sound
        {
            if (output != null)
            {
                output.Play();
            }
        }
    }
}

