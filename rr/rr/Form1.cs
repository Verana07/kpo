using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace VideoPlayerApp
{
    public partial class MainForm : Form
    {
        private HttpListener _listener;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;

        private void InitializeComponent()
        {
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // axWindowsMediaPlayer1
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(13, 13);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(560, 315);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Name = "MainForm";
            this.Text = "Video Player";
            this.Load += new System.EventHandler(this.MainForm_Load());
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
        }


        public MainForm()
        {
            InitializeComponent();
            StartServer();
    
        }

        private void StartServer()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:5000/");
            _listener.Start();

            Task.Run(() => ListenForRequests());
        }

        private async Task ListenForRequests()
        {
            while (_listener.IsListening)
            {
                var context = await _listener.GetContextAsync();
                var response = context.Response;
                if (context.Request.Url.AbsolutePath == "/play-video")
                {
                    PlayVideo();
                    response.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                }
                response.Close();
            }
        }

        private void PlayVideo()
        {
            string videoPath = @"C:\path\to\your\video.mp4"; //elérési útja
            var processStartInfo = new ProcessStartInfo
            {
                FileName = videoPath,
                UseShellExecute = true 
            };
            Process.Start(processStartInfo);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _listener.Stop();
        }
    }
}

