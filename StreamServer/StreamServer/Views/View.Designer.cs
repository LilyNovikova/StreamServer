using System;

namespace StreamServer.Views
{
    public partial class View
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.port = new System.Windows.Forms.TextBox();
            this.ServerIPAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listen = new System.Windows.Forms.Button();
            this.serverLabel = new System.Windows.Forms.Label();
            this.serverStatus = new System.Windows.Forms.TextBox();
            this.clientRequests = new System.Windows.Forms.TextBox();
            this.clientLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(103, 39);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(100, 20);
            this.port.TabIndex = 0;
            this.port.Text = "3000";
            // 
            // ServerIPAddress
            // 
            this.ServerIPAddress.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ServerIPAddress.Location = new System.Drawing.Point(351, 39);
            this.ServerIPAddress.Name = "ServerIPAddress";
            this.ServerIPAddress.ReadOnly = true;
            this.ServerIPAddress.Size = new System.Drawing.Size(100, 20);
            this.ServerIPAddress.TabIndex = 1;
            this.ServerIPAddress.Text = "192.168.0.101";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Listen on Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Server IP Address";
            // 
            // listen
            // 
            this.listen.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listen.Location = new System.Drawing.Point(537, 22);
            this.listen.Name = "listen";
            this.listen.Size = new System.Drawing.Size(128, 52);
            this.listen.TabIndex = 2;
            this.listen.Text = "Listen";
            this.listen.UseVisualStyleBackColor = true;
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Location = new System.Drawing.Point(30, 89);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(71, 13);
            this.serverLabel.TabIndex = 8;
            this.serverLabel.Text = "Server Status";
            // 
            // serverStatus
            // 
            this.serverStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.serverStatus.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.serverStatus.Location = new System.Drawing.Point(32, 105);
            this.serverStatus.Multiline = true;
            this.serverStatus.Name = "serverStatus";
            this.serverStatus.ReadOnly = true;
            this.serverStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.serverStatus.Size = new System.Drawing.Size(331, 424);
            this.serverStatus.TabIndex = 9;
            // 
            // clientRequests
            // 
            this.clientRequests.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clientRequests.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clientRequests.Location = new System.Drawing.Point(396, 105);
            this.clientRequests.Multiline = true;
            this.clientRequests.Name = "clientRequests";
            this.clientRequests.ReadOnly = true;
            this.clientRequests.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.clientRequests.Size = new System.Drawing.Size(331, 424);
            this.clientRequests.TabIndex = 11;
            // 
            // clientLabel
            // 
            this.clientLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clientLabel.AutoSize = true;
            this.clientLabel.Location = new System.Drawing.Point(393, 89);
            this.clientLabel.Name = "clientLabel";
            this.clientLabel.Size = new System.Drawing.Size(81, 13);
            this.clientLabel.TabIndex = 10;
            this.clientLabel.Text = "Client Requests";
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 541);
            this.Controls.Add(this.clientRequests);
            this.Controls.Add(this.clientLabel);
            this.Controls.Add(this.serverStatus);
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listen);
            this.Controls.Add(this.ServerIPAddress);
            this.Controls.Add(this.port);
            this.Name = "View";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox ServerIPAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button listen;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.TextBox serverStatus;
        private System.Windows.Forms.TextBox clientRequests;
        private System.Windows.Forms.Label clientLabel;
    }
}

