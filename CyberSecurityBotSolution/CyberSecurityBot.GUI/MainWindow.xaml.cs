using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;
using CyberSecurityBot.Core; // This connects to your shared library!

namespace CyberSecurityBot.GUI
{
    public partial class MainWindow : Window
    {
        // Variables to hold our shared objects
        private SentimentAnalyzer _sentimentAnalyzer;
        private MemoryManager _memoryManager;
        private ResponseManager _responseManager;
        private ChatBot _chatBot;
        private string _lastTopic = "";

        public MainWindow()
        {
            InitializeComponent();
            InitializeBot();
            LoadAsciiArt();
            PlayGreeting(); // Play audio on startup
        }

        private void InitializeBot()
        {
            // Initialize from the Core Library
            _sentimentAnalyzer = new SentimentAnalyzer();
            _memoryManager = new MemoryManager();
            _responseManager = new ResponseManager();
            _chatBot = new ChatBot(_responseManager, _sentimentAnalyzer, _memoryManager);

            AddBotMessage("Initializing system...");
            AddBotMessage("Hello! Welcome to the Cybersecurity Awareness Bot. What is your name?");
        }

        private void LoadAsciiArt()
        {
            string art = @"
   ____            _     _             _     _            
  / ___|___  _ __ | | __| |_ __   __ _| |__ | | ___ _ __  
 | |   / _ \| '_ \| |/ _` | '_ \ / _` | '_ \| |/ _ \ '__| 
 | |__| (_) | | | | | (_| | | | | (_| | |_) | |  __/ |    
  \____\___/|_| |_|_|\__,_|_| |_|\__,_|_.__/|_|\___|_|    
                                                          
   ____  _                    _   _                       
  / ___|| |_ __ _ _ __   __ _| | | |_   _ _ __ ___  _ __  
  \___ \| __/ _` | '_ \ / _` | | | | | | | '_ ` _ \| '_ \ 
   ___) | || (_| | | | | (_| | | | | |_| | | | | | | |_) |
  |____/ \__\__,_|_| |_|\__,_|_| |_|\__,_|_| |_| |_| .__/ 
                                                   |_|    
";
            txtAscii.Text = art;
        }

        private void PlayGreeting()
        {
            try
            {
                // Plays the greeting.wav file
                var player = new System.Media.SoundPlayer("greeting.wav");
                player.Play();
            }
            catch { }
        }

        private async void ProcessInput(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput)) return;

            // Add user message to UI
            AddUserMessage(userInput);
            txtInput.Clear();

            // 1. Detect Sentiment
            string sentiment = _sentimentAnalyzer.DetectSentiment(userInput);

            // 2. Process logic from Core Library
            string response = _chatBot.ProcessInput(userInput, sentiment, out _lastTopic);

            // 3. Format response based on sentiment
            if (sentiment != "neutral")
            {
                string empathy = _sentimentAnalyzer.GetEmpatheticResponse(sentiment);
                AddBotMessage($"{empathy}\n{response}");
            }
            else
            {
                AddBotMessage(response);
            }
        }

        private void AddBotMessage(string text)
        {
            AddMessageToChat("Bot", text, new SolidColorBrush(Color.FromRgb(0, 212, 255)));
        }

        private void AddUserMessage(string text)
        {
            AddMessageToChat("You", text, new SolidColorBrush(Color.FromRgb(0, 255, 0)));
        }

        private void AddMessageToChat(string sender, string text, Brush color)
        {
            var panel = new StackPanel { Margin = new Thickness(0, 5, 0, 5) };

            var senderText = new TextBlock
            {
                Text = sender,
                FontWeight = FontWeights.Bold,
                Foreground = color
            };

            var msgText = new TextBlock
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.White,
                Margin = new Thickness(5, 2, 0, 0)
            };

            panel.Children.Add(senderText);
            panel.Children.Add(msgText);
            chatPanel.Children.Add(panel);
            scrollChat.ScrollToEnd();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            ProcessInput(txtInput.Text);
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessInput(txtInput.Text);
            }
        }
    }
}