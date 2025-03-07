using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Launchbyte.Properties;

namespace Launchbyte;

public class SteamTools : Form
{
	private bool isDragging = false;

	private Point offset;

	private IContainer components = null;

	private Panel panel1;

	private Label exitapp;

	private Panel panel2;

	private Label exit;

	private Label installSteamTools;

	public SteamTools()
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

	private void exitapp_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void exit_Click_1(object sender, EventArgs e)
	{
		Close();
	}

	private void installSteamTools_Click(object sender, EventArgs e)
	{
		try
		{
			string url = "https://github.com/uxqc/m4n1f3st/raw/refs/heads/main/SteamtoolsSetup.exe";
			string tempPath = Path.Combine(Path.GetTempPath(), "SteamtoolsSetup.exe");
			using (WebClient webClient = new WebClient())
			{
				webClient.DownloadFile(url, tempPath);
			}
			Process.Start(new ProcessStartInfo(tempPath)
			{
				UseShellExecute = true
			});
			MessageBox.Show("Steam Tools installer has been downloaded and is now running.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launchbyte.SteamTools));
		this.panel1 = new System.Windows.Forms.Panel();
		this.exitapp = new System.Windows.Forms.Label();
		this.panel2 = new System.Windows.Forms.Panel();
		this.installSteamTools = new System.Windows.Forms.Label();
		this.exit = new System.Windows.Forms.Label();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.Transparent;
		this.panel1.Controls.Add(this.exitapp);
		this.panel1.Location = new System.Drawing.Point(2, -1);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(399, 47);
		this.panel1.TabIndex = 0;
		this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(header_MouseDown);
		this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(header_MouseMove);
		this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(header_MouseUp);
		this.exitapp.AutoSize = true;
		this.exitapp.Location = new System.Drawing.Point(376, 0);
		this.exitapp.Name = "exitapp";
		this.exitapp.Size = new System.Drawing.Size(22, 15);
		this.exitapp.TabIndex = 0;
		this.exitapp.Text = "     ";
		this.exitapp.Click += new System.EventHandler(exitapp_Click);
		this.panel2.BackColor = System.Drawing.Color.Transparent;
		this.panel2.Controls.Add(this.installSteamTools);
		this.panel2.Controls.Add(this.exit);
		this.panel2.Location = new System.Drawing.Point(12, 69);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(376, 44);
		this.panel2.TabIndex = 1;
		this.installSteamTools.AutoSize = true;
		this.installSteamTools.Location = new System.Drawing.Point(218, 17);
		this.installSteamTools.Name = "installSteamTools";
		this.installSteamTools.Size = new System.Drawing.Size(91, 15);
		this.installSteamTools.TabIndex = 1;
		this.installSteamTools.Text = "                            ";
		this.installSteamTools.Click += new System.EventHandler(installSteamTools_Click);
		this.exit.AutoSize = true;
		this.exit.Location = new System.Drawing.Point(62, 17);
		this.exit.Name = "exit";
		this.exit.Size = new System.Drawing.Size(91, 15);
		this.exit.TabIndex = 0;
		this.exit.Text = "                            ";
		this.exit.Click += new System.EventHandler(exit_Click_1);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackgroundImage = Launchbyte.Properties.Resources.steamtoolz;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.ClientSize = new System.Drawing.Size(400, 113);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "SteamTools";
		this.Text = "Form1";
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		base.ResumeLayout(false);
	}
}
