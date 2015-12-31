using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testWinForm
{
    public partial class 删除 : Form
    {
        #region Pro/属性
        protected string tPath
        {
            get
            {
                return txtTargetPath.Text.Trim();
            }
        }

        protected string bPath
        {
            get
            {
                return txtBackUpPath.Text.Trim();
            }
        }

        protected string sPath
        {
            get
            {
                return txtPath.Text.Trim();
            }
        }
        #endregion 

        public 删除()
        {
            InitializeComponent();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string path = txtPath.Text.ToString().Trim();
            string txtDate = txtLastDate.Text.ToString().Trim();
            DateTime date = Convert.ToDateTime(txtDate);
            DeleteFileWithPostfix(path, "*.config");
            DeleteFile(path, date);
            MessageBox.Show( "删除完成");
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            string targetPath = txtTargetPath.Text.Trim();
            string backupPath = txtBackUpPath.Text.Trim();
            string sourcePath = txtPath.Text.Trim();
            BackUpFile(targetPath, backupPath, sourcePath);
            
        }

        /// <summary>
        /// 删除日期之前的文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="date">最后修改日期</param>
        public void DeleteFile(string path, DateTime date)
        {      
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] dirs = null;
            FileInfo[] fileList = null;
            if (dir.Exists)
            {
                fileList = dir.GetFiles();
                foreach (FileInfo file in fileList)
                {
                    if (file.LastWriteTime < date)
                    {
                        file.Delete();
                    }
                }

                dirs = dir.GetDirectories();
                foreach (DirectoryInfo info in dirs)
                {
                    DeleteFile(info.FullName, date);
                    if (info.GetFiles().Length == 0  && info.GetDirectories().Length == 0)
                    {
                        Directory.Delete(info.FullName);
                    }
                }
            }
            
        }

        //删除指定后缀名的文件
        public void DeleteFileWithPostfix(string path , string postfixName)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {                
                foreach (FileInfo file in dir.GetFiles( postfixName))
                {
                    if (file.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    {
                        file.Attributes = FileAttributes.Normal;
                    }
                    file.Delete();
                }
            }
        }


        public void BackUpFile(string targetPath, string backupPath, string sourcePath)
        {
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            DirectoryInfo targetDir = new DirectoryInfo(targetPath);
            DirectoryInfo[] dirs = null;
            FileInfo[] files = null;
            if (dir.Exists)
            {
                
                files = dir.GetFiles();
                foreach(FileInfo file in files)
                {
                    string name = file.FullName;

                }
                dirs = dir.GetDirectories();

            }
        }

        

        

    }
}
