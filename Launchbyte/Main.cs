using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Launchbyte.Properties;

namespace Launchbyte;

public class Main : Form
{
	public int broj = 1;

	private bool isDragging = false;

	private Point offset;

	private IContainer components = null;

	private Panel header;

	private Label exit;

	private Label minimize;

	private Panel gtavbanner;

	private Label GamesBtn;

	private Label GtaVSlide;

	private Label GtaSaSlide;

	private Label RDR2Slide;

	private Panel MainPanel;

	private PictureBox SteamDetect;

	private PictureBox steamToolsDetect;

	private Label label1;

	private Label DiscordServ;

	private Label YouTubeBtn;

	private Label Updates;

	private TextBox EnterID;

	private Label AddGame;

	private Label AddToSteam;

	private Label PlayGame;

	public Main()
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

	private void GtaVSlide_Click(object sender, EventArgs e)
	{
		broj = 1;
		gtavbanner.BackgroundImage = Resources.gtavbanner1;
	}

	private void RDR2Slide_Click(object sender, EventArgs e)
	{
		broj = 2;
		gtavbanner.BackgroundImage = Resources.rdr2baner;
	}

	private void GtaSaSlide_Click(object sender, EventArgs e)
	{
		broj = 3;
		gtavbanner.BackgroundImage = Resources.gtasabaner2;
	}

	private void Main_Load(object sender, EventArgs e)
	{
		string steamToolsPath = "C:\\Program Files (x86)\\Steam\\config\\stUI\\Steamtools.exe";
		string steamPath = "C:\\Program Files (x86)\\Steam\\steam.exe";
		if (File.Exists(steamToolsPath))
		{
			steamToolsDetect.BackgroundImage = Resources.y;
		}
		else
		{
			steamToolsDetect.BackgroundImage = Resources.x;
		}
		if (File.Exists(steamPath))
		{
			SteamDetect.BackgroundImage = Resources.y;
		}
		else
		{
			SteamDetect.BackgroundImage = Resources.x;
		}
	}

	private void label1_Click(object sender, EventArgs e)
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

	private void Updates_Click(object sender, EventArgs e)
	{
		string url = "https://github.com/onajlikezz/Launchbyte/releases";
		try
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = url,
				UseShellExecute = true
			});
		}
		catch (Exception ex)
		{
			MessageBox.Show("Failed to open the URL: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void YouTubeBtn_Click(object sender, EventArgs e)
	{
		string url = "https://www.youtube.com/@twovvq";
		try
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = url,
				UseShellExecute = true
			});
		}
		catch (Exception ex)
		{
			MessageBox.Show("Failed to open the URL: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void DiscordServ_Click(object sender, EventArgs e)
	{
		string url = "https://discord.gg/pBFaCQQVBV";
		try
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = url,
				UseShellExecute = true
			});
		}
		catch (Exception ex)
		{
			MessageBox.Show("Failed to open the URL: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private async void AddGame_Click(object sender, EventArgs e)
	{
		string steamToolsPath = "C:\\Program Files (x86)\\Steam\\config\\stUI\\Steamtools.exe";
		if (!File.Exists(steamToolsPath))
		{
			new SteamTools().Show();
			return;
		}
		string gameId = EnterID.Text.Trim();
		if (string.IsNullOrEmpty(gameId))
		{
			MessageBox.Show("Please enter a Game ID");
			return;
		}
		if (!gameId.All(char.IsDigit))
		{
			MessageBox.Show("Game ID must contain numbers only");
			return;
		}
		try
		{
			string tempPath = Path.Combine(Path.GetTempPath(), "Launchbyte");
			Directory.CreateDirectory(tempPath);
			string zipPath = Path.Combine(tempPath, gameId + ".zip");
			bool downloadSuccess = false;
			string primaryUrl = "https://furcate.eu/files/" + gameId + ".zip";
			string fallbackUrl = "https://cysaw.top/uploads/" + gameId + ".zip";
			try
			{
				using WebClient client = new WebClient();
				client.Headers.Add("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/133.0.0.0 Mobile Safari/537.36");
				client.Headers.Add("Referer", "https://furcate.eu/sites/generator.html");
				await client.DownloadFileTaskAsync(new Uri(primaryUrl), zipPath);
				downloadSuccess = true;
			}
			catch (Exception)
			{
				try
				{
					using WebClient client2 = new WebClient();
					client2.Headers.Add("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/133.0.0.0 Mobile Safari/537.36");
					client2.Headers.Add("Referer", "https://cysaw.top");
					await client2.DownloadFileTaskAsync(new Uri(fallbackUrl), zipPath);
					downloadSuccess = true;
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					throw new Exception("Both download attempts failed.", ex2);
				}
			}
			if (!downloadSuccess)
			{
				throw new Exception("Download unsuccessful");
			}
			ZipFile.ExtractToDirectory(zipPath, tempPath);
			string depotCache = "C:\\Program Files (x86)\\Steam\\config\\depotcache";
			string stPlugin = "C:\\Program Files (x86)\\Steam\\config\\stplug-in";
			Directory.CreateDirectory(depotCache);
			Directory.CreateDirectory(stPlugin);
			string[] files = Directory.GetFiles(tempPath);
			foreach (string file in files)
			{
				string fileName = Path.GetFileName(file);
				string extension = Path.GetExtension(file).ToLower();
				string destPath = null;
				if (extension == ".manifest")
				{
					destPath = Path.Combine(depotCache, fileName);
				}
				else if (extension == ".st" || extension == ".lua")
				{
					destPath = Path.Combine(stPlugin, fileName);
				}
				if (destPath != null)
				{
					if (File.Exists(destPath))
					{
						File.Delete(destPath);
					}
					File.Move(file, destPath);
				}
			}
			string luaFile = Path.Combine(stPlugin, gameId + ".lua");
			string stFile = Path.Combine(stPlugin, gameId + ".st");
			if (File.Exists(luaFile))
			{
				string luaPackaPath = Path.Combine(stPlugin, "luapacka.exe");
				if (!File.Exists(luaPackaPath))
				{
					MessageBox.Show("Executable not found: " + luaPackaPath);
					return;
				}
				Process.Start(new ProcessStartInfo
				{
					FileName = luaPackaPath,
					Arguments = $"\"{luaFile}\" \"{stFile}\"",
					WorkingDirectory = stPlugin,
					UseShellExecute = false
				})?.WaitForExit();
			}
			if (Process.GetProcessesByName("Steamtools").Length == 0)
			{
				Process.Start("C:\\Program Files (x86)\\Steam\\config\\stUI\\Steamtools.exe");
			}
			Process[] steamProcesses = Process.GetProcessesByName("Steam");
			if (steamProcesses.Length != 0)
			{
				Process[] array = steamProcesses;
				foreach (Process steam in array)
				{
					steam.Kill();
					steam.WaitForExit();
				}
				Process.Start("C:\\Program Files (x86)\\Steam\\Steam.exe");
			}
			MessageBox.Show("Game added successfully!");
		}
		catch (Exception ex)
		{
			Exception ex4 = ex;
			MessageBox.Show("Error: " + ex4.Message);
		}
		finally
		{
			string tempDir = Path.Combine(Path.GetTempPath(), "Launchbyte");
			if (Directory.Exists(tempDir))
			{
				Directory.Delete(tempDir, recursive: true);
			}
		}
	}

	private void AddToSteam_Click(object sender, EventArgs e)
	{
		string[] manifestUrls;
		string stFileUrl;
		string stFileName;
		if (broj == 1)
		{
			manifestUrls = new string[6] { "https://github.com/uxqc/m4n1f3st/raw/main/271590/1899671_424738606525145871.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/271590/271591_4148712806132596056.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/271590/271592_6895113908287539233.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/271590/271593_6533191187872171909.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/271590/271594_8496545153343461683.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/271590/271595_4898511214642125194.manifest" };
			stFileUrl = "https://github.com/uxqc/m4n1f3st/raw/main/271590/271590.st";
			stFileName = "271590.st";
		}
		else if (broj == 2)
		{
			manifestUrls = new string[7] { "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174181_4757128409362785099.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174182_2258488483491377476.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174183_4785346550216140531.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174184_2698939236196220127.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174185_8173342615013177810.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174186_357926288062992323.manifest", "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1899671_2485572587613409384.manifest" };
			stFileUrl = "https://github.com/uxqc/m4n1f3st/raw/main/1174180/1174180.st";
			stFileName = "1174180.st";
		}
		else
		{
			if (broj != 3)
			{
				MessageBox.Show("No game selected or unrecognized banner image.");
				return;
			}
			manifestUrls = new string[1] { "https://github.com/uxqc/m4n1f3st/raw/main/1547000/1547001_6814041191190748315.manifest" };
			stFileUrl = "https://github.com/uxqc/m4n1f3st/raw/main/1547000/1547000.st";
			stFileName = "1547000.st";
		}
		string steamToolsPath = "C:\\Program Files (x86)\\Steam\\config\\stUI\\Steamtools.exe";
		if (!File.Exists(steamToolsPath))
		{
			SteamTools steamToolsForm = new SteamTools();
			steamToolsForm.ShowDialog();
		}
		else
		{
			Process.Start(steamToolsPath);
			ProcessGameFiles(manifestUrls, stFileUrl, stFileName);
		}
	}

	private void ProcessGameFiles(string[] manifestUrls, string stFileUrl, string stFileName)
	{
		try
		{
			string depotcachePath = "C:\\Program Files (x86)\\Steam\\config\\depotcache";
			Directory.CreateDirectory(depotcachePath);
			using (WebClient client = new WebClient())
			{
				foreach (string url in manifestUrls)
				{
					string fileName = Path.GetFileName(url);
					string destPath = Path.Combine(depotcachePath, fileName);
					client.DownloadFile(url, destPath);
				}
			}
			string stpluginPath = "C:\\Program Files (x86)\\Steam\\config\\stplug-in";
			Directory.CreateDirectory(stpluginPath);
			string stFilePath = Path.Combine(stpluginPath, stFileName);
			using (WebClient client2 = new WebClient())
			{
				client2.DownloadFile(stFileUrl, stFilePath);
			}
			RestartSteam();
			MessageBox.Show("Game has been added successfully!");
		}
		catch (Exception ex)
		{
			MessageBox.Show("An error occurred: " + ex.Message);
		}
	}

	private void RestartSteam()
	{
		Process[] processesByName = Process.GetProcessesByName("steam");
		foreach (Process process in processesByName)
		{
			process.Kill();
			process.WaitForExit();
		}
		string steamExePath = "C:\\Program Files (x86)\\Steam\\steam.exe";
		Process.Start(steamExePath);
	}

	private void PlayGame_Click(object sender, EventArgs e)
	{
		try
		{
			string steamGameId = null;
			string executableName = null;
			string gameTitle = null;
			string fileFilter = null;
			if (broj == 1)
			{
				steamGameId = "271590";
				executableName = "PlayGTAV.exe";
				gameTitle = "GTA V";
				fileFilter = "GTA V Executable|PlayGTAV.exe";
			}
			else if (broj == 2)
			{
				steamGameId = "1174180";
				executableName = "RDR2.exe";
				gameTitle = "Red Dead Redemption 2";
				fileFilter = "RDR2 Executable|RDR2.exe";
			}
			else
			{
				if (broj != 3)
				{
					MessageBox.Show("No game selected or unrecognized banner image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				steamGameId = "1547000";
				executableName = "gta-sa.exe";
				gameTitle = "GTA San Andreas";
				fileFilter = "GTA SA Executable|gta-sa.exe";
			}
			if (Process.GetProcessesByName("Steam").Length != 0)
			{
				LaunchThroughSteam(steamGameId);
			}
			else
			{
				LaunchManually(gameTitle, executableName, fileFilter);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Failed to launch game: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void LaunchThroughSteam(string steamGameId)
	{
		Process.Start(new ProcessStartInfo
		{
			FileName = "steam://rungameid/" + steamGameId,
			UseShellExecute = true
		});
	}

	private void LaunchManually(string gameTitle, string executableName, string fileFilter)
	{
		using OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = fileFilter;
		openFileDialog.Title = "Select " + gameTitle + " Installation";
		openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
		openFileDialog.FileName = executableName;
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
				MessageBox.Show("Could not find " + executableName + " at the specified location!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launchbyte.Main));
		this.header = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.GamesBtn = new System.Windows.Forms.Label();
		this.minimize = new System.Windows.Forms.Label();
		this.exit = new System.Windows.Forms.Label();
		this.gtavbanner = new System.Windows.Forms.Panel();
		this.PlayGame = new System.Windows.Forms.Label();
		this.AddToSteam = new System.Windows.Forms.Label();
		this.GtaSaSlide = new System.Windows.Forms.Label();
		this.RDR2Slide = new System.Windows.Forms.Label();
		this.GtaVSlide = new System.Windows.Forms.Label();
		this.MainPanel = new System.Windows.Forms.Panel();
		this.AddGame = new System.Windows.Forms.Label();
		this.EnterID = new System.Windows.Forms.TextBox();
		this.DiscordServ = new System.Windows.Forms.Label();
		this.YouTubeBtn = new System.Windows.Forms.Label();
		this.Updates = new System.Windows.Forms.Label();
		this.SteamDetect = new System.Windows.Forms.PictureBox();
		this.steamToolsDetect = new System.Windows.Forms.PictureBox();
		this.header.SuspendLayout();
		this.gtavbanner.SuspendLayout();
		this.MainPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SteamDetect).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.steamToolsDetect).BeginInit();
		base.SuspendLayout();
		this.header.BackgroundImage = Launchbyte.Properties.Resources.header2;
		this.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.header.Controls.Add(this.label1);
		this.header.Controls.Add(this.GamesBtn);
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
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.label1.ForeColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(623, 25);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(30, 8);
		this.label1.TabIndex = 6;
		this.label1.Text = "             ";
		this.label1.Click += new System.EventHandler(label1_Click);
		this.GamesBtn.AutoSize = true;
		this.GamesBtn.BackColor = System.Drawing.Color.Transparent;
		this.GamesBtn.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.GamesBtn.ForeColor = System.Drawing.Color.Transparent;
		this.GamesBtn.Location = new System.Drawing.Point(578, 25);
		this.GamesBtn.Name = "GamesBtn";
		this.GamesBtn.Size = new System.Drawing.Size(38, 8);
		this.GamesBtn.TabIndex = 5;
		this.GamesBtn.Text = "                 ";
		this.GamesBtn.Click += new System.EventHandler(GamesBtn_Click);
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
		this.gtavbanner.BackgroundImage = Launchbyte.Properties.Resources.gtavbanner1;
		this.gtavbanner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.gtavbanner.Controls.Add(this.PlayGame);
		this.gtavbanner.Controls.Add(this.AddToSteam);
		this.gtavbanner.Controls.Add(this.GtaSaSlide);
		this.gtavbanner.Controls.Add(this.RDR2Slide);
		this.gtavbanner.Controls.Add(this.GtaVSlide);
		this.gtavbanner.Location = new System.Drawing.Point(12, 50);
		this.gtavbanner.Name = "gtavbanner";
		this.gtavbanner.Size = new System.Drawing.Size(654, 108);
		this.gtavbanner.TabIndex = 2;
		this.PlayGame.BackColor = System.Drawing.Color.Transparent;
		this.PlayGame.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.PlayGame.ForeColor = System.Drawing.Color.Transparent;
		this.PlayGame.Location = new System.Drawing.Point(89, 84);
		this.PlayGame.Name = "PlayGame";
		this.PlayGame.Size = new System.Drawing.Size(61, 17);
		this.PlayGame.TabIndex = 8;
		this.PlayGame.Text = "                 ";
		this.PlayGame.Click += new System.EventHandler(PlayGame_Click);
		this.AddToSteam.BackColor = System.Drawing.Color.Transparent;
		this.AddToSteam.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.AddToSteam.ForeColor = System.Drawing.Color.Transparent;
		this.AddToSteam.Location = new System.Drawing.Point(14, 83);
		this.AddToSteam.Name = "AddToSteam";
		this.AddToSteam.Size = new System.Drawing.Size(61, 17);
		this.AddToSteam.TabIndex = 7;
		this.AddToSteam.Text = "                 ";
		this.AddToSteam.Click += new System.EventHandler(AddToSteam_Click);
		this.GtaSaSlide.AutoSize = true;
		this.GtaSaSlide.BackColor = System.Drawing.Color.Transparent;
		this.GtaSaSlide.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.GtaSaSlide.ForeColor = System.Drawing.Color.Transparent;
		this.GtaSaSlide.Location = new System.Drawing.Point(344, 90);
		this.GtaSaSlide.Name = "GtaSaSlide";
		this.GtaSaSlide.Size = new System.Drawing.Size(10, 8);
		this.GtaSaSlide.TabIndex = 7;
		this.GtaSaSlide.Text = "   ";
		this.GtaSaSlide.Click += new System.EventHandler(GtaSaSlide_Click);
		this.RDR2Slide.AutoSize = true;
		this.RDR2Slide.BackColor = System.Drawing.Color.Transparent;
		this.RDR2Slide.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.RDR2Slide.ForeColor = System.Drawing.Color.Transparent;
		this.RDR2Slide.Location = new System.Drawing.Point(323, 90);
		this.RDR2Slide.Name = "RDR2Slide";
		this.RDR2Slide.Size = new System.Drawing.Size(10, 8);
		this.RDR2Slide.TabIndex = 7;
		this.RDR2Slide.Text = "   ";
		this.RDR2Slide.Click += new System.EventHandler(RDR2Slide_Click);
		this.GtaVSlide.AutoSize = true;
		this.GtaVSlide.BackColor = System.Drawing.Color.Transparent;
		this.GtaVSlide.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.GtaVSlide.ForeColor = System.Drawing.Color.Transparent;
		this.GtaVSlide.Location = new System.Drawing.Point(301, 90);
		this.GtaVSlide.Name = "GtaVSlide";
		this.GtaVSlide.Size = new System.Drawing.Size(12, 8);
		this.GtaVSlide.TabIndex = 6;
		this.GtaVSlide.Text = "    ";
		this.GtaVSlide.Click += new System.EventHandler(GtaVSlide_Click);
		this.MainPanel.BackgroundImage = Launchbyte.Properties.Resources.HomePage;
		this.MainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.MainPanel.Controls.Add(this.AddGame);
		this.MainPanel.Controls.Add(this.EnterID);
		this.MainPanel.Controls.Add(this.DiscordServ);
		this.MainPanel.Controls.Add(this.YouTubeBtn);
		this.MainPanel.Controls.Add(this.Updates);
		this.MainPanel.Controls.Add(this.SteamDetect);
		this.MainPanel.Controls.Add(this.steamToolsDetect);
		this.MainPanel.Location = new System.Drawing.Point(12, 166);
		this.MainPanel.Name = "MainPanel";
		this.MainPanel.Size = new System.Drawing.Size(663, 209);
		this.MainPanel.TabIndex = 3;
		this.AddGame.BackColor = System.Drawing.Color.Transparent;
		this.AddGame.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.AddGame.ForeColor = System.Drawing.Color.Transparent;
		this.AddGame.Location = new System.Drawing.Point(190, 82);
		this.AddGame.Name = "AddGame";
		this.AddGame.Size = new System.Drawing.Size(68, 19);
		this.AddGame.TabIndex = 7;
		this.AddGame.Text = "                 ";
		this.AddGame.Click += new System.EventHandler(AddGame_Click);
		this.EnterID.BackColor = System.Drawing.Color.FromArgb(217, 217, 217);
		this.EnterID.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.EnterID.Location = new System.Drawing.Point(89, 47);
		this.EnterID.Margin = new System.Windows.Forms.Padding(0);
		this.EnterID.MaxLength = 13;
		this.EnterID.Multiline = true;
		this.EnterID.Name = "EnterID";
		this.EnterID.PlaceholderText = "Enter Game ID";
		this.EnterID.Size = new System.Drawing.Size(274, 26);
		this.EnterID.TabIndex = 10;
		this.EnterID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.EnterID.WordWrap = false;
		this.DiscordServ.BackColor = System.Drawing.Color.Transparent;
		this.DiscordServ.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.DiscordServ.ForeColor = System.Drawing.Color.Transparent;
		this.DiscordServ.Location = new System.Drawing.Point(470, 95);
		this.DiscordServ.Name = "DiscordServ";
		this.DiscordServ.Size = new System.Drawing.Size(174, 30);
		this.DiscordServ.TabIndex = 9;
		this.DiscordServ.Text = "                 ";
		this.DiscordServ.Click += new System.EventHandler(DiscordServ_Click);
		this.YouTubeBtn.BackColor = System.Drawing.Color.Transparent;
		this.YouTubeBtn.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.YouTubeBtn.ForeColor = System.Drawing.Color.Transparent;
		this.YouTubeBtn.Location = new System.Drawing.Point(470, 58);
		this.YouTubeBtn.Name = "YouTubeBtn";
		this.YouTubeBtn.Size = new System.Drawing.Size(174, 30);
		this.YouTubeBtn.TabIndex = 8;
		this.YouTubeBtn.Text = "                 ";
		this.YouTubeBtn.Click += new System.EventHandler(YouTubeBtn_Click);
		this.Updates.BackColor = System.Drawing.Color.Transparent;
		this.Updates.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.Updates.ForeColor = System.Drawing.Color.Transparent;
		this.Updates.Location = new System.Drawing.Point(470, 20);
		this.Updates.Name = "Updates";
		this.Updates.Size = new System.Drawing.Size(174, 30);
		this.Updates.TabIndex = 7;
		this.Updates.Text = "                 ";
		this.Updates.Click += new System.EventHandler(Updates_Click);
		this.SteamDetect.BackColor = System.Drawing.Color.Transparent;
		this.SteamDetect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.SteamDetect.Location = new System.Drawing.Point(317, 162);
		this.SteamDetect.Name = "SteamDetect";
		this.SteamDetect.Size = new System.Drawing.Size(20, 20);
		this.SteamDetect.TabIndex = 1;
		this.SteamDetect.TabStop = false;
		this.steamToolsDetect.BackColor = System.Drawing.Color.Transparent;
		this.steamToolsDetect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.steamToolsDetect.InitialImage = null;
		this.steamToolsDetect.Location = new System.Drawing.Point(176, 162);
		this.steamToolsDetect.Name = "steamToolsDetect";
		this.steamToolsDetect.Size = new System.Drawing.Size(20, 20);
		this.steamToolsDetect.TabIndex = 0;
		this.steamToolsDetect.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		base.ClientSize = new System.Drawing.Size(680, 380);
		base.Controls.Add(this.MainPanel);
		base.Controls.Add(this.gtavbanner);
		base.Controls.Add(this.header);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Main";
		this.Text = "Launchbyte";
		base.Load += new System.EventHandler(Main_Load);
		this.header.ResumeLayout(false);
		this.header.PerformLayout();
		this.gtavbanner.ResumeLayout(false);
		this.gtavbanner.PerformLayout();
		this.MainPanel.ResumeLayout(false);
		this.MainPanel.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.SteamDetect).EndInit();
		((System.ComponentModel.ISupportInitialize)this.steamToolsDetect).EndInit();
		base.ResumeLayout(false);
	}
}
