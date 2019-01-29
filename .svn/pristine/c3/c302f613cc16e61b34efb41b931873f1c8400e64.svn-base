//需要添加两个引用：System.Web、System.Drawing
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EohiDataServerApi.CommonUtil
{
    public class ValidateCode2
    {
        public ValidateCode2()
        {
        }

        public void GetRegionCode()
        {
            Encoding gb = Encoding.GetEncoding("gb2312");

            //调用函数产生4个随机中文汉字编码 
            var Code = CreateRegionCode(4);


            var code = CreateValidateGraphic(Code[0], Code[1], Code[2], Code[3]);
            string str = Convert.ToBase64String(code);
            //ViewBag.img = "data:image/png;base64," + str;
            //Session["RegionCode"] = Code[0] + Code[1] + Code[2] + Code[3];
        }
        /// <summary>
        /// 产生随机数字加英文
        /// </summary>
        /// <param name="strlength"></param>
        /// <returns></returns>
        public  List<string> CreateRegionCode(int strlength)
        {
            List<string> Codes = new List<string>();
            Random rom = new Random();
            char[] allcheckRandom ={'3','4','5','6','7','8','9',
                                       'A','B','C','D','E','F','G','H','J','K','L','M','N','P','Q','R','S','T','U','V','W',
                                       'X','Y','a','b','c','d','e','f','g','h','j','k','l','m','n','p','q',
                                       'r','s','t','u','v','w','x','y'};
            char[] Randomcode;
            for (int i = 0; i < strlength; i++)
            {
                Codes.Add(allcheckRandom[rom.Next(allcheckRandom.Length)].ToString());
            }
            return Codes;

        }

        /// <summary>
        /// 产生常用汉字
        /// </summary>
        /// <param name="strlength"></param>
        /// <returns></returns>
        public static object[] CreateRegionCodeFrequently(int strlength)
        {
            object[] bytes = new object[strlength];
            Random rm = new Random();
            Encoding gb = Encoding.GetEncoding("gb2312");

            for (int i = 0; i < strlength; i++)
            {
                // 获取区码(常用汉字的区码范围为16-55)  
                int regionCode = rm.Next(16, 56);
                // 获取位码(位码范围为1-94 由于55区的90,91,92,93,94为空,故将其排除)  
                int positionCode;
                if (regionCode == 55)
                {
                    // 55区排除90,91,92,93,94  
                    positionCode = rm.Next(1, 90);
                }
                else
                {
                    positionCode = rm.Next(1, 95);
                }

                // 转换区位码为机内码  
                int regionCode_Machine = regionCode + 160;// 160即为十六进制的20H+80H=A0H  
                int positionCode_Machine = positionCode + 160;// 160即为十六进制的20H+80H=A0H  

                // 转换为汉字  
                byte[] Bytes = new byte[] { (byte)regionCode_Machine, (byte)positionCode_Machine };
                bytes.SetValue(Bytes, i);
            }

            return bytes;
        }

        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="validateCode"></param>
        /// <returns></returns>
        public byte[] CreateValidateGraphic(string validateCode, string validateCode1, string validateCode2, string validateCode3)
        {
            Bitmap image = new Bitmap(200, 50);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 30; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.FromArgb(random.Next())), x1, x2, y1, y2);
                }
                Font font = new Font("Arial", 19, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), GetRandomColor(), GetRandomColor(), 1.2f, true);
                LinearGradientBrush brush1 = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), GetRandomColor(), GetRandomColor(), 1.2f, true);
                LinearGradientBrush brush2 = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), GetRandomColor(), GetRandomColor(), 1.2f, true);
                LinearGradientBrush brush3 = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), GetRandomColor(), GetRandomColor(), 1.2f, true);

                g.DrawString(validateCode, font, brush, 40, 16);
                g.DrawString(validateCode1, font, brush1, 70, 10);
                g.DrawString(validateCode2, font, brush2, 100, 14);
                g.DrawString(validateCode3, font, brush3, 130, 15);
                //画图片的前景干扰线
                for (int i = 0; i < 50; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);

                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }




        /// <summary>
        /// 产生随机颜色
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            //  对于C#的随机数，没什么好说的
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);

            //  为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;

            return System.Drawing.Color.FromArgb(int_Red, int_Green, int_Blue);
        }
    }
}


