using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadlock_8
{
    class Nodes
    {
        virtual public void Draw(Graphics g) { }
        public Point Location;
        public string Name { get; set; }
        virtual public void DrawArrowTo(Graphics g, Pen p, ProcessNodes Too) { }
        virtual public void DrawArrowTo(Graphics g, Pen p, ResourceNodes Too) { }
        virtual public void DrawArrowTo(Graphics g, Pen p, Nodes Too) { }
        virtual public void DrawArrowTo(Graphics g, Pen p, Point point) { }
    }
    class ProcessNodes:Nodes
    {
        public int CurrentPoint { get; set; }
        public List<ResourceNodes> Want { get; set; }
        public List<ResourceNodes> Have { get; set; }
        //public Point Location;
        public ProcessNodes()
        {
            Name = "";
            CurrentPoint = 0;
            Want = new List<ResourceNodes>();
            Have = new List<ResourceNodes>();
            Location = new Point();
        }
        public ProcessNodes(string name)
        {
            Name = name;
            CurrentPoint = 0;
            Want = new List<ResourceNodes>();
            Have = new List<ResourceNodes>();
            Location = new Point();
        }
        public ProcessNodes(string name, List<ResourceNodes> have, List<ResourceNodes> want)
        {
            Name = name;
            CurrentPoint = 0;
            Want = want;
            Have = have;
            Location = new Point();
        }
        public ResourceNodes ResWantNow()
        {
            return Want[CurrentPoint];
        }
        override public void Draw(Graphics g)
        {
            SolidBrush sb = new SolidBrush(Color.Cyan);
            SolidBrush sbString = new SolidBrush(Color.Red);
            g.FillEllipse(sb, Location.X, Location.Y, 30, 30);
            g.DrawString(Name, new Font("Arial", 15), sbString, Location.X + 3, Location.Y + 3);
            sb.Dispose();
            sbString.Dispose();
        }
        public void Draw(Graphics g, SolidBrush sb, SolidBrush sbString)
        {

            g.FillEllipse(sb, Location.X, Location.Y, 30, 30);
            g.DrawString(Name, new Font("Arial", 15), sbString, Location.X + 3, Location.Y + 3);
        }
        override public void DrawArrowTo(Graphics g, Pen p, ResourceNodes Too)
        {
            Point To = Too.Location;
            if (Location.X == To.X)//Ve duoc thang doc
            {
                if (Location.Y > To.Y)//tu duoi di len
                {
                    Location.X += 15;
                    To.X += 15;
                    To.Y += 30;
                    g.DrawLine(p, Location, To);
                }
                else //ve tu tren xuong
                {
                    Location.X += 15; Location.Y += 30; To.X += 15;
                    g.DrawLine(p, Location, To);
                }
            }
            else if (Location.Y == To.Y) //Ve duoc ngang
            {
                if (Location.X < To.X) //Ve tu trai sang phai
                {
                    Location.X += 30;
                    Location.Y += 15;
                    To.Y += 15;
                    g.DrawLine(p, Location, To);
                }
                else
                {
                    Location.Y += 15;
                    To.X += 30;
                    To.Y += 15;
                    g.DrawLine(p, Location, To);
                }
            }
        }
        override public void DrawArrowTo(Graphics g, Pen p, Nodes Too)
        {
            Point From = Location;
            Point To = Too.Location;
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
    class ResourceNodes:Nodes
    {
        public ProcessNodes OF { get; set; }
        public List<ProcessNodes> WantMe { get; set; }
        //public Point Location;
        public ResourceNodes()
        {
            Name = " ";
            WantMe = new List<ProcessNodes>();
            
        }
        public ResourceNodes(string name)
        {
            Name = name;
            WantMe = new List<ProcessNodes>();
        }
        public ResourceNodes(string name,ProcessNodes of)
        {
            Name = name;
            OF = of;
            WantMe = new List<ProcessNodes>();
        }
        override public void Draw(Graphics g)
        {
            SolidBrush sb = new SolidBrush(Color.Yellow);
            SolidBrush sbString = new SolidBrush(Color.Red);
            g.FillRectangle(sb, Location.X, Location.Y, 30, 30);
            g.DrawString(Name, new Font("Arial", 15), sbString, Location.X + 3, Location.Y + 3);
            sb.Dispose();
            sbString.Dispose();
        }
        public void Draw(Graphics g, SolidBrush sb, SolidBrush sbString)
        {

            g.FillRectangle(sb, Location.X, Location.Y, 30, 30);
            g.DrawString(Name, new Font("Arial", 15), sbString, Location.X + 3, Location.Y + 3);
        }
        override public void DrawArrowTo(Graphics g, Pen p, ProcessNodes Too)
        {
            
            Point To = Too.Location;
            if (Location.X == To.X)//Ve duoc thang doc
            {
                if (Location.Y > To.Y)//tu duoi di len
                {
                    Location.X += 15;
                    To.X += 15;
                    To.Y += 30;
                    g.DrawLine(p, Location, To);
                }
                else //ve tu tren xuong
                {
                    Location.X += 15; Location.Y += 30; To.X += 15;
                    g.DrawLine(p, Location, To);
                }
            }
            else if (Location.Y == To.Y) //Ve duoc ngang
            {
                if (Location.X < To.X) //Ve tu trai sang phai
                {
                    Location.X += 30;
                    Location.Y += 15;
                    To.Y += 15;
                    g.DrawLine(p, Location, To);
                }
                else
                {
                    Location.Y += 15;
                    To.X += 30;
                    To.Y += 15;
                    g.DrawLine(p, Location, To);
                }
            }


        }
        override public void DrawArrowTo(Graphics g, Pen p, Nodes Too)
        {
            Point From = Location;
            Point To = Too.Location;
            if (Location.X == To.X)//Ve duoc thang doc
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
