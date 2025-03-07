using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Launchbyte.Properties;

namespace Launchbyte;

public class RDR2 : Form
{
	private bool isDragging = false;

	private Point offset;

	private IContainer components = null;

	private Panel header;

	private Label exit;

	private Label minimize;

	private Label HomeBtn;

	private Label AppsForm;

	private Label GamesBtn;

	private Panel GamePanel;

	private Label AddToSteamBtn;

	private Label LaunchGameBtn;

	private Label BypassGameBtn;

	public RDR2()
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

	private void Apps_Click(object sender, EventArgs e)
	{
		Apps appsForm = new Apps();
		appsForm.StartPosition = FormStartPosition.Manual;
		appsForm.Location = base.Location;
		appsForm.Size = base.Size;
		appsForm.Show();
		Hide();
		appsForm.FormClosed += delegate
		{
			Show();
		};
	}

	private void BypassGameBtn_Click(object sender, EventArgs e)
	{
		using FolderBrowserDialog folderDialog = new FolderBrowserDialog();
		folderDialog.Description = "Select RDR2 folder";
		if (folderDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		string selectedPath = folderDialog.SelectedPath;
		string gameExePath = Path.Combine(selectedPath, "RDR2.exe");
		if (File.Exists(gameExePath))
		{
			MessageBox.Show(selectedPath + " probably has game installed!", "Game Found", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show(selectedPath + " probably doesn't have game installed!", "Game Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		string tempFolder = Path.GetTempPath();
		string downloadedFilePath = Path.Combine(tempFolder, "rdr2bypass.exe");
		string downloadUrl = "https://github.com/uxqc/L4unch3rH0sting/raw/main/rdr2bypass.exe";
		try
		{
			using WebClient client = new WebClient();
			client.DownloadFile(downloadUrl, downloadedFilePath);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Failed to download rdr2bypass.exe: " + ex.Message, "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		ProcessStartInfo psi = new ProcessStartInfo
		{
			FileName = downloadedFilePath,
			Arguments = "\"" + selectedPath + "\"",
			UseShellExecute = false,
			CreateNoWindow = false
		};
		try
		{
			Process process = Process.Start(psi);
			if (process != null)
			{
				process.WaitForExit();
				process.Dispose();
			}
			if (File.Exists(downloadedFilePath))
			{
				File.Delete(downloadedFilePath);
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show("Failed to execute or delete gtavbypass.exe: " + ex2.Message, "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void AddToSteamBtn_Click(object sender, EventArgs e)
	{
		string steamToolsPath = "C:\\Program Files (x86)\\Steam\\config\\stUI\\Steamtools.exe";
		if (!File.Exists(steamToolsPath))
		{
			SteamTools steamToolsForm = new SteamTools();
			steamToolsForm.ShowDialog();
			return;
		}
		Process.Start(steamToolsPath);
		string depotcachePath = "C:\\Program Files (x86)\\Steam\\config\\depotcache";
		Directory.CreateDirectory(depotcachePath);
		string[] manifestUrls = new string[7] { "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174181_4757128409362785099.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174182_2258488483491377476.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174183_4785346550216140531.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174184_2698939236196220127.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174185_8173342615013177810.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174186_357926288062992323.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1899671_2485572587613409384.manifest" };
		using (WebClient client = new WebClient())
		{
			string[] array = manifestUrls;
			foreach (string url in array)
			{
				try
				{
					string fileName = Path.GetFileName(url);
					string destPath = Path.Combine(depotcachePath, fileName);
					client.DownloadFile(url, destPath);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error while downloading " + url + ": " + ex.Message);
				}
			}
		}
		string stpluginPath = "C:\\Program Files (x86)\\Steam\\config\\stplug-in";
		Directory.CreateDirectory(stpluginPath);
		string stFileUrl = "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174180.st";
		string stFilePath = Path.Combine(stpluginPath, "1174180.st");
		using (WebClient client2 = new WebClient())
		{
			try
			{
				client2.DownloadFile(stFileUrl, stFilePath);
			}
			catch (Exception ex2)
			{
				MessageBox.Show("Error while downloading .st file: " + ex2.Message);
			}
		}
		Process[] steamProcesses = Process.GetProcessesByName("steam");
		Process[] array2 = steamProcesses;
		foreach (Process process in array2)
		{
			process.Kill();
			process.WaitForExit();
		}
		string steamExePath = "C:\\Program Files (x86)\\Steam\\steam.exe";
		Process.Start(steamExePath);
		MessageBox.Show("Game has been added successfully!");
	}

	private void LaunchGameBtn_Click(object sender, EventArgs e)
	{
		try
		{
			if (Process.GetProcessesByName("Steam").Any())
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = "steam://rungameid/1174180",
					UseShellExecute = true
				});
				return;
			}
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "RDR2 Executable|Launcher.exe",
				Title = "Select RDR 2 Installation",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
				FileName = "Launcher.exe"
			};
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string gamePath = openFileDialog.FileName;
				if (File.Exists(gamePath))
				{
					Process.Start(new ProcessStartInfo
					{
						FileName = gamePath,
						UseShellExecute = true,
						WorkingDirectory = Path.GetDirectoryName(gamePath)
					});
				}
				else
				{
					MessageBox.Show("Could not find Launcher.exe at the specified location!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Failed to launch game: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launchbyte.RDR2));
		this.header = new System.Windows.Forms.Panel();
		this.AppsForm = new System.Windows.Forms.Label();
		this.GamesBtn = new System.Windows.Forms.Label();
		this.HomeBtn = new System.Windows.Forms.Label();
		this.minimize = new System.Windows.Forms.Label();
		this.exit = new System.Windows.Forms.Label();
		this.GamePanel = new System.Windows.Forms.Panel();
		this.BypassGameBtn = new System.Windows.Forms.Label();
		this.AddToSteamBtn = new System.Windows.Forms.Label();
		this.LaunchGameBtn = new System.Windows.Forms.Label();
		this.header.SuspendLayout();
		this.GamePanel.SuspendLayout();
		base.SuspendLayout();
		this.header.BackgroundImage = Launchbyte.Properties.Resources.header2;
		this.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.header.Controls.Add(this.AppsForm);
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
		this.AppsForm.AutoSize = true;
		this.AppsForm.BackColor = System.Drawing.Color.Transparent;
		this.AppsForm.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.AppsForm.ForeColor = System.Drawing.Color.Transparent;
		this.AppsForm.Location = new System.Drawing.Point(623, 26);
		this.AppsForm.Name = "AppsForm";
		this.AppsForm.Size = new System.Drawing.Size(30, 8);
		this.AppsForm.TabIndex = 8;
		this.AppsForm.Text = "             ";
		this.AppsForm.Click += new System.EventHandler(Apps_Click);
		this.GamesBtn.AutoSize = true;
		this.GamesBtn.BackColor = System.Drawing.Color.Transparent;
		this.GamesBtn.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.GamesBtn.ForeColor = System.Drawing.Color.Transparent;
		this.GamesBtn.Location = new System.Drawing.Point(580, 26);
		this.GamesBtn.Name = "GamesBtn";
		this.GamesBtn.Size = new System.Drawing.Size(36, 8);
		this.GamesBtn.TabIndex = 7;
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
		this.GamePanel.BackColor = System.Drawing.Color.Transparent;
		this.GamePanel.BackgroundImage = Launchbyte.Properties.Resources.rdr2;
		this.GamePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.GamePanel.Controls.Add(this.BypassGameBtn);
		this.GamePanel.Controls.Add(this.AddToSteamBtn);
		this.GamePanel.Controls.Add(this.LaunchGameBtn);
		this.GamePanel.Location = new System.Drawing.Point(18, 58);
		this.GamePanel.Name = "GamePanel";
		this.GamePanel.Size = new System.Drawing.Size(644, 306);
		this.GamePanel.TabIndex = 2;
		this.BypassGameBtn.BackColor = System.Drawing.Color.Transparent;
		this.BypassGameBtn.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.BypassGameBtn.ForeColor = System.Drawing.Color.Transparent;
		this.BypassGameBtn.Location = new System.Drawing.Point(562, 225);
		this.BypassGameBtn.Name = "BypassGameBtn";
		this.BypassGameBtn.Size = new System.Drawing.Size(34, 33);
		this.BypassGameBtn.TabIndex = 11;
		this.BypassGameBtn.Text = "             ";
		this.BypassGameBtn.Click += new System.EventHandler(BypassGameBtn_Click);
		this.AddToSteamBtn.BackColor = System.Drawing.Color.Transparent;
		this.AddToSteamBtn.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.AddToSteamBtn.ForeColor = System.Drawing.Color.Transparent;
		this.AddToSteamBtn.Location = new System.Drawing.Point(416, 225);
		this.AddToSteamBtn.Name = "AddToSteamBtn";
		this.AddToSteamBtn.Size = new System.Drawing.Size(140, 33);
		this.AddToSteamBtn.TabIndex = 10;
		this.AddToSteamBtn.Text = "             ";
		this.AddToSteamBtn.Click += new System.EventHandler(AddToSteamBtn_Click);
		this.LaunchGameBtn.BackColor = System.Drawing.Color.Transparent;
		this.LaunchGameBtn.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.LaunchGameBtn.ForeColor = System.Drawing.Color.Transparent;
		this.LaunchGameBtn.Location = new System.Drawing.Point(411, 181);
		this.LaunchGameBtn.Name = "LaunchGameBtn";
		this.LaunchGameBtn.Size = new System.Drawing.Size(185, 35);
		this.LaunchGameBtn.TabIndex = 9;
		this.LaunchGameBtn.Text = "             ";
		this.LaunchGameBtn.Click += new System.EventHandler(LaunchGameBtn_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		base.ClientSize = new System.Drawing.Size(680, 380);
		base.Controls.Add(this.GamePanel);
		base.Controls.Add(this.header);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "RDR2";
		this.Text = "LB";
		this.header.ResumeLayout(false);
		this.header.PerformLayout();
		this.GamePanel.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
