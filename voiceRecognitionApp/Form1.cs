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
        DateTime timenow = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            recognizer.SetInputToDefaultAudioDevice();
            recognizer.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines("CommendsDF.txt")))));
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Defult_speechrecognized);
            recognizer.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(recognizer_speechrecognized);
            recognizer.RecognizeAsync(RecognizeMode.Multiple);

            startlistening.SetInputToDefaultAudioDevice();
            startlistening.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines("CommendsDF.txt")))));
            startlistening.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(startlistening_speechrecognized);

        }

        private void Defult_speechrecognized(object sender, SpeechRecognizedEventArgs e)
        {
            int ranNum;
            string speech = e.Result.Text;

            if (speech == "Hello")
            {
                Tino.SpeakAsync("Hello, I am here how can i help?");
            }
            if (speech == "How are you")
            {
                Tino.SpeakAsync("I am working normaly");
            }
            if (speech == "What time is it")
            {
                Tino.SpeakAsync(DateTime.Now.ToString("h mm tt"));
            }
            if (speech =="stop listening")
            {
                Tino.SpeakAsyncCancelAll();
                ranNum = rnd.Next(1 , 2);
                if (ranNum == 1)
                {
                    Tino.SpeakAsync("Yes sir");
                }
                
                if (ranNum == 2)
                {
                    Tino.SpeakAsync("I am sorry i will be quiet");
                }

            }
            if (speech=="Start Listening")
            {
                Tino.SpeakAsync("If you need me just ask");
                recognizer.RecognizeAsyncCancel();
                startlistening.RecognizeAsync(RecognizeMode.Multiple);
            }
            if (speech=="show commands")
            {
                string[] commands = (File.ReadAllLines(@"CommendsDF.txt"));
                lstCommands.Items.Clear();
                lstCommands.SelectionMode = SelectionMode.None;
                lstCommands.Visible = true;
                foreach (string command in commands)
                {
                    lstCommands.Items.Add(command);
                }

            }
            if (speech== "Hide commands")
            {
                lstCommands.Visible=false;
            }

        }

        private void recognizer_speechrecognized(object sender, SpeechDetectedEventArgs e)
        {
            Rectimeout = 0;
        }

        private void startlistening_speechrecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            if (speech == "Wake up")
            {
                startlistening.RecognizeAsyncCancel();
                Tino.SpeakAsync("Yes , I am here");
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

        private void tmrspeek_Tick(object sender, EventArgs e)
        {
            if(Rectimeout == 10)
            {
                recognizer.RecognizeAsyncCancel();
            }
            else if(Rectimeout == 11)
            {
                tmrspeek.Stop();
                startlistening.RecognizeAsync(RecognizeMode.Multiple);
                Rectimeout = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tino.SpeakAsync("Hey, my name is Tino");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
