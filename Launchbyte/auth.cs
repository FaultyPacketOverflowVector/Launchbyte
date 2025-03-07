using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Launchbyte.Properties;

namespace Launchbyte;

public class auth : Form
{
	private bool isDragging = false;

	private Point offset;

	private IContainer components = null;

	private TextBox keybox;

	private Panel panel1;

	private Panel panel2;

	private Label label2;

	private Label label1;

	private Label label4;

	private Label label3;

	public auth()
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

	private void label4_Click(object sender, EventArgs e)
	{
		Application.Exit();
	}

	private void label3_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private async void label2_Click(object sender, EventArgs e)
	{
		string url = "https://raw.githubusercontent.com/uxqc/L4unch3rH0sting/refs/heads/main/k3y";
		try
		{
			using WebClient client = new WebClient();
			string[] validKeys = (await client.DownloadStringTaskAsync(url)).Split(new string[2] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
			if (validKeys.Contains(keybox.Text))
			{
				Main mainForm = new Main();
				mainForm.Show();
				Hide();
			}
			else
			{
				MessageBox.Show("Invalid key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show("Error fetching keys: " + ex2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void label1_Click(object sender, EventArgs e)
	{
		try
		{
			ProcessStartInfo psi = new ProcessStartInfo
			{
				FileName = "https://link-target.net/501526/launchbyte-key1",
				UseShellExecute = true
			};
			Process.Start(psi);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Unable to open link: " + ex.Message);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launchbyte.auth));
		this.keybox = new System.Windows.Forms.TextBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.keybox.BackColor = System.Drawing.Color.FromArgb(238, 238, 238);
		this.keybox.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.keybox.Location = new System.Drawing.Point(186, 116);
		this.keybox.MaxLength = 200;
		this.keybox.Name = "keybox";
		this.keybox.Size = new System.Drawing.Size(278, 16);
		this.keybox.TabIndex = 0;
		this.panel1.BackColor = System.Drawing.Color.Transparent;
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Location = new System.Drawing.Point(196, 136);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(256, 36);
		this.panel1.TabIndex = 1;
		this.label2.Location = new System.Drawing.Point(152, 7);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(98, 21);
		this.label2.TabIndex = 1;
		this.label2.Click += new System.EventHandler(label2_Click);
		this.label1.Location = new System.Drawing.Point(11, 7);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(96, 21);
		this.label1.TabIndex = 0;
		this.label1.Click += new System.EventHandler(label1_Click);
		this.panel2.BackColor = System.Drawing.Color.Transparent;
		this.panel2.Controls.Add(this.label4);
		this.panel2.Controls.Add(this.label3);
		this.panel2.Location = new System.Drawing.Point(2, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(648, 18);
		this.panel2.TabIndex = 2;
		this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(header_MouseDown);
		this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(header_MouseMove);
		this.panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(header_MouseUp);
		this.label4.Location = new System.Drawing.Point(623, 1);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(26, 10);
		this.label4.TabIndex = 3;
		this.label4.Click += new System.EventHandler(label4_Click);
		this.label3.Location = new System.Drawing.Point(591, 1);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(28, 10);
		this.label3.TabIndex = 2;
		this.label3.Click += new System.EventHandler(label3_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackgroundImage = Launchbyte.Properties.Resources.auth2;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.ClientSize = new System.Drawing.Size(650, 186);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.keybox);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "auth";
		this.Text = "auth";
		this.panel1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
