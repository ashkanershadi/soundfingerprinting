namespace SoundFingerprinting.SoundTools
{
    using System;
    using System.Windows.Forms;

    using SoundFingerprinting.Audio;
    using SoundFingerprinting.Audio.Bass;
    using SoundFingerprinting.Audio.NAudio;
    using SoundFingerprinting.Audio.NAudio.Play;
    using SoundFingerprinting.Builder;
    using SoundFingerprinting.FFT;
    using SoundFingerprinting.MinHash.Permutations;
    using SoundFingerprinting.SoundTools.BassResampler;
    using SoundFingerprinting.SoundTools.DbFiller;
    using SoundFingerprinting.SoundTools.DrawningTool;
    using SoundFingerprinting.SoundTools.FFMpegResampler;
    using SoundFingerprinting.SoundTools.FilePermutations;
    using SoundFingerprinting.SoundTools.Misc;
    using SoundFingerprinting.SoundTools.PermutationGenerator;
    using SoundFingerprinting.SoundTools.Properties;
    using SoundFingerprinting.SoundTools.QueryDb;
    using SoundFingerprinting.SoundTools.WaveletDecomposition;
    using SoundFingerprinting.SQL;

    public partial class WinMain : Form
    {
        private readonly IAudioService audioService;
        private readonly IModelService modelService;
        private readonly IPlayAudioFileService playAudioFileService;
        private readonly IFingerprintCommandBuilder fingerprintCommandBuilder;
        private readonly IQueryCommandBuilder queryCommandBuilder;
        private readonly ITagService tagService;
        private readonly IPermutationGeneratorService permutationGeneratorService;
        private readonly ISpectrumService spectrumService;
        private readonly IImageService imageService;

        public WinMain()
        {
            InitializeComponent();
            Icon = Resources.Sound;
            audioService = new NAudioService();
            modelService = new SqlModelService();
            playAudioFileService = new NAudioPlayAudioFileService();
            fingerprintCommandBuilder = new FingerprintCommandBuilder();
            queryCommandBuilder = new QueryCommandBuilder();
            tagService = new BassTagService();
            permutationGeneratorService = new PermutationGeneratorService();
            spectrumService = new SpectrumService();
            imageService = new ImageService();
        }

        private void FillDatabaseToolStripClick(object sender, EventArgs e)
        {
            WinDbFiller filler = new WinDbFiller(fingerprintCommandBuilder, audioService, tagService, modelService);
            filler.Show();
        }

        private void BtnFillDatabaseClick(object sender, EventArgs e)
        {
            FillDatabaseToolStripClick(sender, e);
        }

        private void BtnQueryDbClick(object sender, EventArgs e)
        {
            QueryDatabaseToolStripClick(sender, e);
        }

        private void QueryDatabaseToolStripClick(object sender, EventArgs e)
        {
            WinCheckHashBins queryDatabase = new WinCheckHashBins(
                queryCommandBuilder, tagService, modelService, audioService);
            queryDatabase.Show();
        }

        private void MinHashPermGeneratorToolStripClick(object sender, EventArgs e)
        {
            WinPermGenerator win = new WinPermGenerator(permutationGeneratorService);
            win.Show();
        }

        private void AudioToolToolStripMenuItemClick(object sender, EventArgs e)
        {
            WinDrawningTool win = new WinDrawningTool(audioService, fingerprintCommandBuilder, spectrumService);
            win.Show();
        }

        private void RandomPermutationToolStripMenuItemClick(object sender, EventArgs e)
        {
            WinFilePermutation win = new WinFilePermutation(permutationGeneratorService);
            win.ShowDialog();
        }

        private void FFMpegResamplerToolStripMenuItemClick(object sender, EventArgs e)
        {
            WinFfMpegResampler win = new WinFfMpegResampler(tagService, playAudioFileService);
            win.Show();
        }

        private void CloseToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void BassResamplerToolStripMenuItemClick(object sender, EventArgs e)
        {
            WinBassResampler win = new WinBassResampler(audioService);
            win.Show();
        }

        private void SimilarityCalculationToolStripMenuItemClick(object sender, EventArgs e)
        {
            WinMisc win = new WinMisc(fingerprintCommandBuilder, audioService);
            win.Show();
        }

        private void WaveletDecompositionToolStripMenuItemClick(object sender, EventArgs e)
        {
            WinHaarWavelet win = new WinHaarWavelet(imageService);
            win.Show();
        }
    }
}