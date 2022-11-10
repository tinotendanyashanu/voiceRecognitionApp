using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace voiceRecognitionApp
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
        SpeechSynthesizer Tino = new SpeechSynthesizer();   
        SpeechRecognitionEngine startlistening = new SpeechRecognitionEngine();
        Random rnd = new Random();
        int Rectimeout = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            recognizer.SetInputToDefaultAudioDevice();
            recognizer.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines("")))));
        }
    }
}
