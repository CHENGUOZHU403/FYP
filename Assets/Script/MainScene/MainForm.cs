//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using System.Runtime.InteropServices;
//using System.Windows.Forms;

//namespace BrightnessAdjustmentApp

////public class MainForm : MonoBehaviour
//{
//    public partial class MainForm : Form
//    {
//        // 調用操作系統API來設置亮度
//        [DllImport("user32.dll")]
//        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
//
//        // 消息代碼，用於設置亮度
//        private const int WM_SYSCOMMAND = 0x0112;
//        private const int SC_MONITORPOWER = 0xF170;
//        private const int MONITOR_ON = -1;
//        private const int MONITOR_OFF = 2;
//
//        public MainForm()
//        {
//            InitializeComponent();
//        }
//
//        // 滑動條值改變時的事件處理方法
//        private void trackBarBrightness_Scroll(object sender, EventArgs e)
//        {
//            int brightness = trackBarBrightness.Value;
//
//            // 設置亮度
//            SetBrightness(brightness);
//        }
//
//        // 設置亮度的方法
//        private void SetBrightness(int brightness)
//        {
//            // 檢查範圍是0-100
//            if (brightness < 0)
//                brightness = 0;
//            else if (brightness > 100)
//                brightness = 100;
//
//            // 計算亮度所需的值，視調整情況而定
//            int value = (brightness * 65535) / 100;
//
//            // 呼叫操作系統API設置亮度
//            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MONITORPOWER, value);
//        }
//    }
//}
//