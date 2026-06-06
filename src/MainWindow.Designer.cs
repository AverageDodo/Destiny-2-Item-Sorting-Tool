namespace ItemSortingTool;

partial class MainWindow
{
	private System.ComponentModel.IContainer components = null;

	#region Windows Form Designer generated code

	private void InitializeComponent()
	{
		_importCsvButton = new System.Windows.Forms.Button();
		_selectedFileTextBox = new System.Windows.Forms.TextBox();
		_selectedFileLabel = new System.Windows.Forms.Label();
		_statisticsGroupBox = new System.Windows.Forms.GroupBox();
		_linesImportedTitleLabel = new System.Windows.Forms.Label();
		_linesImportedValueLabel = new System.Windows.Forms.Label();
		_importStatusTitleLabel = new System.Windows.Forms.Label();
		_importStatusValueLabel = new System.Windows.Forms.Label();
		_csvOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
		_resultGroupBox = new System.Windows.Forms.GroupBox();
		_duplicatesDataGridView = new System.Windows.Forms.DataGridView();
		_equalityComparerComboBox = new ComboBox();
		_statisticsGroupBox.SuspendLayout();
		_resultGroupBox.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)_duplicatesDataGridView).BeginInit();
		SuspendLayout();
		//
		// _importCsvButton
		//
		_importCsvButton.Location = new System.Drawing.Point(30, 30);
		_importCsvButton.Name = "_importCsvButton";
		_importCsvButton.Size = new System.Drawing.Size(160, 35);
		_importCsvButton.TabIndex = 0;
		_importCsvButton.Text = "Select CSV File";
		_importCsvButton.UseVisualStyleBackColor = true;
		_importCsvButton.Click += ImportCsvButton_Click;
		//
		// _equalityComparerComboBox
		//
		_equalityComparerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		_equalityComparerComboBox.FormattingEnabled = true;
		_equalityComparerComboBox.Location = new System.Drawing.Point(210, 36);
		_equalityComparerComboBox.Name = "_equalityComparerComboBox";
		_equalityComparerComboBox.Size = new System.Drawing.Size(220, 23);
		_equalityComparerComboBox.TabIndex = 1;
		//
		// _selectedFileTextBox
		//
		_selectedFileTextBox.Location = new System.Drawing.Point(30, 105);
		_selectedFileTextBox.Name = "_selectedFileTextBox";
		_selectedFileTextBox.ReadOnly = true;
		_selectedFileTextBox.Size = new System.Drawing.Size(720, 23);
		_selectedFileTextBox.TabIndex = 2;
		//
		// _selectedFileLabel
		//
		_selectedFileLabel.AutoSize = true;
		_selectedFileLabel.Location = new System.Drawing.Point(30, 85);
		_selectedFileLabel.Name = "_selectedFileLabel";
		_selectedFileLabel.Size = new System.Drawing.Size(73, 15);
		_selectedFileLabel.TabIndex = 1;
		_selectedFileLabel.Text = "Selected file:";
		//
		// _statisticsGroupBox
		//
		_statisticsGroupBox.Controls.Add(_linesImportedTitleLabel);
		_statisticsGroupBox.Controls.Add(_linesImportedValueLabel);
		_statisticsGroupBox.Controls.Add(_importStatusTitleLabel);
		_statisticsGroupBox.Controls.Add(_importStatusValueLabel);
		_statisticsGroupBox.Location = new System.Drawing.Point(30, 155);
		_statisticsGroupBox.Name = "_statisticsGroupBox";
		_statisticsGroupBox.Size = new System.Drawing.Size(720, 140);
		_statisticsGroupBox.TabIndex = 3;
		_statisticsGroupBox.TabStop = false;
		_statisticsGroupBox.Text = "Import Statistics";
		//
		// _linesImportedTitleLabel
		//
		_linesImportedTitleLabel.AutoSize = true;
		_linesImportedTitleLabel.Location = new System.Drawing.Point(20, 35);
		_linesImportedTitleLabel.Name = "_linesImportedTitleLabel";
		_linesImportedTitleLabel.Size = new System.Drawing.Size(89, 15);
		_linesImportedTitleLabel.TabIndex = 0;
		_linesImportedTitleLabel.Text = "Lines imported:";
		//
		// _linesImportedValueLabel
		//
		_linesImportedValueLabel.AutoSize = true;
		_linesImportedValueLabel.Location = new System.Drawing.Point(150, 35);
		_linesImportedValueLabel.Name = "_linesImportedValueLabel";
		_linesImportedValueLabel.Size = new System.Drawing.Size(13, 15);
		_linesImportedValueLabel.TabIndex = 1;
		_linesImportedValueLabel.Text = "0";
		//
		// _importStatusTitleLabel
		//
		_importStatusTitleLabel.AutoSize = true;
		_importStatusTitleLabel.Location = new System.Drawing.Point(20, 70);
		_importStatusTitleLabel.Name = "_importStatusTitleLabel";
		_importStatusTitleLabel.Size = new System.Drawing.Size(42, 15);
		_importStatusTitleLabel.TabIndex = 2;
		_importStatusTitleLabel.Text = "Status:";
		//
		// _importStatusValueLabel
		//
		_importStatusValueLabel.AutoSize = true;
		_importStatusValueLabel.Location = new System.Drawing.Point(150, 70);
		_importStatusValueLabel.Name = "_importStatusValueLabel";
		_importStatusValueLabel.Size = new System.Drawing.Size(88, 15);
		_importStatusValueLabel.TabIndex = 3;
		_importStatusValueLabel.Text = "No file selected";
		//
		// _csvOpenFileDialog
		//
		_csvOpenFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
		_csvOpenFileDialog.Title = "Select CSV File";
		//
		// _resultGroupBox
		//
		_resultGroupBox.Controls.Add(_duplicatesDataGridView);
		_resultGroupBox.Location = new System.Drawing.Point(28, 319);
		_resultGroupBox.Name = "_resultGroupBox";
		_resultGroupBox.Size = new System.Drawing.Size(750, 600);
		_resultGroupBox.TabIndex = 4;
		_resultGroupBox.TabStop = false;
		_resultGroupBox.Text = "Duplicate Items";
		_resultGroupBox.Visible = false;
		//
		// _duplicatesDataGridView
		//
		_duplicatesDataGridView.AllowUserToAddRows = false;
		_duplicatesDataGridView.AllowUserToDeleteRows = false;
		_duplicatesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		_duplicatesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		_duplicatesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
		_duplicatesDataGridView.Location = new System.Drawing.Point(3, 19);
		_duplicatesDataGridView.MaximumSize = new System.Drawing.Size(0, 500);
		_duplicatesDataGridView.Name = "_duplicatesDataGridView";
		_duplicatesDataGridView.ReadOnly = true;
		_duplicatesDataGridView.RowHeadersVisible = false;
		_duplicatesDataGridView.Size = new System.Drawing.Size(715, 250);
		_duplicatesDataGridView.TabIndex = 0;
		//
		// MainWindow
		//
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		ClientSize = new System.Drawing.Size(984, 761);
		Controls.Add(_resultGroupBox);
		Controls.Add(_importCsvButton);
		Controls.Add(_equalityComparerComboBox);
		Controls.Add(_selectedFileLabel);
		Controls.Add(_selectedFileTextBox);
		Controls.Add(_statisticsGroupBox);
		StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "Item Sorting Tool";
		_statisticsGroupBox.ResumeLayout(false);
		_statisticsGroupBox.PerformLayout();
		_resultGroupBox.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)_duplicatesDataGridView).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}

		base.Dispose(disposing);
	}

}