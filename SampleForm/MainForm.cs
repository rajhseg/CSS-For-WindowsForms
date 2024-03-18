using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CssLibrary;

namespace SampleForm
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		WinformsStyleLoader theme;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			dataGridView1.Rows.Add(new[]{"id1","name1"});
			dataGridView1.Rows.Add(new[]{"id1","name1"});
			dataGridView1.Rows.Add(new[]{"id1","name1"});
			dataGridView1.Rows.Add(new[]{"id1","name1"});					
						
			theme =  WinformsStyleLoader.GetThemeLoader();
			theme.Load();
		}
		
		void DataGridView1CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			
		}
		
		void ComboBox1StyleChanged(object sender, EventArgs e)
		{
			
		}
		
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			var sheetname = comboBox1.SelectedItem;
			WinformsStyleLoader.SetSheetName(sheetname.ToString());
			theme.RenderStyle();
		}
		
		void ComboBox2SelectedIndexChanged(object sender, EventArgs e)
		{
			var themefile = comboBox2.SelectedItem;
			theme.Load(themefile.ToString());
			
			comboBox1.SelectedIndex = 0;
			var sheetname = comboBox1.SelectedItem;
			
			WinformsStyleLoader.SetSheetName(sheetname.ToString());
			theme.RenderStyle();
		}
	}
}
