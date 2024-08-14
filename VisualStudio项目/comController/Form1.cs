using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace comController
{
    public partial class Form1 : Form
    {
        private string portName = "COM5";
        private int baudRate = 9600;
        private Parity parity = Parity.Odd;//奇校验
        private int dataBits = 8;
        private StopBits stopBits = StopBits.One;//停止位

        // 串口对象
        private SerialPort sp = null;//创建一个串口对象
        // 读取数据线程对象
        private Thread dataReceiveThread = null;//创建一个线程
        private Thread writeReceived = null;//创建一个线程,用于写入接收到的数据
        // 是否接收数据
        private bool canRecieveMsg = true;
        // 接收到的数据
        public string strReceived;//接收到的数据
        public byte[] bytesReceived;//接收到的字节数组
        public int bytesLength;//接收到的字节数组长度
        private bool IsOpenSerial = false;

        public string receivedData = string.Empty;


        [System.Runtime.InteropServices.DllImport("user32")]//引入user32.dll鼠标库
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        //移动鼠标 
        const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public int moveSpeed = 10;//鼠标移动速度
        public int moveSleep = 30;//鼠标移动延时

        public int keybdSleep = 15;//键盘输入延时

        public int controlMode = 0;//控制模式，0为鼠标，1为键盘

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)//窗体加载时
        {
            stateTEXT.Text = "COM5";
            textBox1.Text = "9600";

            sp = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            strReceived = string.Empty;
        }

        public void ClosePort()
        {
            try
            {
                sp.Close();
                Console.WriteLine("close success");
                stateTEXT.Text = "关闭成功";
                //Debug.Log("close success");
                dataReceiveThread.Abort();
                writeReceived.Abort();

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Debug.Log(ex.Message);
            }

        }

        private void whileRunning()
        {
            while (true)
            {
                if (IsOpenSerial)
                {
                    //textBox1.Text = receivedData;
                    //
                    //handleReceivedData(strReceived);
                }
            }
        }
        private void connect_Click(object sender, EventArgs e)
        {
            sp.ReadTimeout = 100;
            try
            {
                sp.Open();
                stateTEXT.Text = "连接成功";
                //Debug.Log("open success");

                // 实例化读取数据线程
                this.dataReceiveThread = new Thread(new ThreadStart(DataReceiveFunction));//实例化一个线程用于接收串口数据
                this.dataReceiveThread.IsBackground = true;//设置为后台线程
                this.dataReceiveThread.Start();//启动线程

                this.writeReceived = new Thread(new ThreadStart(whileRunning));
                this.writeReceived.IsBackground = true;
                this.writeReceived.Start();

                IsOpenSerial = true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Debug.Log(ex.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        void DataReceiveFunction()
        {
            try
            {
                while (canRecieveMsg)
                {
                    // 设定读取间隔
                    Thread.Sleep(25);//25ms读取一次
                                     //Thread.Sleep(33);
                    if (!sp.IsOpen)
                        return;
                    int datalength = sp.BytesToRead;
                    if (datalength == 0)
                    {
                        bytesLength = 0;
                        continue;
                    }

                    byte[] bytes = new byte[datalength];
                    bytesReceived = new byte[datalength];
                    bytesLength = datalength;
                    sp.Read(bytes, 0, datalength);
                    bytes.CopyTo(bytesReceived, 0);
                    //strReceived = System.Text.Encoding.Default.GetString(bytes);//将字节数组转换为字符串

                    textBox1.Text = BitConverter.ToString(bytesReceived);
                    if(controlMode == 0)
                    {
                        nowMODE.Text = "鼠标模式";
                        mousecontroller(bytesReceived);
                    }
                    else if(controlMode == 1)
                    {
                        nowMODE.Text = "键盘模式";
                         keyboardcontroller(bytesReceived);
                    }

                    //receivedData = bytesReceived.ToString();
                }
            }
            catch (System.Exception ex)
            {
                if (ex.GetType() != typeof(ThreadAbortException))
                {
                }
                Console.WriteLine(ex.Message);
                //Debug.Log(ex);
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void DisConnect_Click(object sender, EventArgs e)
        {
            ClosePort();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            canRecieveMsg = false;
            ClosePort();
        }

        private void stateTEXT_TextChanged(object sender, EventArgs e)
        {

        }

        private void mousecontroller(byte[] comIN)//控制鼠标
        {
            if (comIN[0] == 0x05)//左键单击，按下数字键4
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            }
            else if (comIN[0] == 0x02)//右键单击，2
            {
                mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            }
            else if (comIN[0] == 0x04)//鼠标向左移动，7
            {
                mouse_event(MOUSEEVENTF_MOVE, -moveSpeed, 0, 0, 0);//相对当前鼠标位置x轴分别移动50像素
                Thread.Sleep(moveSleep);//延时30ms
            }
            else if (comIN[0] == 0x01)//鼠标向右移动，3
            {
                mouse_event(MOUSEEVENTF_MOVE,  moveSpeed,0, 0, 0);//相对当前鼠标位置x轴分别移动50像素
                Thread.Sleep(moveSleep);//延时30ms
            }
            else if (comIN[0] == 0x07)//鼠标向上移动，6
            {
                mouse_event(MOUSEEVENTF_MOVE, 0, -moveSpeed, 0, 0);//相对当前鼠标位置x轴分别移动50像素
                Thread.Sleep(moveSleep);//延时30ms
            }
            else if (comIN[0] == 0x06)//鼠标向下移动，5
            {
                mouse_event(MOUSEEVENTF_MOVE, 0, moveSpeed, 0, 0);//相对当前鼠标位置x轴分别移动50像素
                Thread.Sleep(moveSleep);//延时30ms
            }
            else if (comIN[0] == 0x00)//模式切换为键盘，8
            {
                controlMode = 1;
                Thread.Sleep(200);//延时200ms
            }
        }

        private void keyboardcontroller(byte[] comIN)//控制键盘
        {
            if (comIN[0] == 0x07)//按下键盘w，6
            {
                SendKeys.SendWait("{w}");
                Thread.Sleep(keybdSleep);//延时15ms
            }
            else if (comIN[0] == 0x05)//按下键盘e,4
            {
                SendKeys.SendWait("{e}");
                Thread.Sleep(keybdSleep);//延时15ms
            }
            else if (comIN[0] == 0x02)//按下键盘r,2
            {
                SendKeys.SendWait("{r}");
                Thread.Sleep(keybdSleep);//延时15ms
            }
            else if (comIN[0] == 0x04)//按下键盘a,7
            {
                SendKeys.SendWait("{a}");
                Thread.Sleep(keybdSleep);//延时15ms
            }
            else if (comIN[0] == 0x06)//按下键盘s,5
            {
                SendKeys.SendWait("{s}");
                Thread.Sleep(keybdSleep);//延时15ms
            }
            else if (comIN[0] == 0x01)//按下键盘d,3
            {
                SendKeys.SendWait("{d}");
                Thread.Sleep(keybdSleep);//延时15ms
            }
            else if (comIN[0] == 0x03)//按下键盘f,1
            {
                SendKeys.SendWait("{f}");
                Thread.Sleep(keybdSleep);//延时15ms
            }
            else if (comIN[0] == 0x00)//模式切换为鼠标，8
            {
                controlMode = 0;
                Thread.Sleep(200);//延时200ms
            }

        }

        private void nowMODE_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

