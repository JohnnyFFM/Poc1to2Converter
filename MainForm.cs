using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;


namespace poc1poc2Conv
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>

    public class MainForm : System.Windows.Forms.Form
    {
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetDiskFreeSpace(string lpRootPathName, out int lpSectorsPerCluster, out int lpBytesPerSector, out int lpNumberOfFreeClusters, out int lpTotalNumberOfClusters);

        private GroupBox grpConverter;
        private Button btnAddFile;
        private ListView plotFileList;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private Label plotFileListLabel;
        private Button btnConversion;
        private Label memLimitLabel1;
        private Label memLimitLabel2;
        private NumericUpDown memoryLimit;
        private Button btnAddFolder;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private GroupBox grpStatus;
        private Label label1;
        private TextBox textBox2;
        private Button button1;
        private ListView statusList;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader13;
        private ColumnHeader columnHeader14;
        private FolderBrowserDialog folderBrowserDialog;
        private Button btnClearSelected;
        private Button btnClearAll;
        private ColumnHeader columnHeader15;
        private StatusStrip statusStrip;
        private ColumnHeader columnHeader11;
        private OpenFileDialog openFileDialog;
        private Button btnBrowse;
        private TextBox outputDir;
        private Label outputLabel;
        private CheckBox log;
        private CheckBox fastmode;
        static AutoResetEvent[] autoEvents;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public MainForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //Fill command line parameter
            memoryLimit.Value = memlimit;
            if (startscoop != 0) { btnConversion.Text = "Start Repair"; }
            if (logging) log.Checked = true;
            if (sourcefile != "") addFile(sourcefile);
            if (sourcedir != "") addDir(sourcedir);
            if (targetdir != "") outputDir.Text = targetdir;
            if (autostart) button4_Click(null,null);
            if (fast) fastmode.Checked = true;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.grpConverter = new System.Windows.Forms.GroupBox();
            this.log = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.outputDir = new System.Windows.Forms.TextBox();
            this.outputLabel = new System.Windows.Forms.Label();
            this.btnClearSelected = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.plotFileListLabel = new System.Windows.Forms.Label();
            this.btnConversion = new System.Windows.Forms.Button();
            this.memLimitLabel1 = new System.Windows.Forms.Label();
            this.memLimitLabel2 = new System.Windows.Forms.Label();
            this.memoryLimit = new System.Windows.Forms.NumericUpDown();
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.plotFileList = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddFile = new System.Windows.Forms.Button();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.statusList = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fastmode = new System.Windows.Forms.CheckBox();
            this.grpConverter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoryLimit)).BeginInit();
            this.grpStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConverter
            // 
            this.grpConverter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConverter.Controls.Add(this.fastmode);
            this.grpConverter.Controls.Add(this.log);
            this.grpConverter.Controls.Add(this.btnBrowse);
            this.grpConverter.Controls.Add(this.outputDir);
            this.grpConverter.Controls.Add(this.outputLabel);
            this.grpConverter.Controls.Add(this.btnClearSelected);
            this.grpConverter.Controls.Add(this.btnClearAll);
            this.grpConverter.Controls.Add(this.plotFileListLabel);
            this.grpConverter.Controls.Add(this.btnConversion);
            this.grpConverter.Controls.Add(this.memLimitLabel1);
            this.grpConverter.Controls.Add(this.memLimitLabel2);
            this.grpConverter.Controls.Add(this.memoryLimit);
            this.grpConverter.Controls.Add(this.btnAddFolder);
            this.grpConverter.Controls.Add(this.plotFileList);
            this.grpConverter.Controls.Add(this.btnAddFile);
            this.grpConverter.Location = new System.Drawing.Point(12, 12);
            this.grpConverter.Name = "grpConverter";
            this.grpConverter.Size = new System.Drawing.Size(846, 287);
            this.grpConverter.TabIndex = 11;
            this.grpConverter.TabStop = false;
            this.grpConverter.Text = "Sequential Plot File Conversion (POC1 --> POC2)";
            // 
            // log
            // 
            this.log.AutoSize = true;
            this.log.Location = new System.Drawing.Point(511, 19);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(91, 17);
            this.log.TabIndex = 24;
            this.log.Text = "Create Logfile";
            this.log.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(511, 252);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(74, 24);
            this.btnBrowse.TabIndex = 23;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.Click += new System.EventHandler(this.button2_Click);
            // 
            // outputDir
            // 
            this.outputDir.Location = new System.Drawing.Point(243, 255);
            this.outputDir.Name = "outputDir";
            this.outputDir.Size = new System.Drawing.Size(262, 20);
            this.outputDir.TabIndex = 22;
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(12, 258);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(208, 13);
            this.outputLabel.TabIndex = 21;
            this.outputLabel.Text = "Output Folder (empty for inline conversion):";
            // 
            // btnClearSelected
            // 
            this.btnClearSelected.Location = new System.Drawing.Point(243, 21);
            this.btnClearSelected.Name = "btnClearSelected";
            this.btnClearSelected.Size = new System.Drawing.Size(112, 24);
            this.btnClearSelected.TabIndex = 20;
            this.btnClearSelected.Text = "Clear Selected";
            this.btnClearSelected.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(361, 21);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(112, 24);
            this.btnClearAll.TabIndex = 19;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.Click += new System.EventHandler(this.button5_Click);
            // 
            // plotFileListLabel
            // 
            this.plotFileListLabel.AutoSize = true;
            this.plotFileListLabel.Location = new System.Drawing.Point(12, 55);
            this.plotFileListLabel.Name = "plotFileListLabel";
            this.plotFileListLabel.Size = new System.Drawing.Size(77, 13);
            this.plotFileListLabel.TabIndex = 16;
            this.plotFileListLabel.Text = "Poc1 Plot Files";
            // 
            // btnConversion
            // 
            this.btnConversion.Location = new System.Drawing.Point(679, 252);
            this.btnConversion.Name = "btnConversion";
            this.btnConversion.Size = new System.Drawing.Size(156, 24);
            this.btnConversion.TabIndex = 13;
            this.btnConversion.Text = "Start Conversion";
            this.btnConversion.Click += new System.EventHandler(this.button4_Click);
            // 
            // memLimitLabel1
            // 
            this.memLimitLabel1.AutoSize = true;
            this.memLimitLabel1.Location = new System.Drawing.Point(676, 20);
            this.memLimitLabel1.Name = "memLimitLabel1";
            this.memLimitLabel1.Size = new System.Drawing.Size(68, 13);
            this.memLimitLabel1.TabIndex = 15;
            this.memLimitLabel1.Text = "Memory Limit";
            // 
            // memLimitLabel2
            // 
            this.memLimitLabel2.AutoSize = true;
            this.memLimitLabel2.Location = new System.Drawing.Point(817, 20);
            this.memLimitLabel2.Name = "memLimitLabel2";
            this.memLimitLabel2.Size = new System.Drawing.Size(23, 13);
            this.memLimitLabel2.TabIndex = 14;
            this.memLimitLabel2.Text = "MB";
            // 
            // memoryLimit
            // 
            this.memoryLimit.Increment = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.memoryLimit.Location = new System.Drawing.Point(750, 18);
            this.memoryLimit.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.memoryLimit.Minimum = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.memoryLimit.Name = "memoryLimit";
            this.memoryLimit.Size = new System.Drawing.Size(61, 20);
            this.memoryLimit.TabIndex = 13;
            this.memoryLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.memoryLimit.ThousandsSeparator = true;
            this.memoryLimit.Value = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(125, 21);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(112, 24);
            this.btnAddFolder.TabIndex = 11;
            this.btnAddFolder.Text = "Add Directory...";
            this.btnAddFolder.Click += new System.EventHandler(this.button3_Click);
            // 
            // plotFileList
            // 
            this.plotFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotFileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.plotFileList.FullRowSelect = true;
            this.plotFileList.HideSelection = false;
            this.plotFileList.Location = new System.Drawing.Point(10, 75);
            this.plotFileList.Name = "plotFileList";
            this.plotFileList.Size = new System.Drawing.Size(824, 168);
            this.plotFileList.TabIndex = 10;
            this.plotFileList.UseCompatibleStateImageBehavior = false;
            this.plotFileList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File";
            this.columnHeader2.Width = 121;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Account ID";
            this.columnHeader3.Width = 148;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Start";
            this.columnHeader4.Width = 95;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "End";
            this.columnHeader5.Width = 84;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Nonces";
            this.columnHeader6.Width = 82;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Stagger";
            this.columnHeader7.Width = 80;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "FileSize";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Status";
            this.columnHeader9.Width = 150;
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(7, 21);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(112, 24);
            this.btnAddFile.TabIndex = 9;
            this.btnAddFile.Text = "Add File(s)...";
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // grpStatus
            // 
            this.grpStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStatus.Controls.Add(this.label1);
            this.grpStatus.Controls.Add(this.textBox2);
            this.grpStatus.Controls.Add(this.button1);
            this.grpStatus.Controls.Add(this.statusList);
            this.grpStatus.Location = new System.Drawing.Point(12, 305);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(846, 170);
            this.grpStatus.TabIndex = 12;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 255);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Target Folder (empty for inline): ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(173, 249);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(243, 20);
            this.textBox2.TabIndex = 17;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(569, 249);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 24);
            this.button1.TabIndex = 13;
            this.button1.Text = "Generate Dummy Plotfile";
            // 
            // statusList
            // 
            this.statusList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.statusList.FullRowSelect = true;
            this.statusList.Location = new System.Drawing.Point(10, 19);
            this.statusList.Name = "statusList";
            this.statusList.Size = new System.Drawing.Size(824, 145);
            this.statusList.TabIndex = 10;
            this.statusList.UseCompatibleStateImageBehavior = false;
            this.statusList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Task";
            this.columnHeader10.Width = 106;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Nonces";
            this.columnHeader11.Width = 95;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Status";
            this.columnHeader12.Width = 346;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Completition";
            this.columnHeader13.Width = 89;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Time Elapsed";
            this.columnHeader14.Width = 93;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Time Remaining";
            this.columnHeader15.Width = 91;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.SelectedPath = "d:\\plot";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 489);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(870, 22);
            this.statusStrip.TabIndex = 13;
            this.statusStrip.Text = "statusStrip1";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Poc1 Plot files|*_*_*_*.*";
            this.openFileDialog.Multiselect = true;
            // 
            // fastmode
            // 
            this.fastmode.AutoSize = true;
            this.fastmode.Location = new System.Drawing.Point(511, 43);
            this.fastmode.Name = "fastmode";
            this.fastmode.Size = new System.Drawing.Size(227, 17);
            this.fastmode.TabIndex = 25;
            this.fastmode.Text = "Fast Disk to Disk mode (EXPERIMENTAL)";
            this.fastmode.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(870, 511);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.grpConverter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Johnny\'s POC1->POC2 Plot Converter v.2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.grpConverter.ResumeLayout(false);
            this.grpConverter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoryLimit)).EndInit();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static Boolean logging = false;       //disable direct IO
        public static Boolean ddio = false;       //disable direct IO
        public static Boolean fast = false;       //fast disk to disk
        private static Boolean autostart = false;  //autostart conversion
        private static string sourcefile = "";  //add plot file via command line
        private static string sourcedir = "";   //add plot files via command line
        private static string targetdir = "";   //set output dir via command line
        private static int memlimit = 4096;     //memory limit
        private static int startscoop = 0;     //scoop to start at (in case of a unwanted interruption)
        private static string logfile = "";   //logfile
        private static Boolean halt;         //halt conversion after current file
        private static Boolean stop;         //stop conversion after error
      

        static void logIT(string e)
        {
            File.AppendAllText(System.Environment.CurrentDirectory + "\\" + logfile , e + "\r\n");
        }

        [STAThread]
        static void Main(string[] args)
        {

            logfile = string.Format(@"PoC1PoC2Converter_{0}.log", Guid.NewGuid());

            //parse arguments
            for (int i = 0; i < args.Length; i++) {
                if ((args[i] == "-srcfile") && (args[++i]) is string)
                    sourcefile = (args[i].ToString());
                if ((args[i] == "-srcdir") && (args[++i]) is string)
                    sourcedir = (args[i].ToString());
                if ((args[i] == "-tardir") && (args[++i]) is string)
                    targetdir = (args[i].ToString());
                if (args[i] == "-startscoop")
                {
                    int.TryParse(args[++i], out startscoop);
                    startscoop = Math.Max(0, Math.Min(2047, startscoop));
                }
                if (args[i] == "-ddio") ddio = true;
                if (args[i] == "-log") logging = true;
                if (args[i] == "-fast") fast = true;
                if (args[i] == "-autostart") autostart = true;
                if (args[i] == "-mem")
                {
                    int.TryParse(args[++i], out memlimit);
                    memlimit = Math.Max(96, Math.Min(4096, memlimit));
                }
            }
            Application.Run(new MainForm());
        }


        private plotfile parsePlotFileName(string name)
        {
            string[] temp = name.Split('\\');
            string[] pfn = temp[temp.GetLength(0) - 1].Split('_');
            plotfile result;
            result.id = Convert.ToUInt64(pfn[0]);
            result.start = Convert.ToUInt32(pfn[1]);
            result.nonces = Convert.ToInt64(pfn[2]);
            result.stagger = Convert.ToInt64(pfn[3]);
            return result;
        }

        private string prettyBytes(long bytes)
        {
            string result;
            if (bytes < 1024) { result = Math.Round((double)bytes, 1).ToString() + "B"; }
            else if (bytes < 1024 * 1024) { result = Math.Round((double)bytes / 1024, 1).ToString() + "kB"; }
            else if (bytes < 1024 * 1024 * 1024) { result = Math.Round((double)bytes / 1024 / 1024, 1).ToString() + "MB"; }
            else if (bytes < 1024L * 1024 * 1024 * 1024) { result = Math.Round((double)bytes / 1024 / 1024 / 1024, 1).ToString() + "GB"; }
            else { result = Math.Round((double)bytes / 1024 / 1024 / 1024 / 1024, 1).ToString() + "TB"; }
            return result;
        }

        private bool isOptimizedPOC1PlotFileName(string filename)
        {
            Regex rgx = new Regex(@"(.)*(\\)+\d+(_)\d+(_)\d+(_)\d+$");

            if (rgx.IsMatch(filename)) {
                plotfile temp = parsePlotFileName(filename);
                return temp.stagger == temp.nonces ? true : false;
            }
            else
            {
                return false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in System.IO.Directory.GetFiles(folderBrowserDialog.SelectedPath, "*"))
                {
                    if (isOptimizedPOC1PlotFileName(file) && !plotFileList.Items.ContainsKey(file))
                    {
                        plotfile temp = parsePlotFileName(file);
                        long length = new System.IO.FileInfo(file).Length;
                        ListViewItem item = new ListViewItem(file);
                        item.Text = file;
                        item.Name = file;
                        item.SubItems.Add(temp.id.ToString());
                        item.SubItems.Add(temp.start.ToString());
                        item.SubItems.Add((temp.start + Convert.ToUInt32(temp.nonces) - 1).ToString());
                        item.SubItems.Add(temp.nonces.ToString());
                        item.SubItems.Add(temp.stagger.ToString());
                        item.SubItems.Add(prettyBytes(length));
                        item.SubItems.Add(length == temp.nonces * 4096 * 32 * 2 ? "Ok." : "Error: Actual file size not matching implied file size!");
                        plotFileList.Items.Add(item);
                    }

                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            while (plotFileList.SelectedItems.Count > 0)
            {
                plotFileList.Items.Remove(plotFileList.SelectedItems[0]);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            plotFileList.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            halt = false;
            stop = false;
            if (outputDir.Text == "")
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to start a inline conversion of your files? Process can't be stopped! Stopping will damage your Plot-Files. Are you sure?", "Inline Conversion Warning", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            btnConversion.Enabled = false;
            outputDir.Enabled = false;
            btnBrowse.Enabled = false;
            memoryLimit.Enabled = false;

            //create status tasks
            statusList.Items.Clear();
            foreach (ListViewItem x in plotFileList.Items) {
                if (x.SubItems[7].Text.Equals("Ok.")) {
                    ListViewItem item = new ListViewItem(x.Name);
                    item.Text = x.Name;
                    item.Name = x.Name;
                    item.SubItems.Add(x.SubItems[4].Text);
                    item.SubItems.Add("Waiting...");
                    item.SubItems.Add("0%");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    statusList.Items.Add(item);
                }
            }
            plotFileList.Items.Clear();

            //create tasklist
            int[] index = new int[statusList.Items.Count];
            string[] filename = new string[statusList.Items.Count];
            int[] nonces = new int[statusList.Items.Count];
            int count = 0;
            foreach (ListViewItem x in statusList.Items)
            {
                index[count] = x.Index;
                filename[count] = x.Text;
                nonces[count] = Convert.ToInt32(x.SubItems[1].Text);
                count++;
            }

            //start Conversion as Thread to keep UI responsive                
            new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            if (outputDir.Text == "")
            {
                Conversion(index, filename, nonces);
            }
            else
            {
                if (fastmode.Checked)
                {
                    Conversion2(index, filename, nonces);
                }
                else
                {
                    Conversion(index, filename, nonces);
                }
            }
            enableControl(btnConversion);
            enableControl(outputDir);
            enableControl(btnBrowse);
            enableControl(memoryLimit);
        }).Start();
        }

        private void Conversion(int[] index, string[] filename, int[] nonces)
        {
            // calc maximum nonces to read (limit)
            int limit = Convert.ToInt32(memoryLimit.Value - 1) * 8192;
            //loop all tasks
            for (int i = 0; i < index.Length; i++)
            {
                if (halt)
                {
                    setStatus(index[i], 2, "Halt requested. Skipped!");
                    continue;
                }
                if (stop)
                {
                    setStatus(index[i], 2, "Skipped file!");
                    continue;
                }
                if (log.Checked) logIT("Processing file: "+filename[i]);
                DateTime start = DateTime.Now;
                TimeSpan elapsed;
                TimeSpan togo;
                //allocate memory
                if (log.Checked) logIT("Allocating Memory...");
                Scoop scoop1 = new Scoop(Math.Min(nonces[i], limit));  //space needed for one partial scoop
                Scoop scoop2 = new Scoop(Math.Min(nonces[i], limit));  //space needed for one partial scoop
                //Create and open Reader/Writer
                if (log.Checked) logIT("Opening File(s)...");
                ScoopReadWriter scoopReadWriter;
                if (outputDir.Text == "")  //inline
                {
                    scoopReadWriter = new ScoopReadWriter(filename[i]);
                }
                else
                {
                    scoopReadWriter = new ScoopReadWriter(filename[i], outputDir.Text + "\\" + Path.GetFileName(filename[i]).Replace(nonces[i].ToString() + "_" + nonces[i].ToString(), nonces[i].ToString()));
                }
                if (ddio) {
                    if (log.Checked) logIT("Direct I/O disabled by default.");
                    scoopReadWriter.Open(!ddio);
                }
                else
                {
                    //checks if direct I/O would fail
                    int SectorsPerCluster;
                    int BytesPerSector;
                    int NumberOfFreeClusters;
                    int TotalNumberOfClusters; 
                    FileInfo file = new FileInfo(filename[i]);
                    DriveInfo drive = new DriveInfo(file.Directory.Root.FullName);
                    GetDiskFreeSpace(drive.Name, out SectorsPerCluster, out BytesPerSector, out NumberOfFreeClusters, out TotalNumberOfClusters);

                    if (nonces[i] % (BytesPerSector/64)!= 0)
                    {
                        if (log.Checked) logIT("Direct I/O disabled.");
                        scoopReadWriter.Open(false);
                    }
                    else
                    {
                        if (log.Checked) logIT("Direct I/O enabled.");
                        scoopReadWriter.Open(true);
                    }
                }
                
                //skip file if opening fails
                if (!scoopReadWriter.isOpen())
                {
                    if (log.Checked) logIT("Error: Can_t open file, skipped.");
                    setStatus(index[i], 2, "Skipped! Error opening file.");
                    continue;
                }

                //prealloc disk space
                if (outputDir.Text != "")
                {
                    if (!scoopReadWriter.PreAlloc(nonces[i])){
                        if (log.Checked) logIT("Error allocating disk space.");
                        setStatus(index[i], 2, "Skipped! Error allocating disk space.");
                        continue;
                    }
                }

                //loop scoop pairs
                for (int y = startscoop; y < 2048; y++)
                {
                    if (stop) break;
                    setStatus(index[i], 2, "Processing Scoop Pairs " + (y + 1).ToString() + "/2048");
                    Application.DoEvents();
                    //loop partial scoop
                    for (int z = 0; z < nonces[i]; z += limit)
                    {
                        if (log.Checked) logIT("Processing Scoop Pair: "+y.ToString());
                        stop = stop || !scoopReadWriter.ReadScoop(y, nonces[i], z, scoop1, Math.Min(nonces[i] - z, limit));
                        if (stop)
                        {
                            if (log.Checked) logIT("ERROR! Error reading front scoop : " + y.ToString());
                            setStatus(index[i], 2, "ERROR! Error reading front scoop : "+y.ToString());
                            break;
                        }
                        stop = stop || !scoopReadWriter.ReadScoop(4095 - y, nonces[i], z, scoop2, Math.Min(nonces[i] - z, limit));
                        if (stop)
                        {
                            if (log.Checked) logIT("ERROR! Error reading back scoop : " + y.ToString());
                            setStatus(index[i], 2, "ERROR! Error reading back scoop : " + y.ToString());
                            break;
                        }
                        Poc1poc2shuffle(scoop1, scoop2, Math.Min(nonces[i] - z, limit));
                        stop = stop || !scoopReadWriter.WriteScoop(4095 - y, nonces[i], z, scoop2, Math.Min(nonces[i] - z, limit));
                        if (stop)
                        {
                            if (log.Checked) logIT("ERROR! Error writing back scoop : " + y.ToString());
                            setStatus(index[i], 2, "ERROR! Error writing back scoop : " + y.ToString());
                            break;
                        }
                        stop = stop || !scoopReadWriter.WriteScoop(y, nonces[i], z, scoop1, Math.Min(nonces[i] - z, limit));
                        if (stop)
                        {
                            if (log.Checked) logIT("ERROR! Error writing front scoop : " + y.ToString());
                            setStatus(index[i], 2, "ERROR! Error writing front scoop : " + y.ToString());
                            break;
                        }
                    }
                    //update status
                    if (!stop)
                    {
                        elapsed = DateTime.Now.Subtract(start);
                        togo = TimeSpan.FromTicks(elapsed.Ticks / (y - startscoop + 1) * (2048 - y - 1));
                        setStatus(index[i], 3, Math.Round((double)(y + 1) / 2048 * 100).ToString() + "%");
                        setStatus(index[i], 4, timeSpanToString(elapsed));
                        setStatus(index[i], 5, timeSpanToString(togo));
                    }
                }
                // close reader/writer
                scoopReadWriter.Close();
                if (!stop)
                {
                    if (log.Checked) logIT("Renaming File...");
                    // rename file
                    if (outputDir.Text == "")
                        System.IO.File.Move(filename[i], filename[i].Replace(nonces[i].ToString() + "_" + nonces[i].ToString(), nonces[i].ToString()));
                    // update status
                    if (log.Checked) logIT("Plot successfully converted.");
                    setStatus(index[i], 2, "Plot successfully converted.");
                }
                //free memory
                if (log.Checked) logIT("Releasing Memory.");
                scoop1 = null;
                scoop2 = null;
                GC.Collect();
            }
        }

        //improved Conversion
        private void Conversion2(int[] index, string[] filename, int[] nonces)
        {
            // calc maximum nonces to read (limit)
            int limit = Convert.ToInt32(memoryLimit.Value - 1) * 4096;
            //loop all tasks
            for (int i = 0; i < index.Length; i++)
            {
                DateTime start = DateTime.Now;
                TimeSpan elapsed;
                TimeSpan togo;
                //allocate memory
                Scoop scoop1 = new Scoop(Math.Min(nonces[i], limit));  //space needed for one partial scoop
                Scoop scoop2 = new Scoop(Math.Min(nonces[i], limit));  //space needed for one partial scoop
                Scoop scoop3 = new Scoop(Math.Min(nonces[i], limit));  //space needed for one partial scoop
                Scoop scoop4 = new Scoop(Math.Min(nonces[i], limit));  //space needed for one partial scoop     

                plotfile src = parsePlotFileName(filename[i]);
                plotfile tar = parsePlotFileName(outputDir.Text + "\\" + Path.GetFileName(filename[i]).Replace(nonces[i].ToString() + "_" + nonces[i].ToString(), nonces[i].ToString()));


                //Create and open Reader/Writer
                ScoopReadWriterX scoopReadWriter1;
                scoopReadWriter1 = new ScoopReadWriterX(filename[i]);
                scoopReadWriter1.OpenR();

                ScoopReadWriterX scoopReadWriter2;
                scoopReadWriter2 = new ScoopReadWriterX(outputDir.Text + "\\" + Path.GetFileName(filename[i]).Replace(nonces[i].ToString() + "_" + nonces[i].ToString(), nonces[i].ToString()));
                scoopReadWriter2.OpenW();

                //prealloc disk space
                scoopReadWriter2.PreAlloc(nonces[i]);


                //create taskplan
                int loops = (int)Math.Ceiling((double)(nonces[i]) / limit);
                TaskInfo[] masterplan = new TaskInfo[2048 * loops];

                //create masterplan
                for (int y = 0; y < 2048; y++)
                {
                    //loop partial scoop
                    int zz = 0;
                    for (int z = 0; z < nonces[i]; z += limit)
                    {
                        masterplan[y * loops + zz] = new TaskInfo();
                        masterplan[y * loops + zz].reader = scoopReadWriter1;
                        masterplan[y * loops + zz].writer = scoopReadWriter2;
                        masterplan[y * loops + zz].y = y;
                        masterplan[y * loops + zz].z = z;
                        masterplan[y * loops + zz].x = y * loops + zz;
                        masterplan[y * loops + zz].limit = limit;
                        masterplan[y * loops + zz].src = src;
                        masterplan[y * loops + zz].tar = tar;
                        masterplan[y * loops + zz].scoop1 = scoop1;
                        masterplan[y * loops + zz].scoop2 = scoop2;
                        masterplan[y * loops + zz].scoop3 = scoop3;
                        masterplan[y * loops + zz].scoop4 = scoop4;
                        masterplan[y * loops + zz].shuffle = true;
                        masterplan[y * loops + zz].end = masterplan.LongLength;
                        zz += 1;
                    }
                }


                //work masterplan
                //perform first read
                Th_read(masterplan[0]);

                autoEvents = new AutoResetEvent[]
                {
                new AutoResetEvent(false),
                new AutoResetEvent(false)
                };
                //perform reads and writes parallel
                for (long x = 1; x < masterplan.LongLength; x++)
                {
                    setStatus(index[i], 2, "Processing Scoop Pairs " + (masterplan[x].y + 1).ToString() + "/2048");
                    Application.DoEvents();

                    ThreadPool.QueueUserWorkItem(new WaitCallback(Th_write), masterplan[x - 1]);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Th_read), masterplan[x]);
                    WaitHandle.WaitAll(autoEvents);

                    //update status
                    elapsed = DateTime.Now.Subtract(start);
                    togo = TimeSpan.FromTicks(elapsed.Ticks / (masterplan[x].y + 1) * (2048 - masterplan[x].y - 1));
                    setStatus(index[i], 3, Math.Round((double)(masterplan[x].y + 1) / 2048 * 100).ToString() + "%");
                    setStatus(index[i], 4, timeSpanToString(elapsed));
                    setStatus(index[i], 5, timeSpanToString(togo));
                }
                //perform last write
                Th_write(masterplan[masterplan.LongLength - 1]);


                // close reader/writer
                scoopReadWriter1.Close();
                scoopReadWriter2.Close();


                // update status
                setStatus(index[i], 2, "Plot successfully converted.");

                //free memory
                scoop1 = null;
                scoop2 = null;
                scoop3 = null;
                scoop4 = null;
                GC.Collect();
            }
        }

        public static void Th_read(object stateInfo)
        {
            TaskInfo ti = (TaskInfo)stateInfo;

            //determine cache cycle and front scoop back scoop cycle to alternate
            if (ti.x % 2 == 0)
            {
                ti.reader.ReadScoop(ti.y, ti.src.nonces, ti.z, ti.scoop1, Math.Min((int)ti.src.nonces - ti.z, ti.limit));
                ti.reader.ReadScoop(4095 - ti.y, ti.src.nonces, ti.z, ti.scoop2, Math.Min((int)ti.src.nonces - ti.z, ti.limit));
                if (ti.shuffle) Poc1poc2shuffle(ti.scoop1, ti.scoop2, Math.Min((int)ti.src.nonces - ti.z, ti.limit));
            }
            else
            {
                ti.reader.ReadScoop(4095 - ti.y, ti.src.nonces, ti.z, ti.scoop4, Math.Min((int)ti.src.nonces - ti.z, ti.limit));
                ti.reader.ReadScoop(ti.y, ti.src.nonces, ti.z, ti.scoop3, Math.Min((int)ti.src.nonces - ti.z, ti.limit));
                if (ti.shuffle) Poc1poc2shuffle(ti.scoop3, ti.scoop4, Math.Min((int)ti.src.nonces - ti.z, ti.limit));
            }
            if (ti.x != 0) autoEvents[0].Set();

        }

        public static void Th_write(object stateInfo)
        {
            TaskInfo ti = (TaskInfo)stateInfo;
            if (ti.x % 2 == 0)
            {
                ti.writer.WriteScoop(ti.y, ti.tar.nonces, ti.z + ti.src.start - ti.tar.start, ti.scoop1, Math.Min((int)ti.src.nonces - ti.z, ti.limit));
                ti.writer.WriteScoop(4095 - ti.y, ti.tar.nonces, ti.z + ti.src.start - ti.tar.start, ti.scoop2, Math.Min((int)ti.src.nonces - ti.z, ti.limit));
            }
            else
            {
                ti.writer.WriteScoop(4095 - ti.y, ti.tar.nonces, ti.z + ti.src.start - ti.tar.start, ti.scoop4, Math.Min((int)ti.src.nonces - ti.z, ti.limit));
                ti.writer.WriteScoop(ti.y, ti.tar.nonces, ti.z + ti.src.start - ti.tar.start, ti.scoop3, Math.Min((int)ti.src.nonces - ti.z, ti.limit));
            }
            //Thread.Sleep(2000);
            if (ti.x != (ti.end - 1))
                autoEvents[1].Set();
        }

        //prettyprint timespan
        private static string timeSpanToString(TimeSpan timeSpan)
        {
            if (timeSpan.ToString().LastIndexOf(".") > -1)
            {
                return timeSpan.ToString().Substring(0, timeSpan.ToString().LastIndexOf("."));
            }
            else
            {
                return timeSpan.ToString();
            }
        }

        private void setStatus(int ix1, int ix2, string value)
        {
            if (statusList.InvokeRequired)
            {
                statusList.Invoke(new MethodInvoker(() => { setStatus(ix1, ix2, value); }));
            }
            else
            {
                statusList.Items[ix1].SubItems[ix2].Text = value;
            }
        }

        private void enableControl(Control btn)
        {
            if (btn.InvokeRequired)
            {
                btn.Invoke(new MethodInvoker(() => { enableControl(btn); }));
            }
            else
            {
                btn.Enabled = true;
            }
        }

        private static void Poc1poc2shuffle(Scoop scoop1, Scoop scoop2, int limit)
        {
            byte buffer;
            for (int i = 0; i < limit; i++)
            {
                for (int j = 32; j < 64; j++)
                {
                    buffer = scoop1.byteArrayField[64 * i + j];
                    scoop1.byteArrayField[64 * i + j] = scoop2.byteArrayField[64 * i + j];
                    scoop2.byteArrayField[64 * i + j] = buffer;
                }
            }
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    addFile(file);
                }
            }
        }

        private void addDir(string dir)
        {
            foreach (string file in System.IO.Directory.GetFiles(dir, "*"))
            {
                addFile(file);
            }
        }

        private void addFile(string file)
        {
        if (isOptimizedPOC1PlotFileName(file) && !plotFileList.Items.ContainsKey(file))
        {
            plotfile temp = parsePlotFileName(file);
                long length;
                try
                {
                    length = new System.IO.FileInfo(file).Length;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message,"Error Opening File", MessageBoxButtons.OK);
                    return;
                }
            ListViewItem item = new ListViewItem(file);
            item.Text = file;
            item.Name = file;
            item.SubItems.Add(temp.id.ToString());
            item.SubItems.Add(temp.start.ToString());
            item.SubItems.Add((temp.start + Convert.ToUInt32(temp.nonces) - 1).ToString());
            item.SubItems.Add(temp.nonces.ToString());
            item.SubItems.Add(temp.stagger.ToString());
            item.SubItems.Add(prettyBytes(length));
            item.SubItems.Add(length == temp.nonces * 4096 * 32 * 2 ? "Ok." : "Error: Actual file size not matching implied file size!");
            plotFileList.Items.Add(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                outputDir.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnConversion.Enabled) {
                if (outputDir.Text == "") {
                    var window = MessageBox.Show("Inline Conversion in progress! Closing the app will destroy your plot file. Are you sure?\n (Yes = Close Converter, No = Stop after current file is converted, Cancel=Continue)", "Warning!", MessageBoxButtons.YesNoCancel);
                    halt = (window == DialogResult.No);
                    e.Cancel = (window != DialogResult.Yes); }
                else {
                    var window = MessageBox.Show("Conversion in progress! Closing the app will destroy your target plot file. Are you sure?\n (Yes = Close Converter, No = Stop after current file is converted, Cancel=Continue)", "Warning!", MessageBoxButtons.YesNoCancel);
                    halt = (window == DialogResult.No);
                    e.Cancel = (window != DialogResult.Yes);
                }
            }
        }
    }

    struct plotfile
    {
        public ulong id;
        public long start;
        public long nonces;
        public long stagger;
    }

    struct TaskInfo
    {
        public ScoopReadWriterX reader;
        public ScoopReadWriterX writer;
        public int y;
        public int z;
        public int x;
        public int limit;
        public plotfile src;
        public plotfile tar;
        public Scoop scoop1;
        public Scoop scoop2;
        public Scoop scoop3;
        public Scoop scoop4;
        public bool shuffle;
        public long end;
    }
}
