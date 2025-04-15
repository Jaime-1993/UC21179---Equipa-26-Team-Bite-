namespace PDFNarrator
{
    partial class View
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLoadPDF = new System.Windows.Forms.Button();
            this.btnStartNarration = new System.Windows.Forms.Button();
            this.btnStopNarration = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Dummy = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLoadPDF
            // 
            this.btnLoadPDF.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnLoadPDF.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadPDF.Location = new System.Drawing.Point(59, 31);
            this.btnLoadPDF.Name = "btnLoadPDF";
            this.btnLoadPDF.Size = new System.Drawing.Size(128, 52);
            this.btnLoadPDF.TabIndex = 0;
            this.btnLoadPDF.Text = "Load PDF";
            this.btnLoadPDF.UseVisualStyleBackColor = true;
            // 
            // btnStartNarration
            // 
            this.btnStartNarration.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnStartNarration.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartNarration.Location = new System.Drawing.Point(228, 31);
            this.btnStartNarration.Name = "btnStartNarration";
            this.btnStartNarration.Size = new System.Drawing.Size(128, 52);
            this.btnStartNarration.TabIndex = 1;
            this.btnStartNarration.Text = "Start Narration";
            this.btnStartNarration.UseVisualStyleBackColor = true;
            // 
            // btnStopNarration
            // 
            this.btnStopNarration.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnStopNarration.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopNarration.Location = new System.Drawing.Point(393, 31);
            this.btnStopNarration.Name = "btnStopNarration";
            this.btnStopNarration.Size = new System.Drawing.Size(128, 52);
            this.btnStopNarration.TabIndex = 2;
            this.btnStopNarration.Text = "Stop Narration";
            this.btnStopNarration.UseVisualStyleBackColor = true;
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.SystemColors.Window;
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtOutput.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(59, 114);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(462, 216);
            this.txtOutput.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "PDF Transcription";
            // 
            // btn_Dummy
            // 
            this.btn_Dummy.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_Dummy.Font = new System.Drawing.Font("Verdana", 9F);
            this.btn_Dummy.Location = new System.Drawing.Point(507, 336);
            this.btn_Dummy.Name = "btn_Dummy";
            this.btn_Dummy.Size = new System.Drawing.Size(74, 23);
            this.btn_Dummy.TabIndex = 5;
            this.btn_Dummy.Text = "Dummy";
            this.btn_Dummy.UseVisualStyleBackColor = true;
            this.btn_Dummy.Click += new System.EventHandler(this.btn_dummy_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_clear.Font = new System.Drawing.Font("Verdana", 9F);
            this.btn_clear.Location = new System.Drawing.Point(393, 336);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(74, 23);
            this.btn_clear.TabIndex = 6;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_Dummy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnStopNarration);
            this.Controls.Add(this.btnStartNarration);
            this.Controls.Add(this.btnLoadPDF);
            this.Name = "View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[UC21179 - Team 26 (Team Bite)] - PDF Narrator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadPDF;
        private System.Windows.Forms.Button btnStartNarration;
        private System.Windows.Forms.Button btnStopNarration;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Dummy;
        private System.Windows.Forms.Button btn_clear;
    }
}

