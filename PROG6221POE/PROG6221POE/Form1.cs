using PROG6221POE;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PROG6221POE
{
    public partial class Form1 : Form
    {
        private ChatbotEngine? bot;

        private Label? lblTitle;
        private Label? lblSubtitle;
        private Label? lblName;
        private TextBox? txtName;
        private TextBox? txtInput;
        private Button? btnStart;
        private Button? btnSend;
        private RichTextBox? rtbChat;

        public Form1()
        {
            InitializeComponent();
            BuildInterface();
        }

        private void BuildInterface()
        {
            Text = "CyberSecure Bot";
            Size = new Size(980, 730);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(215, 245, 240);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            lblTitle = new Label();
            lblTitle.Text = "CyberSecure Bot";
            lblTitle.ForeColor = Color.FromArgb(0, 95, 110);
            lblTitle.Font = new Font("Bahnschrift SemiBold", 30, FontStyle.Bold);
            lblTitle.Location = new Point(35, 20);
            lblTitle.AutoSize = true;
            Controls.Add(lblTitle);

            lblSubtitle = new Label();
            lblSubtitle.Text = "Online Safety Guidance Interface";
            lblSubtitle.ForeColor = Color.FromArgb(40, 130, 115);
            lblSubtitle.Font = new Font("Bahnschrift", 11);
            lblSubtitle.Location = new Point(40, 76);
            lblSubtitle.AutoSize = true;
            Controls.Add(lblSubtitle);

            lblName = new Label();
            lblName.Text = "USER";
            lblName.ForeColor = Color.FromArgb(0, 95, 110);
            lblName.Font = new Font("Bahnschrift", 10, FontStyle.Bold);
            lblName.Location = new Point(40, 118);
            lblName.AutoSize = true;
            Controls.Add(lblName);

            txtName = new TextBox();
            txtName.Location = new Point(105, 114);
            txtName.Size = new Size(300, 35);
            txtName.Font = new Font("Bahnschrift", 11);
            txtName.BackColor = Color.White;
            txtName.ForeColor = Color.Gray;
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Text = "Enter your name...";

            txtName.Enter += (s, e) =>
            {
                if (txtName.Text == "Enter your name...")
                {
                    txtName.Text = "";
                    txtName.ForeColor = Color.Black;
                }
            };

            txtName.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    txtName.Text = "Enter your name...";
                    txtName.ForeColor = Color.Gray;
                }
            };

            Controls.Add(txtName);

            btnStart = new Button();
            btnStart.Text = "CONNECT";
            btnStart.Location = new Point(425, 111);
            btnStart.Size = new Size(150, 42);
            btnStart.BackColor = Color.FromArgb(0, 145, 135);
            btnStart.ForeColor = Color.White;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Font = new Font("Bahnschrift", 10, FontStyle.Bold);
            btnStart.Cursor = Cursors.Hand;
            btnStart.Click += BtnStart_Click;
            Controls.Add(btnStart);

            rtbChat = new RichTextBox();
            rtbChat.Location = new Point(40, 185);
            rtbChat.Size = new Size(890, 395);
            rtbChat.ReadOnly = true;
            rtbChat.BackColor = Color.FromArgb(235, 255, 250);
            rtbChat.ForeColor = Color.FromArgb(0, 75, 90);
            rtbChat.BorderStyle = BorderStyle.FixedSingle;
            rtbChat.Font = new Font("Bahnschrift", 10);
            Controls.Add(rtbChat);

            txtInput = new TextBox();
            txtInput.Location = new Point(40, 615);
            txtInput.Size = new Size(730, 40);
            txtInput.Font = new Font("Bahnschrift", 11);
            txtInput.BackColor = Color.White;
            txtInput.ForeColor = Color.Black;
            txtInput.BorderStyle = BorderStyle.FixedSingle;
            txtInput.Enabled = false;
            txtInput.KeyDown += TxtInput_KeyDown;
            Controls.Add(txtInput);

            btnSend = new Button();
            btnSend.Text = "SEND";
            btnSend.Location = new Point(790, 611);
            btnSend.Size = new Size(140, 42);
            btnSend.BackColor = Color.FromArgb(0, 145, 135);
            btnSend.ForeColor = Color.White;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.Font = new Font("Bahnschrift", 10, FontStyle.Bold);
            btnSend.Cursor = Cursors.Hand;
            btnSend.Enabled = false;
            btnSend.Click += BtnSend_Click;
            Controls.Add(btnSend);
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            string name = txtName!.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || name == "Enter your name...")
            {
                name = "Friend";
            }

            bot = new ChatbotEngine(name);
            rtbChat!.Clear();

            AddBotMessage(bot.GetAsciiArt());
            AddBotMessage("Connection opened successfully.");
            AddBotMessage("Welcome, " + name + ".");
            AddBotMessage("Ask about phishing, passwords, scams, privacy, safe browsing, public Wi-Fi, or 2FA.");
            AddBotMessage("Try: 'I am curious about safe browsing' or 'explain more about scams'.");

            AudioPlayer.PlayGreeting("C:\\Users\\shong\\Downloads\\PROG6221POE\\PROG6221POE\\PROG6221POE\\greeting.wav");

            txtInput!.Enabled = true;
            btnSend!.Enabled = true;
            txtInput.Focus();
        }

        private void BtnSend_Click(object? sender, EventArgs e)
        {
            ProcessUserInput();
        }

        private void TxtInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessUserInput();
                e.SuppressKeyPress = true;
            }
        }

        private void ProcessUserInput()
        {
            if (bot == null)
            {
                MessageBox.Show("Connect to the assistant first.");
                return;
            }

            string userInput = txtInput!.Text.Trim();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                AddBotMessage("Please enter a message.");
                return;
            }

            AddUserMessage(userInput);

            string response = bot.GetResponse(userInput);

            AddBotMessage(response);

            txtInput.Clear();
            txtInput.Focus();
        }

        private void AddUserMessage(string message)
        {
            rtbChat!.SelectionColor = Color.FromArgb(0, 115, 130);
            rtbChat.AppendText("YOU > " + message + Environment.NewLine + Environment.NewLine);
            rtbChat.SelectionColor = Color.FromArgb(0, 75, 90);
        }

        private void AddBotMessage(string message)
        {
            rtbChat!.SelectionColor = Color.FromArgb(0, 75, 90);
            rtbChat.AppendText("CYBERSECURITY > " + message + Environment.NewLine + Environment.NewLine);
            rtbChat.SelectionColor = Color.FromArgb(0, 75, 90);
            rtbChat.ScrollToCaret();
        }
    }
}