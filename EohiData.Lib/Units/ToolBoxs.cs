using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EohiData.Lib.Units
{
    public static class ToolBoxs
    {
        /// <summary>
        /// 在指定的字符串列表CnStr中检索符合拼音索引字符串
        /// </summary>
        /// <param name="CnStr">汉字字符串</param>
        /// <returns>相对应的汉语拼音首字母串</returns>
        public static string GetSpellCode(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
                myStr += getSpell(strText.Substring(i, 1));

            return myStr;
        }

        /// <summary>
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母
        /// </summary>
        /// <param name="CnChar">单个汉字</param>
        /// <returns>单个大写字母</returns>
        private static string getSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };

                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25)
                    {
                        max = areacode[i + 1];
                    }
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(97 + i) });
                    }
                }
                return "*";
            }
            else return cnChar;
        }

        /// <summary>
        /// 加载框
        /// </summary>
        public static FrmLoading FrmLoad { get; set; }
        //加载进度条
        public static System.Windows.Forms.ToolStripProgressBar ToolPro { get; set; }
        //消息提示
        public static System.Windows.Forms.ToolStripLabel ToolMsg { get; set; }
    }
}
