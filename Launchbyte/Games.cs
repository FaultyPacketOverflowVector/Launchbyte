using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Launchbyte.Properties;

namespace Launchbyte;

public class Games : Form
{
	private bool isDragging = false;

	private Point offset;

	private IContainer components = null;

	private Panel header;

	private Label exit;

	private Label minimize;

	private Label HomeBtn;

	private Label label1;

	private PictureBox pictureBox1;

	private Label GTAVBtn;

	private Label GTAIVBtn;

	private Label GTASABtn;

	private Label RDR1Btn;

	private Label RDR2Btn;

	public Games()
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

	private void Games_Load(object sender, EventArgs e)
	{
		GTAVBtn.Parent = pictureBox1;
		GTAIVBtn.Parent = pictureBox1;
		GTASABtn.Parent = pictureBox1;
		RDR2Btn.Parent = pictureBox1;
		RDR1Btn.Parent = pictureBox1;
	}

	private void GTAVBtn_Click(object sender, EventArgs e)
	{
		GTAV gtavForm = new GTAV();
		gtavForm.StartPosition = FormStartPosition.Manual;
		gtavForm.Location = base.Location;
		gtavForm.Size = base.Size;
		gtavForm.Show();
		Hide();
		gtavForm.FormClosed += delegate
		{
			Show();
		};
	}

	private void GTAIVBtn_Click(object sender, EventArgs e)
	{
		GTAIV gtaivForm = new GTAIV();
		gtaivForm.StartPosition = FormStartPosition.Manual;
		gtaivForm.Location = base.Location;
		gtaivForm.Size = base.Size;
		gtaivForm.Show();
		Hide();
		gtaivForm.FormClosed += delegate
		{
			Show();
		};
	}

	private void GTASABtn_Click(object sender, EventArgs e)
	{
		GTASA gtasaForm = new GTASA();
		gtasaForm.StartPosition = FormStartPosition.Manual;
		gtasaForm.Location = base.Location;
		gtasaForm.Size = base.Size;
		gtasaForm.Show();
		Hide();
		gtasaForm.FormClosed += delegate
		{
			Show();
		};
	}

	private void RDR2Btn_Click(object sender, EventArgs e)
	{
		RDR2 rdr2Form = new RDR2();
		rdr2Form.StartPosition = FormStartPosition.Manual;
		rdr2Form.Location = base.Location;
		rdr2Form.Size = base.Size;
		rdr2Form.Show();
		Hide();
		rdr2Form.FormClosed += delegate
		{
			Show();
		};
	}

	private void RDR1Btn_Click(object sender, EventArgs e)
	{
		RDR1 rdr1Form = new RDR1();
		rdr1Form.StartPosition = FormStartPosition.Manual;
		rdr1Form.Location = base.Location;
		rdr1Form.Size = base.Size;
		rdr1Form.Show();
		Hide();
		rdr1Form.FormClosed += delegate
		{
			Show();
		};
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launchbyte.Games));
		this.header = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.HomeBtn = new System.Windows.Forms.Label();
		this.minimize = new System.Windows.Forms.Label();
		this.exit = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.GTAVBtn = new System.Windows.Forms.Label();
		this.GTAIVBtn = new System.Windows.Forms.Label();
		this.GTASABtn = new System.Windows.Forms.Label();
		this.RDR1Btn = new System.Windows.Forms.Label();
		this.RDR2Btn = new System.Windows.Forms.Label();
		this.header.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.header.BackgroundImage = Launchbyte.Properties.Resources.header2;
		this.header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.header.Controls.Add(this.label1);
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
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Segoe UI", 4f);
		this.label1.ForeColor = System.Drawing.Color.Transparent;
		this.label1.Location = new System.Drawing.Point(623, 26);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(30, 8);
		this.label1.TabIndex = 8;
		this.label1.Text = "             ";
		this.label1.Click += new System.EventHandler(label1_Click);
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
		this.pictureBox1.BackgroundImage = Launchbyte.Properties.Resources.gameswithbypass;
		this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pictureBox1.Location = new System.Drawing.Point(5, 54);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(664, 136);
		this.pictureBox1.TabIndex = 2;
		this.pictureBox1.TabStop = false;
		this.GTAVBtn.BackColor = System.Drawing.Color.Transparent;
		this.GTAVBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.GTAVBtn.Location = new System.Drawing.Point(30, 34);
		this.GTAVBtn.Name = "GTAVBtn";
		this.GTAVBtn.Size = new System.Drawing.Size(79, 90);
		this.GTAVBtn.TabIndex = 3;
		this.GTAVBtn.Text = " ";
		this.GTAVBtn.Click += new System.EventHandler(GTAVBtn_Click);
		this.GTAIVBtn.BackColor = System.Drawing.Color.Transparent;
		this.GTAIVBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.GTAIVBtn.Location = new System.Drawing.Point(120, 34);
		this.GTAIVBtn.Name = "GTAIVBtn";
		this.GTAIVBtn.Size = new System.Drawing.Size(79, 90);
		this.GTAIVBtn.TabIndex = 4;
		this.GTAIVBtn.Text = " ";
		this.GTAIVBtn.Click += new System.EventHandler(GTAIVBtn_Click);
		this.GTASABtn.BackColor = System.Drawing.Color.Transparent;
		this.GTASABtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.GTASABtn.Location = new System.Drawing.Point(211, 34);
		this.GTASABtn.Name = "GTASABtn";
		this.GTASABtn.Size = new System.Drawing.Size(79, 90);
		this.GTASABtn.TabIndex = 5;
		this.GTASABtn.Text = " ";
		this.GTASABtn.Click += new System.EventHandler(GTASABtn_Click);
		this.RDR1Btn.BackColor = System.Drawing.Color.Transparent;
		this.RDR1Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.RDR1Btn.ForeColor = System.Drawing.Color.Transparent;
		this.RDR1Btn.Location = new System.Drawing.Point(393, 34);
		this.RDR1Btn.Name = "RDR1Btn";
		this.RDR1Btn.Size = new System.Drawing.Size(79, 90);
		this.RDR1Btn.TabIndex = 6;
		this.RDR1Btn.Text = " ";
		this.RDR1Btn.Click += new System.EventHandler(RDR1Btn_Click);
		this.RDR2Btn.BackColor = System.Drawing.Color.Transparent;
		this.RDR2Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.RDR2Btn.Location = new System.Drawing.Point(302, 34);
		this.RDR2Btn.Name = "RDR2Btn";
		this.RDR2Btn.Size = new System.Drawing.Size(79, 90);
		this.RDR2Btn.TabIndex = 7;
		this.RDR2Btn.Text = " ";
		this.RDR2Btn.Click += new System.EventHandler(RDR2Btn_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		base.ClientSize = new System.Drawing.Size(680, 380);
		base.Controls.Add(this.RDR2Btn);
		base.Controls.Add(this.RDR1Btn);
		base.Controls.Add(this.GTASABtn);
		base.Controls.Add(this.GTAIVBtn);
		base.Controls.Add(this.GTAVBtn);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.header);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Games";
		this.Text = "LB";
		base.Load += new System.EventHandler(Games_Load);
		this.header.ResumeLayout(false);
		this.header.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}
}
