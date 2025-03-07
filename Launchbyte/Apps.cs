using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Launchbyte.Properties;

namespace Launchbyte;

public class Apps : Form
{
	private bool isDragging = false;

	private Point offset;

	private IContainer components = null;

	private Panel header;

	private Label exit;

	private Label minimize;

	private Label HomeBtn;

	private Label GamesBtn;

	private Label Spotify;

	private Panel panelApps;

	public Apps()
	{
		InitializeComponent();
	}

	private void header_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			isDragging = true;
			offset = new Point(e.X, e.Y);
		}
	}

	private void header_MouseMove(object sender, MouseEventArgs e)
	{
		if (isDragging)
		{
			Point newLocation = PointToScreen(new Point(e.X, e.Y));
			newLocation.Offset(-offset.X, -offset.Y);
			base.Location = newLocation;
		}
	}

	private void header_MouseUp(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			isDragging = false;
		}
	}

	private void exit_Click(object sender, EventArgs e)
	{
		Application.Exit();
	}

	private void minimize_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private void HomeBtn_Click(object sender, EventArgs e)
	{
		Main mainForm = new Main();
		mainForm.StartPosition = FormStartPosition.Manual;
		mainForm.Location = base.Location;
		mainForm.Size = base.Size;
		mainForm.Show();
		Hide();
		mainForm.FormClosed += delegate
		{
			Show();
		};
	}

	private void GamesBtn_Click(object sender, EventArgs e)
	{
		Games gamesForm = new Games();
		gamesForm.StartPosition = FormStartPosition.Manual;
		gamesForm.Location = base.Location;
		gamesForm.Size = base.Size;
		gamesForm.Show();
		Hide();
		gamesForm.FormClosed += delegate
		{
			Show();
		};
	}

	private void Spotify_Click(object sender, EventArgs e)
	{
		try
		{
			ProcessStartInfo psi = new ProcessStartInfo
			{
				FileName = "powershell",
				Arguments = "-NoProfile -Command \"& {[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; Invoke-Expression ((New-Object Net.WebClient).DownloadString('https://spotx-official.github.io/run.ps1'))}\"",
				Verb = "runas",
				UseShellExecute = true,
				WindowStyle = ProcessWindowStyle.Normal
			};
			Process.Start(psi);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Failed to execute the PowerShell script: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launchbyte.Apps));
		this.header = new System.Windows.Forms.Panel();
		this.GamesBtn = new System.Windows.Forms.Label();
		this.HomeBtn = new System.Windows.Forms.Label();
		this.minimize = new System.Windows.Forms.Label();
		this.exit = new System.Windows.Forms.Label();
		this.Spotify = new System.Windows.Forms.Label();
		this.panelApps = new System.Windows.Forms.Panel();
		this.header.SuspendLayout();
		this.panelApps.SuspendLayout();
		base.SuspendLayout();
		this.header.BackgroundImage = Launchbyte.Properties.Resources.header2;
		this.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.header.Controls.Add(this.GamesBtn);
		this.header.Controls.Add(this.HomeBtn);
		this.header.Controls.Add(this.minimize);
		this.header.Controls.Add(this.exit);
		this.header.ForeColor = System.Drawing.Color.Transparent;
		this.header.Location = new System.Drawing.Point(0, 0);
		this.header.Name = "header";
		this.header.Size = new System.Drawing.Size(680, 43);
		this.header.TabIndex = 1;
		this.header.MouseDown += new System.Windows.Forms.MouseEventHandler(header_MouseDown);
		this.header.MouseMove += new System.Windows.Forms.MouseEventHandler(header_MouseMove);
		this.header.MouseUp += new System.Windows.Forms.MouseEventHandler(header_MouseUp);
		this.GamesBtn.AutoSize = true;
		this.GamesBtn.BackColor = System.Drawing.Color.Transparent;
		this.GamesBtn.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.GamesBtn.ForeColor = System.Drawing.Color.Transparent;
		this.GamesBtn.Location = new System.Drawing.Point(580, 26);
		this.GamesBtn.Name = "GamesBtn";
		this.GamesBtn.Size = new System.Drawing.Size(36, 8);
		this.GamesBtn.TabIndex = 10;
		this.GamesBtn.Text = "                ";
		this.GamesBtn.Click += new System.EventHandler(GamesBtn_Click);
		this.HomeBtn.AutoSize = true;
		this.HomeBtn.BackColor = System.Drawing.Color.Transparent;
		this.HomeBtn.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.HomeBtn.ForeColor = System.Drawing.Color.Transparent;
		this.HomeBtn.Location = new System.Drawing.Point(537, 26);
		this.HomeBtn.Name = "HomeBtn";
		this.HomeBtn.Size = new System.Drawing.Size(34, 8);
		this.HomeBtn.TabIndex = 6;
		this.HomeBtn.Text = "               ";
		this.HomeBtn.Click += new System.EventHandler(HomeBtn_Click);
		this.minimize.AutoSize = true;
		this.minimize.BackColor = System.Drawing.Color.Transparent;
		this.minimize.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.minimize.ForeColor = System.Drawing.Color.Transparent;
		this.minimize.Location = new System.Drawing.Point(637, 0);
		this.minimize.Name = "minimize";
		this.minimize.Size = new System.Drawing.Size(16, 8);
		this.minimize.TabIndex = 4;
		this.minimize.Text = "      ";
		this.minimize.Click += new System.EventHandler(minimize_Click);
		this.exit.AutoSize = true;
		this.exit.BackColor = System.Drawing.Color.Transparent;
		this.exit.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.exit.ForeColor = System.Drawing.Color.Transparent;
		this.exit.Location = new System.Drawing.Point(661, 0);
		this.exit.Name = "exit";
		this.exit.Size = new System.Drawing.Size(16, 8);
		this.exit.TabIndex = 3;
		this.exit.Text = "      ";
		this.exit.Click += new System.EventHandler(exit_Click);
		this.Spotify.BackColor = System.Drawing.Color.Transparent;
		this.Spotify.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.Spotify.ForeColor = System.Drawing.Color.Transparent;
		this.Spotify.Location = new System.Drawing.Point(29, 40);
		this.Spotify.Name = "Spotify";
		this.Spotify.Size = new System.Drawing.Size(72, 88);
		this.Spotify.TabIndex = 9;
		this.Spotify.Text = "             ";
		this.Spotify.Click += new System.EventHandler(Spotify_Click);
		this.panelApps.BackColor = System.Drawing.Color.Transparent;
		this.panelApps.BackgroundImage = Launchbyte.Properties.Resources.freepremiumstuff;
		this.panelApps.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.panelApps.Controls.Add(this.Spotify);
		this.panelApps.Location = new System.Drawing.Point(5, 54);
		this.panelApps.Name = "panelApps";
		this.panelApps.Size = new System.Drawing.Size(664, 136);
		this.panelApps.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		base.ClientSize = new System.Drawing.Size(680, 380);
		base.Controls.Add(this.panelApps);
		base.Controls.Add(this.header);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Apps";
		this.Text = "LB";
		this.header.ResumeLayout(false);
		this.header.PerformLayout();
		this.panelApps.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
