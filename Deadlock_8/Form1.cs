using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;

namespace Deadlock_8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region Khai bao bien
        List<ProcessNodes> P; //Luu tat ca cac tien trinh
        List<ResourceNodes> RofP; //Luu cac tai nguyen da thuoc ve mot tien trinh nao do
        List<ResourceNodes> AllRes; //Luu tat ca cac tai nguyen
        List<ProcessNodes> pathProcess;//Luu vet duong di, cac tien trinh da di qua
        List<ResourceNodes> pathResouce;//Luu vet duong di, cac tai nguyen da di qua
        List<string> listProcessPass;//Luu ten cac tien trinh da di qua va lui lai, tuc nghia la loai bo khoi vet duong di
        int currentPointInP;
        ProcessNodes nextPeak;
        int viTriDinhGayBatDauDeadlock;
        int currentPointOfStartPeakInP; //xac dinh nut de bat dau
        string KQ;
        int step;
        ResourceNodes resWantNow;
        bool haveDeadlock;
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            P = new List<ProcessNodes>();
            RofP = new List<ResourceNodes>();
            AllRes = new List<ResourceNodes>();
            listProcessPass = new List<string>();
            pathProcess = new List<ProcessNodes>();
            pathResouce = new List<ResourceNodes>();
        }
        //Lam moi/Refresh
        private void button2_Click(object sender, EventArgs e)
        {
            Refresh();
        }
        //Them moi tien trinh
        private void btnAddProcess_Click(object sender, EventArgs e)
        {
            if(checkTxtHaveText(txtProcess.Text.Trim().ToString()))
            {
                if (Contain(txtProcess.Text.Trim().ToString(), P))
                {
                    MessageBox.Show("Tien trinh nay da ton tai");
                }
                else
                {
                    bool existed = false;
                    ProcessNodes Pi = new ProcessNodes(txtProcess.Text.Trim().ToString());
                    if (checkTxtHaveText(txtResouceHave.Text.Trim().ToString()))
                    {
                        string[] resHave = txtResouceHave.Text.Trim().ToString().Split(' ');
                        for(int i =0;i< resHave.Length; i++)
                        {
                            if (Contain(resHave[i], RofP))
                            {
                                MessageBox.Show("Tai nguyen " + RofP[IndexOf(resHave[i], RofP)].Name
                                            + " da thuoc ve tien trinh " + RofP[IndexOf(resHave[i], RofP)].OF.Name);
                                RofP.RemoveRange(RofP.Count - i - 1, i);
                                existed = true;
                                break;
                            }
                            else
                            {
                                if(Contain(resHave[i],AllRes))
                                {
                                    ResourceNodes Ri = AllRes[IndexOf(resHave[i], AllRes)];
                                    Ri.OF = Pi;
                                    Pi.Have.Add(Ri);
                                    RofP.Add(Ri);
                                }
                                else
                                {
                                    ResourceNodes Ri = new ResourceNodes(resHave[i], Pi);
                                    Ri.OF = Pi;
                                    Pi.Have.Add(Ri);
                                    AllRes.Add(Ri);
                                    RofP.Add(Ri);
                                }
                            }
                        }
                        
                    }
                    if (!existed)
                    {
                        if (checkTxtHaveText(txtResourceWant.Text.Trim().ToString()))
                        {
                            string[] resWant = txtResourceWant.Text.Trim().ToString().Split(' ');
                            for (int i = 0; i < resWant.Length; i++)
                            {
                                if (Contain(resWant[i], AllRes))
                                {
                                    ResourceNodes Ri = AllRes[IndexOf(resWant[i], AllRes)];
                                    Ri.WantMe.Add(Pi);
                                    Pi.Want.Add(Ri);
                                }
                                else
                                {
                                    ResourceNodes Ri = new ResourceNodes(resWant[i]);
                                    Ri.OF = Pi;
                                    Pi.Want.Add(Ri);
                                    AllRes.Add(Ri);
                                }
                            }
                        }
                        P.Add(Pi);
                        MessageBox.Show("Success!");
                        txtProcess.Text = "";
                        txtResouceHave.Text = "";
                        txtResourceWant.Text = "";
                        txtProcess.Focus();

                    }
                }
            }
            else
            {
                MessageBox.Show("Insert Process Name");
            }
        }
        private void Refresh()
        {
            if (P != null) P.Clear();
            if (RofP != null) RofP.Clear();
            if (AllRes != null) AllRes.Clear();
            if (pathProcess != null) pathProcess.Clear();
            if (pathResouce != null) pathResouce.Clear();
            if (listProcessPass != null) listProcessPass.Clear();
            haveDeadlock = false;
            currentPointOfStartPeakInP = 0;
            step = 0;
            KQ = "";
        }
        private void btnTakeDataFromFile_Click(object sender, EventArgs e)
        {
            Refresh();
            bool existed = false;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = ofd.FileName;
                string[] lines = File.ReadAllLines(ofd.FileName);
                //lines[0] dung de khai bao cac thu 
                for (int i = 1; i < lines.Length; i = i + 2)
                {
                    string[] have = lines[i].Split(' ');
                    string[] want = lines[i + 1].Split(' ');
                    if (Contain(have[0], P))
                    {
                        MessageBox.Show("Tien trinh " + have[0] + " tai dong " + (i) + " trung voi dong " + (IndexOf(have[0], P) * 2 + 2));
                        existed = true;
                        break;
                    }
                    else if (Contain(want[0], P))
                    {
                        MessageBox.Show("Tien trinh " + want[0] + " tai dong " + (i + 1) + " trung voi dong " + (IndexOf(want[0], P) * 2 + 2));
                        existed = true;
                        break;
                    }
                    else
                    {
                        existed = false;
                        ProcessNodes Pi = new ProcessNodes(have[0]);
                        if (have.Length > 1) //Khong tinh have[0] la ten tien trinh
                        {
                            for(int j = 1; j < have.Length; j++)
                            {
                                if (Contain(have[j], RofP))
                                {
                                    MessageBox.Show("Tai nguyen " + have[j] + " da thuoc ve tien trinh "
                                        + RofP[IndexOf(have[j], RofP)].OF.Name + " tai dong "
                                        + (IndexOf(have[0], P) * 2 - 1));
                                    existed = true;
                                    break;
                                }
                                else
                                {
                                    if (Contain(have[j], AllRes))
                                    {
                                        ResourceNodes Ri = AllRes[IndexOf(have[j], AllRes)];
                                        Ri.OF = Pi;
                                        Pi.Have.Add(Ri);
                                        RofP.Add(Ri);
                                    }
                                    else
                                    {
                                        ResourceNodes Ri = new ResourceNodes(have[j], Pi);
                                        Ri.OF = Pi;
                                        Pi.Have.Add(Ri);
                                        AllRes.Add(Ri);
                                        RofP.Add(Ri);
                                    }
                                }
                            }
                            
                        }
                        if (!existed)
                        {
                            if (want.Length > 1)
                            {
                                for (int j = 1; j < want.Length; j++)
                                {
                                    if (Contain(want[j], AllRes))
                                    {
                                        ResourceNodes Ri = AllRes[IndexOf(want[j], AllRes)];
                                        Ri.WantMe.Add(Pi);
                                        Pi.Want.Add(Ri);
                                    }
                                    else
                                    {
                                        ResourceNodes Ri = new ResourceNodes(want[j]);
                                        Ri.OF = Pi;
                                        Pi.Want.Add(Ri);
                                        AllRes.Add(Ri);
                                    }
                                }
                            }
                            P.Add(Pi);

                        }
                        else
                        {
                            MessageBox.Show("Nap du lieu khong thanh cong!");
                            break;
                        }
                    }
                }
                if (!existed)
                {
                    MessageBox.Show("Doc du lieu thanh cong!");
                }
            }
        }
        #region Cac ham ho tro viec them mot tien trinh
        private bool checkTxtHaveText(string n)
        {
            if (n.Trim() == "" || n.Trim() == null)
                return false;
            return true;
        }
        bool Contain(string id,List<string> vet)
        {
            if (vet == null || vet.Count == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < vet.Count; i++)
                {
                    if (id == vet[i]) return true;
                }
                return false;
            }
        }
        int IndexOf(string id, List<string> vet)
        {
            if (vet == null || vet.Count == 0)
            {
                return -1;
            }
            else
            {
                for (int i = 0; i < vet.Count; i++)
                {
                    if (id == vet[i]) return i;
                }
                return -1;
            }
        }
        bool Contain(string id, List<ProcessNodes> P)
        {
            if (P == null || P.Count == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < P.Count; i++)
                {
                    if (id == P[i].Name)
                        return true;
                }
                return false;
            }

        }
        int IndexOf(string id, List<ProcessNodes> P)
        {
            if (P == null || P.Count == 0)
            {
                return -1;
            }
            else
            {
                for (int i = 0; i < P.Count; i++)
                {
                    if (id == P[i].Name)
                        return i;
                }
                return -1;
            }
        }
        bool Contain(string id, List<ResourceNodes> R)
        {
            if (R == null || R.Count == 0)
                return false;
            else
            {

                for (int i = 0; i < R.Count; i++)
                {
                    if (id == R[i].Name)
                        return true;
                }
                return false;
            }
        }
        int IndexOf(string id, List<ResourceNodes> R)
        {
            if (R == null || R.Count == 0)
                return -1;
            else
            {

                for (int i = 0; i < R.Count; i++)
                {
                    if (id == R[i].Name)
                        return i;
                }
                return -1;
            }
        }
        #endregion
        //Tim deadlock
        private void btnFindDeadlock_Click(object sender, EventArgs e)
        {
            if (!haveDeadlock) // bien xuatHienDeadlock = null/false
            {
                haveDeadlock = false;

            }
            else
            {
                MessageBox.Show(KQ);
            }
            currentPointOfStartPeakInP = 0;

            while (!haveDeadlock && currentPointOfStartPeakInP < P.Count) //Khi chua xuat hien deadlock hoac chua duyet het cac tien trinh
            {
                MessageBox.Show("Chon dinh " + P[currentPointOfStartPeakInP].Name+ " la dinh xuat phat");

                listProcessPass.Clear();
                pathResouce.Clear();
                pathProcess.Clear();
                step = 0;
                pathProcess.Add(P[currentPointOfStartPeakInP]);
                while (!haveDeadlock && step >= 0) //Khi chua xuat hien deadlock hoac chua quay ve vi tri node dau tien va het duong di,
                {
                    currentPointInP = IndexOf(pathProcess[step].Name, P); //
                    // Khi chua xuat hien deadlock hoac tai Mot tien tien trinh nao do van con tai nguyen ma no di toi ( Mong muon )
                    while (!haveDeadlock && currentPointInP >= 0 && P[currentPointInP].CurrentPoint < P[currentPointInP].Want.Count)
                    {
                        resWantNow = P[currentPointInP].ResWantNow(); //lay ten cua tai nguyen ma tien trinh mong muon
                        P[currentPointInP].CurrentPoint++;//Tang con tro toi tai nguyen mong muon le 1
                        pathResouce.Add(resWantNow);   //luu vet tai nguyen
                        if (Contain(resWantNow.Name, RofP)) //Neu tai nguyen do thuoc tien trinh nao do thi di tiep
                        {
                            nextPeak = resWantNow.OF; //Lay ten cua tien trinh tiep theo
                            if (!Contain(nextPeak.Name, pathProcess)) //Kiem tra xem tien trinh tiep theo da di qua truoc do hay chua
                            {
                                if (!Contain(nextPeak.Name, listProcessPass)) //Neu nut nay da tung di qua va bi loai thi khong di qua nua
                                {
                                    pathProcess.Add(nextPeak);//Them vao vet duong di : Khi dinh do chua duoc di qua, luu dinh do vao vet duong di, va tiep tuc tu dinh do di tiep toi cac tai nguyen
                                    step++;
                                    currentPointInP = IndexOf(pathProcess[step].Name, P); //Lay vi tri cua dinh tiep theo do o trong mang P, de tiep tuc xet
                                }
                            }
                            else
                            {
                                viTriDinhGayBatDauDeadlock = IndexOf(nextPeak.Name, pathProcess); //lay vi tri bat dau xuat hien deadlock
                                for (int i = viTriDinhGayBatDauDeadlock; i < pathProcess.Count; i++)
                                {
                                    KQ = KQ + pathProcess[i].Name + " ";
                                }
                                KQ = "Cac tien trinh gay ra deadlock la: " + KQ;
                                MessageBox.Show(KQ);
                                haveDeadlock = true;
                            }
                        }
                        else //Tai nguyen khong thuoc tien trinh nao
                        {
                            pathResouce.RemoveAt(pathResouce.Count - 1);
                        }

                    }
                    if (!haveDeadlock)
                    {
                        listProcessPass.Add(pathProcess[step].Name); //Them nut bi lui lai vao blackList, de loai bo cho luot sau
                        pathProcess.RemoveAt(step); //Lui lai mot buoc, xoa buoc lui lai khoi vet duong di
                        if (step > 0)
                        {
                            pathResouce.RemoveAt(step - 1);
                        }
                        step--;
                    }
                }
                currentPointOfStartPeakInP++;
            }
            if (!haveDeadlock) { MessageBox.Show("Khong co deadlock"); }
        }
        #region Ve cac node thu cong
        private void btnVe_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            VeThuCong();
        }

        Graphics g;
        SolidBrush sbR, sbP;
        SolidBrush sbString;
        Pen pen;
        private void VeThuCong()
        {
            sbR = new SolidBrush(Color.Yellow);
            sbP = new SolidBrush(Color.Cyan);
            sbString = new SolidBrush(Color.Red);
            g = pictureBox1.CreateGraphics();
            pen = new Pen(Color.Black, 5);
            pen.EndCap = LineCap.ArrowAnchor;

            Point start = new Point(20, 20);
            Point end = new Point(20, 20);
            Point chuTHich;

            veNodeResource("R", g, sbR, sbString, start);
            start.X += 100;
            veNodeProcess("A", g, sbP, sbString, start);
            veArrow(g, pen, end, start);

            end = new Point(start.X, start.Y);
            start.Y += 100;
            veNodeResource("S", g, sbR, sbString, start);
            veArrow(g, pen, end, start);

            end = new Point(start.X, start.Y);
            start.X -= 100;
            veNodeProcess("C", g, sbP, sbString, start);
            veArrow(g, pen, start, end);

            start.X += 100; start.Y += 100;
            veNodeProcess("F", g, sbP, sbString, start);
            veArrow(g, pen, start, end);

            end = new Point(start.X, start.Y);
            start.Y += 100;
            veNodeResource("W", g, sbR, sbString, start);
            veArrow(g, pen, start, end);

            end = start;
            start.X += 100; start.Y -= 200;
            veNodeProcess("D", g, sbP, sbString, start);
            veArrow(g, pen, start, end);
            veArrow(g, pen, start, new Point(start.X - 100, start.Y));
            pen.Color = Color.Red;
            end = start;
            start.X += 100;
            veNodeResource("T", g, sbR, sbString, start);
            veArrow(g, pen, end, start);

            pen.Color = Color.Black;
            end = start;
            start.Y -= 100;
            veNodeProcess("B", g, sbP, sbString, start);
            veArrow(g, pen, start, end);

            pen.Color = Color.Red;
            start.X += 100; start.Y += 100;
            veNodeProcess("E", g, sbP, sbString, start);
            veArrow(g, pen, end, start);

            end = start;
            start.Y += 100;
            veNodeResource("V", g, sbR, sbString, start);
            veArrow(g, pen, end, start);

            end = start;
            start.X -= 100;
            veNodeProcess("G", g, sbP, sbString, start);
            veArrow(g, pen, end, start);

            chuTHich = new Point(start.X, start.Y + 100);
            end = start;
            start.X -= 100;
            veNodeResource("U", g, sbR, sbString, start);
            veArrow(g, pen, end, start);
            veArrow(g, pen, start, new Point(start.X, start.Y - 100));

            veNodeProcess("", g, sbP, sbString, chuTHich);
            g.DrawString("Tiến trình", new Font("Arial", 15), sbString, chuTHich.X + 50, chuTHich.Y);
            chuTHich.Y += 50;
            veNodeResource("", g, sbR, sbString, chuTHich);
            g.DrawString("Tài nguyên", new Font("Arial", 15), sbString, chuTHich.X + 50, chuTHich.Y);
            sbR.Dispose();
            sbP.Dispose();
            sbString.Dispose();
            g.Dispose();
        }
        private void veNodeProcess(string name, Graphics g, SolidBrush sb, SolidBrush sbString, Point p)
        {
            g.FillEllipse(sb, p.X, p.Y, 30, 30);
            g.DrawString(name, new Font("Arial", 15), sbString, p.X + 3, p.Y + 3);
        }
        private void veNodeResource(string name, Graphics g, SolidBrush sb, SolidBrush sbString, Point p)
        {
            g.FillRectangle(sb, p.X, p.Y, 30, 30);
            g.DrawString(name, new Font("Arial", 15), sbString, p.X + 3, p.Y + 3);
        }
        #endregion
        Point start;
        private void btnvetientrinh_Click(object sender, EventArgs e)
        {
            sbR = new SolidBrush(Color.Yellow);
            sbP = new SolidBrush(Color.Cyan);
            sbString = new SolidBrush(Color.Red);
            g = pictureBox1.CreateGraphics();
            pen = new Pen(Color.Black, 5);
            pen.EndCap = LineCap.ArrowAnchor;
            int tongSoNutDeadlock = pathProcess.Count - viTriDinhGayBatDauDeadlock;
            int viTri = viTriDinhGayBatDauDeadlock;
           // int demo = 0;
            start = new Point(30, 30);
            List<Nodes> nodes = new List<Nodes>();
            for(int i =0;i<tongSoNutDeadlock;i++)
            {
                nodes.Add(pathProcess[i + viTriDinhGayBatDauDeadlock]);
                nodes.Add(pathResouce[i + viTriDinhGayBatDauDeadlock]);
            }
            //Ve nut dau tien
            nodes[0].Location = start;
            nodes[0].Draw(g);
            #region Chi ve 2 canh
            //for (int i = 1; i < nodes.Count; i++)
            //{
            //    if (i < nodes.Count / 2) //Hang o tren
            //    {
            //        nodes[i].Location.X = nodes[i - 1].Location.X + 100;
            //        nodes[i].Location.Y = nodes[i - 1].Location.Y;
            //        nodes[i].Draw(g);
            //        nodes[i - 1].DrawArrowTo(g, pen, nodes[i]);
            //    }
            //    else if (i == nodes.Count / 2)
            //    {
            //        nodes[i].Location.Y = start.Y + 200;
            //        nodes[i].Location.X = nodes[i - 1].Location.X;
            //        nodes[i].Draw(g);
            //        nodes[i - 1].DrawArrowTo(g, pen, nodes[i]);
            //    }
            //    else if (i < nodes.Count - 1)
            //    {
            //        nodes[i].Location.X = nodes[i - 1].Location.X - 100;
            //        nodes[i].Location.Y = nodes[i - 1].Location.Y;
            //        nodes[i].Draw(g);
            //        nodes[i - 1].DrawArrowTo(g, pen, nodes[i]);
            //    }
            //    else
            //    {
            //        nodes[i].Location.X = nodes[i - 1].Location.X - 100;
            //        nodes[i].Location.Y = nodes[i - 1].Location.Y;
            //        nodes[i].Draw(g);
            //        nodes[i - 1].DrawArrowTo(g, pen, nodes[i]);
            //        nodes[i].DrawArrowTo(g, pen, nodes[0]);
            //    }
            //}
            #endregion
            #region Ve deu 4 canh
            for (int i = 1; i < nodes.Count; i++)
            {
                if (i <= Math.Ceiling(nodes.Count / 4.0)) //Ve canh tren
                {
                    nodes[i].Location.X = nodes[i - 1].Location.X + 100;
                    nodes[i].Location.Y = nodes[i - 1].Location.Y;
                    nodes[i].Draw(g);
                    nodes[i - 1].DrawArrowTo(g, pen, nodes[i]);
                }
                else if (i <= nodes.Count / 2)//Ve canh ben phai
                {
                    nodes[i].Location.Y = nodes[i - 1].Location.Y + 100;
                    nodes[i].Location.X = nodes[i - 1].Location.X;
                    nodes[i].Draw(g);
                    nodes[i - 1].DrawArrowTo(g, pen, nodes[i]);
                }
                else if (i <= Math.Ceiling(3 * nodes.Count / 4.0))//Ve canh duoi
                {
                    nodes[i].Location.X = nodes[i - 1].Location.X - 100;
                    nodes[i].Location.Y = nodes[i - 1].Location.Y;
                    nodes[i].Draw(g);
                    nodes[i - 1].DrawArrowTo(g, pen, nodes[i]);
                    if (i == nodes.Count - 1)
                    {
                        nodes[i].DrawArrowTo(g, pen, nodes[0]);
                    }
                }
                else if (i <= nodes.Count - 1)//Ve canh ben trai
                {
                    nodes[i].Location.X = nodes[i - 1].Location.X;
                    nodes[i].Location.Y = nodes[i - 1].Location.Y - 100;
                    nodes[i].Draw(g);
                    nodes[i - 1].DrawArrowTo(g, pen, nodes[i]);
                    if (i == nodes.Count - 1)
                    {
                        nodes[i].DrawArrowTo(g, pen, nodes[0]);
                    }
                }
            }
            #endregion
        }
        private void veArrow(Graphics g, Pen p, Point From, Point To)
        {

            if (From.X == To.X)//Ve duoc thang doc
            {
                if (From.Y > To.Y)//tu duoi di len
                {
                    From.X += 15;
                    To.X += 15;
                    To.Y += 30;
                    g.DrawLine(p, From, To);
                }
                else //ve tu tren xuong
                {
                    From.X += 15; From.Y += 30; To.X += 15;
                    g.DrawLine(p, From, To);
                }
            }
            else if (From.Y == To.Y) //Ve duoc ngang
            {
                if (From.X < To.X) //Ve tu trai sang phai
                {
                    From.X += 30;
                    From.Y += 15;
                    To.Y += 15;
                    g.DrawLine(p, From, To);
                }
                else
                {
                    From.Y += 15;
                    To.X += 30;
                    To.Y += 15;
                    g.DrawLine(p, From, To);
                }
            }

        }
    }

}
