using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ThumbnailImageTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button_change_dir_Click(object sender, EventArgs e)
        {
            //选择目录;
            FolderBrowserDialog path = new FolderBrowserDialog();
            if (path.ShowDialog() == DialogResult.OK)
                this.textBox_dirc.Text = path.SelectedPath;

        }

        

        private void button_start_watch_Click(object sender, EventArgs e)
        {
            if (this.textBox_dirc.Text.Trim().Length <= 0)
            {
                MessageBox.Show("请选择监控目录");
                return;
            }

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = this.textBox_dirc.Text;
            //watcher.Filter = ".";
            //watcher.Changed += new FileSystemEventHandler(OnProcess);
            watcher.Created += new FileSystemEventHandler(OnProcess);
            //watcher.Deleted += new FileSystemEventHandler(OnProcess);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);
            
            watcher.EnableRaisingEvents = true;
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
            watcher.IncludeSubdirectories = true;

            //
            AddLog("文件夹监控启动");


        }

        


        //创建一个委托：赋值操作
        private delegate void AddLog_Delegate(string msg);
        public void AddLog(string msg)
        {
            if (this.textBox_logs.InvokeRequired)
            {
                this.textBox_logs.BeginInvoke(
                    new AddLog_Delegate(AddLog),
                    new Object[] { msg });
                return;
            }
            try
            {
                if (this.textBox_logs.Lines.Length > 50)
                    this.textBox_logs.Text = "";

                this.textBox_logs.AppendText("\n");
                this.textBox_logs.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss -> "));
                this.textBox_logs.AppendText(msg);

            }
            catch (Exception exp)
            {
                //捕捉错误;
                //this.CatchException(exp);
            }

        }

        private  void OnProcess(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                OnCreated(source, e);
            }
            //else if (e.ChangeType == WatcherChangeTypes.Changed)
            //{
            //    OnChanged(source, e);
            //}
            //else if (e.ChangeType == WatcherChangeTypes.Deleted)
            //{
            //    OnDeleted(source, e);
            //}

        }
        private  void OnCreated(object source, FileSystemEventArgs e)
        {
            //Console.WriteLine("文件新建事件处理逻辑 {0}  {1}  {2}", e.ChangeType, e.FullPath, e.Name);
            CreateThumbnailImage(e.FullPath);
            
        }
        private  void OnChanged(object source, FileSystemEventArgs e)
        {
            //Console.WriteLine("文件改变事件处理逻辑{0}  {1}  {2}", e.ChangeType, e.FullPath, e.Name);
            //CreateThumbnailImage(e.FullPath, e.Name);
        }

        private  void OnDeleted(object source, FileSystemEventArgs e)
        {
           // Console.WriteLine("文件删除事件处理逻辑{0}  {1}   {2}", e.ChangeType, e.FullPath, e.Name);
        }

        private  void OnRenamed(object source, RenamedEventArgs e)
        {
            //Console.WriteLine("文件重命名事件处理逻辑{0}  {1}  {2}", e.ChangeType, e.FullPath, e.Name);
        }

        private  void CreateThumbnailImage(string fullPath)
        {
            try
            {
                //如果文件不存在;
                if (!File.Exists(fullPath))
                    return;
                //string fullPath = @"\WebSite1\Default.aspx";
                string filename = System.IO.Path.GetFileName(fullPath);//文件名 “Default.aspx”
                string extension = System.IO.Path.GetExtension(fullPath);//扩展名 “.aspx”
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fullPath);// 没有扩展名的文件名 “Default”
                string directoryName = System.IO.Path.GetDirectoryName(fullPath);

                //判断是否为缩率图;
                if (fileNameWithoutExtension.EndsWith("_small"))
                    return;

                if (extension.ToLower() == ".jpg"
                    || extension.ToLower() == ".bmp"
                    || extension.ToLower() == ".png"
                    || extension.ToLower() == ".gif")
                {
                    //生成缩略图图l
                    Image img = Image.FromFile(fullPath);
                    if (img != null)
                    {

                        string ThumbnailImagePath = directoryName + "\\" + fileNameWithoutExtension + "_small" + extension;
                        GetReducedImage2(128, 128, img, ThumbnailImagePath);

                        //
                        AddLog("创建缩率图：" + ThumbnailImagePath);

                         //释放
                        img.Dispose();
                        img = null;
                    }
                   
                }
            }
            catch (Exception)
            {
                //throw;
            }
            
        }

        /// <summary>
        /// 生成缩略图重载方法，返回缩略图的Image对象
        /// </summary>
        /// <param name="width">缩略图的宽度</param>
        /// <param name="height">缩略图的高度</param>
        /// <param name="imageFrom">原Image对象</param>
        /// <returns>缩略图的Image对象</returns>
        public void GetReducedImage2(int width, int height, Image imageFrom, string fileSavePath)
        {
            // 源图宽度及高度 
            int imageFromWidth = imageFrom.Width;
            int imageFromHeight = imageFrom.Height;
            try
            {
                // 生成的缩略图实际宽度及高度.如果指定的高和宽比原图大，则返回原图；否则按照指定高宽生成图片
                if (width >= imageFromWidth && height >= imageFromHeight)
                {
                    //return imageFrom;
                    width = imageFromWidth;
                    height = imageFromHeight;
                }

                //
                Size newSize = GetImageSize(imageFrom, 128, 128);
                Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(() => { return false; });
                //调用Image对象自带的GetThumbnailImage()进行图片缩略
                Image reducedImage = imageFrom.GetThumbnailImage(newSize.Width, newSize.Height, callb, IntPtr.Zero);


                Bitmap B = new Bitmap(reducedImage.Width, reducedImage.Height); //新建一个理想大小的图像文件  
                Graphics g = Graphics.FromImage(B);//实例一个画板的对象,就用上面的图像的画板  
                g.DrawImage(reducedImage, 0, 0);//把目标图像画在这个图像文件的画板上  
                B.Save(fileSavePath);//通过这个图像来保存 

                //释放
                B.Dispose();
                B = null;

                reducedImage.Dispose();
                reducedImage = null;


                //将图片以指定的格式保存到到指定的位置
                //reducedImage.Save(fileSavePath);
            }
            catch (Exception exp)
            {
                //抛出异常
                AddLog("转换失败，请重试：" + exp.Message);
                throw new Exception("转换失败，请重试！");
            }
        }

        /// <summary>  
        ///     根据设定的大小返回图片的大小，考虑图片长宽的比例问题  
        /// </summary>  
        public  Size GetImageSize(Image picture, int width, int height)
        {
            if (picture == null || width < 1 || height < 1)
                return Size.Empty;

            var imageSize = new Size(width, height);
            var heightRatio = (double)picture.Height / picture.Width;
            var widthRatio = (double)picture.Width / picture.Height;
            var desiredHeight = imageSize.Height;
            var desiredWidth = imageSize.Width;
            imageSize.Height = desiredHeight;

            if (widthRatio > 0)
                imageSize.Width = Convert.ToInt32(imageSize.Height * widthRatio);

            if (imageSize.Width > desiredWidth)
            {
                imageSize.Width = desiredWidth;
                imageSize.Height = Convert.ToInt32(imageSize.Width * heightRatio);
            }

            return imageSize;
        }

        private void button_init_image_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否开始批量生成文件夹内的图片缩略图？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            if (this.textBox_dirc.Text.Trim().Length <= 0)
            {
                MessageBox.Show("请选择监控目录");
                return;
            }
            DirectoryInfo dirinfo = new DirectoryInfo(this.textBox_dirc.Text.Trim());
            FindFile(dirinfo);
        }

        private void FindFile(DirectoryInfo di)
        {
            FileInfo[] fis = di.GetFiles();
            for (int i = 0; i < fis.Length; i++)
            {
                //Console.WriteLine("文件：" + fis[i].FullName);
                CreateThumbnailImage(fis[i].FullName);
                System.Threading.Thread.Sleep(30);
            }
            DirectoryInfo[] dis = di.GetDirectories();
            for (int j = 0; j < dis.Length; j++)
            {
                //Console.WriteLine("目录：" + dis[j].FullName);
                FindFile(dis[j]);
            }
        }
    }
}
