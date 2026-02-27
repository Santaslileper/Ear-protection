using S=System;
using R=System.Runtime.InteropServices;
using M=System.Threading.Mutex;
using I=System.IO.File;
using Pth=System.IO.Path;
using C=System.Collections.Generic.Dictionary<string,float>;
using F=System.Windows.Forms;
using D=System.Drawing;
using G=System.Drawing.Drawing2D;
using W=Microsoft.Win32.Registry;
using E=System.Environment;

namespace A {
    [R.Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99"), R.InterfaceType(R.ComInterfaceType.InterfaceIsIUnknown)]
    public interface PS { int A(out int c); int B(int i, out PK k); int C(ref PK k, out PV p); int D(ref PK k, ref PV p); int E(); }
    [R.StructLayout(R.LayoutKind.Sequential, Pack=4)] public struct PK { public S.Guid f; public int p; }
    [R.StructLayout(R.LayoutKind.Explicit)] public struct PV { [R.FieldOffset(0)]public short v; [R.FieldOffset(8)]public S.IntPtr p; }
    [R.Guid("5CDF2C82-841E-4546-9722-0CF74078229A"), R.InterfaceType(R.ComInterfaceType.InterfaceIsIUnknown)]
    public interface EV {
        int a();int b();int c();int d(); int SM(float f, [R.In]ref S.Guid c); int e(); int GM(out float f); int f();int g();int h();int i();
        int SMu([R.MarshalAs(R.UnmanagedType.Bool)]bool b, [R.In]ref S.Guid c); int GMu(out bool b);
    }
    [R.Guid("D666063F-1587-4E43-81F1-B948E807363F"), R.InterfaceType(R.ComInterfaceType.InterfaceIsIUnknown)]
    public interface MD { int A([R.In]ref S.Guid i, int c, int a, out EV v); int O(int a, out PS p); int I([R.MarshalAs(R.UnmanagedType.LPWStr)]out string i); int S(out int s); }
    [R.Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E"), R.InterfaceType(R.ComInterfaceType.InterfaceIsIUnknown)]
    public interface MC { int C(out int c); int I(int n, out MD d); }
    [R.Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), R.InterfaceType(R.ComInterfaceType.InterfaceIsIUnknown)]
    public interface ME { int E(int f, int m, out MC c); int G(int f, int r, out MD e); }
    [R.ComImport, R.Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")] public class MM { }
    
    public static class U {
        public static EV V(MD d) { EV v; S.Guid g=typeof(EV).GUID; R.Marshal.ThrowExceptionForHR(d.A(ref g, 23, 0, out v)); return v; }
        public static string N(MD d) { PS s; d.O(0, out s); var k = new PK { f = new S.Guid("a45c254e-df1c-4efd-8020-67d146a850e0"), p = 14 }; PV p; s.C(ref k, out p); return p.v==31 ? R.Marshal.PtrToStringUni(p.p) : "?"; }
        public static S.Collections.Generic.IEnumerable<MD> A() { MC c; (new MM() as ME).E(0, 1, out c); int n; c.C(out n); for(int i=0; i<n; i++) { MD d; c.I(i, out d); yield return d; } }
    }

    class SS : F.Control {
        int v=100; public int V { get{return v;} set{v=value;} } public event S.EventHandler C; bool d;
        public SS() { DoubleBuffered=true; Cursor=F.Cursors.Hand; Height=20; Width=200; }
        protected override void OnPaint(F.PaintEventArgs e) {
            e.Graphics.SmoothingMode = G.SmoothingMode.AntiAlias; e.Graphics.Clear(Parent.BackColor);
            e.Graphics.FillRectangle(new D.SolidBrush(D.Color.FromArgb(60,60,60)), 0, 8, Width, 4);
            int f = (int)((V/100f)*Width); e.Graphics.FillRectangle(new D.SolidBrush(D.Color.FromArgb(0,204,153)), 0, 8, f==0?1:f, 4);
            int cx = f-8<0 ? 0 : (f+8>Width ? Width-16 : f-8); e.Graphics.FillEllipse(new D.SolidBrush(D.Color.White), cx, 2, 16, 16);
            if(d) e.Graphics.DrawEllipse(new D.Pen(D.Color.FromArgb(100,0,204,153), 4), cx-2, 0, 20, 20);
        }
        protected override void OnMouseDown(F.MouseEventArgs e) { d=true; UX(e.X); } protected override void OnMouseUp(F.MouseEventArgs e) { d=false; Invalidate(); }
        protected override void OnMouseMove(F.MouseEventArgs e) { if(d) UX(e.X); }
        void UX(int x) { float xf=(float)x/Width; if(xf<0)xf=0; if(xf>1)xf=1; int nv=(int)(xf*100); if(nv!=V){V=nv; Invalidate(); if(C!=null)C(this,S.EventArgs.Empty);} }
    }

    class ST : F.Control {
        public bool C { get; set; } public event S.EventHandler X;
        public ST() { DoubleBuffered=true; Cursor=F.Cursors.Hand; Size=new D.Size(44, 22); }
        protected override void OnPaint(F.PaintEventArgs e) {
            e.Graphics.SmoothingMode = G.SmoothingMode.AntiAlias; e.Graphics.Clear(Parent.BackColor);
            D.Color c = C ? D.Color.FromArgb(0,204,153) : D.Color.FromArgb(80,80,80);
            e.Graphics.FillPie(new D.SolidBrush(c), 0, 0, Height, Height, 90, 180); e.Graphics.FillPie(new D.SolidBrush(c), Width-Height, 0, Height, Height, 270, 180);
            e.Graphics.FillRectangle(new D.SolidBrush(c), Height/2, 0, Width-Height, Height); e.Graphics.FillEllipse(new D.SolidBrush(D.Color.White), C ? Width-Height+2 : 2, 2, Height-4, Height-4);
        }
        protected override void OnClick(S.EventArgs e) { C=!C; Invalidate(); if(X!=null)X(this,S.EventArgs.Empty); base.OnClick(e); }
    }

    class SF : F.Form {
        public SF() {
            BackColor=D.Color.FromArgb(28,28,30); ForeColor=D.Color.White; FormBorderStyle=F.FormBorderStyle.None; Padding=new F.Padding(2);
            ShowInTaskbar=false; AutoSize=true; AutoSizeMode=F.AutoSizeMode.GrowAndShrink; MinimumSize=new D.Size(600,200); Text="Volume Protector";
            F.Panel B = new F.Panel{ Height=30, Dock=F.DockStyle.Top, BackColor=D.Color.FromArgb(40,40,42) };
            F.Label T = new F.Label{ Text="VOLUME PROTECTOR", ForeColor=D.Color.FromArgb(0,204,153), Font=new D.Font("Segoe UI", 9, D.FontStyle.Bold), AutoSize=true, Location=new D.Point(12,8) };
            F.Label X = new F.Label{ Text="✕", ForeColor=D.Color.White, Font=new D.Font("Segoe UI", 12), Cursor=F.Cursors.Hand, AutoSize=true };
            X.MouseEnter+=(s,e)=>X.ForeColor=D.Color.Red; X.MouseLeave+=(s,e)=>X.ForeColor=D.Color.White; X.Click+=(s,e)=>Close();
            F.Panel H = new F.Panel{ Width=30, Height=30, Dock=F.DockStyle.Right }; B.Controls.Add(T); B.Controls.Add(H); H.Controls.Add(X); X.Location=new D.Point(5,5);
            D.Point dC=D.Point.Empty, dF=D.Point.Empty; bool d=false;
            F.MouseEventHandler md=(s,e)=>{d=true; dC=F.Cursor.Position; dF=Location;}; F.MouseEventHandler mm=(s,e)=>{if(d){Location=D.Point.Add(dF, new D.Size(D.Point.Subtract(F.Cursor.Position, new D.Size(dC))));}}; F.MouseEventHandler mu=(s,e)=>{d=false;};
            B.MouseDown+=md; B.MouseMove+=mm; B.MouseUp+=mu; T.MouseDown+=md; T.MouseMove+=mm; T.MouseUp+=mu; Controls.Add(B);
            F.FlowLayoutPanel p = new F.FlowLayoutPanel{ FlowDirection=F.FlowDirection.TopDown, AutoSize=true, WrapContents=false, Padding=new F.Padding(25), Location=new D.Point(2,32) }; Controls.Add(p);
            Paint += (s,e) => e.Graphics.DrawRectangle(new D.Pen(D.Color.FromArgb(0,204,153), 1), 0, 0, Width-1, Height-1);
            var Ulist = new S.Collections.Generic.List<S.Action>();
            F.FlowLayoutPanel R = new F.FlowLayoutPanel{ FlowDirection=F.FlowDirection.LeftToRight, AutoSize=true, Margin=new F.Padding(0,0,0,25), WrapContents=false };
            ST z = new ST{ Margin=new F.Padding(0,5,15,0), C=Prg.IsStart() }; z.X+=(s,e)=>Prg.SetStart(z.C);
            R.Controls.Add(new F.Label{ Text="CORE PROTECTION", Font=new D.Font("Segoe UI Semibold", 12), AutoSize=true, ForeColor=D.Color.FromArgb(0,204,153), Margin=new F.Padding(0,0,40,0) });
            R.Controls.Add(z); R.Controls.Add(new F.Label{ Text="Sync with Windows", AutoSize=true, Margin=new F.Padding(0,8,0,0) });
            p.Controls.Add(R);
            p.Controls.Add(new F.Label{ Text="DEVICE CONTROLS", Font=new D.Font("Segoe UI", 8, D.FontStyle.Bold), ForeColor=D.Color.FromArgb(120,120,120), AutoSize=true, Margin=new F.Padding(0,0,0,15) });
            foreach(var rawDev in A.U.A()) {
                var dev = rawDev; string n = A.U.N(dev); string locN = n;
                p.Controls.Add(new F.Label{ Text=locN.ToUpper(), AutoSize=true, Font=new D.Font("Segoe UI", 9, D.FontStyle.Bold), Margin=new F.Padding(0,5,0,5) });
                F.FlowLayoutPanel rw = new F.FlowLayoutPanel{ FlowDirection=F.FlowDirection.LeftToRight, AutoSize=true, Margin=new F.Padding(0,0,0,15), WrapContents=false };
                ST m = new ST{ Margin=new F.Padding(0,0,20,0) }; SS t = new SS{ Margin=new F.Padding(0,1,15,0) };
                F.Label l = new F.Label{ AutoSize=true, ForeColor=D.Color.FromArgb(0,204,153), Font=new D.Font("Consolas", 10), Margin=new F.Padding(0,2,0,0), Width=120 };
                try {
                    bool mt; A.U.V(dev).GMu(out mt); m.C=mt;
                    m.X += (s,e) => { S.Guid g=S.Guid.Empty; A.U.V(dev).SMu(m.C, ref g); };
                    Ulist.Add(() => { try { bool muteLive; A.U.V(dev).GMu(out muteLive); if(m.C!=muteLive){m.C=muteLive; m.Invalidate();} } catch {} });
                    if(Prg.L.ContainsKey(locN)) t.V = (int)Prg.L[locN]; l.Text="LIMIT: "+t.V+"%";
                    t.C += (s,e) => { l.Text="LIMIT: "+t.V+"%"; Prg.L[locN]=t.V; Prg.WriteCfg(); };
                } catch { } 
                rw.Controls.Add(m); rw.Controls.Add(t); rw.Controls.Add(l); p.Controls.Add(rw);
            }
            F.Timer Tmr = new F.Timer{ Interval=500 }; Tmr.Tick+=(s,e)=>{ foreach(var a in Ulist) a(); }; Tmr.Start(); FormClosed+=(s,e)=>Tmr.Stop();
        }
    }

    class DR : F.ToolStripProfessionalRenderer {
        public DR() : base(new DC()) {}
        protected override void OnRenderItemText(F.ToolStripItemTextRenderEventArgs e) { e.TextColor = D.Color.White; base.OnRenderItemText(e); }
        protected override void OnRenderItemCheck(F.ToolStripItemImageRenderEventArgs e) {
            e.Graphics.SmoothingMode=G.SmoothingMode.AntiAlias; e.Graphics.FillRectangle(new D.SolidBrush(D.Color.FromArgb(0,204,153)), e.ImageRectangle);
            e.Graphics.DrawString("✓", new D.Font("Segoe UI",10,D.FontStyle.Bold), D.Brushes.White, e.ImageRectangle, new D.StringFormat{Alignment=D.StringAlignment.Center,LineAlignment=D.StringAlignment.Center});
        }
    }
    class DC : F.ProfessionalColorTable {
        public override D.Color MenuItemSelected { get{return D.Color.FromArgb(60,60,60);} } public override D.Color MenuItemBorder { get{return D.Color.Transparent;} }
        public override D.Color ToolStripDropDownBackground { get{return D.Color.FromArgb(28,28,30);} } public override D.Color ToolStripBorder { get{return D.Color.FromArgb(50,50,50);} }
        public override D.Color ImageMarginGradientBegin { get{return D.Color.FromArgb(28,28,30);} } public override D.Color ImageMarginGradientMiddle { get{return D.Color.FromArgb(28,28,30);} }
        public override D.Color ImageMarginGradientEnd { get{return D.Color.FromArgb(28,28,30);} } public override D.Color SeparatorLight { get{return D.Color.FromArgb(50,50,50);} } public override D.Color SeparatorDark { get{return D.Color.FromArgb(50,50,50);} }
    }

    class Prg {
        public static C L = new C();
        static string Ft = Pth.Combine(E.GetFolderPath(E.SpecialFolder.ApplicationData), "VolumeProtectorSettings.txt");
        public static void WriteCfg() { var l=new S.Collections.Generic.List<string>(); foreach(var k in L) l.Add(k.Key+"="+k.Value.ToString(S.Globalization.CultureInfo.InvariantCulture)); I.WriteAllLines(Ft, l.ToArray()); }
        public static void ReadCfg() { L.Clear(); if(!I.Exists(Ft))return; foreach(var l in I.ReadAllLines(Ft)){ var p=l.Split('='); if(p.Length==2){ float v; if(float.TryParse(p[1], S.Globalization.NumberStyles.Float, S.Globalization.CultureInfo.InvariantCulture, out v)) L[p[0]]=v; } } }
        public static bool IsStart() { using(var r=W.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run",false)){return r!=null && r.GetValue("VP")!=null;} }
        public static void SetStart(bool e) { using(var r=W.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run",true)){if(r!=null){if(e)r.SetValue("VP",F.Application.ExecutablePath);else r.DeleteValue("VP",false);}} }

        [S.STAThread]
        static void Main() {
            bool N; M m = new M(true, "VPM", out N); if(!N) return;
            F.Application.EnableVisualStyles(); ReadCfg(); D.Icon ic = null;
            try { ic=D.Icon.ExtractAssociatedIcon(F.Application.ExecutablePath); } catch {} if(ic==null) ic=D.SystemIcons.Shield;
            F.NotifyIcon T = new F.NotifyIcon{ Icon=ic, Visible=true, Text="VP" }; F.ContextMenuStrip c = new F.ContextMenuStrip{ Renderer=new DR(), Font=new D.Font("Segoe UI", 10) };
            F.ToolStripMenuItem en = new F.ToolStripMenuItem("Protection Enabled"){Checked=true,CheckOnClick=true}, st = new F.ToolStripMenuItem("Open Settings..."); st.Click+=(s,e)=>{new SF().ShowDialog();ReadCfg();};
            F.ToolStripMenuItem ex = new F.ToolStripMenuItem("Exit"); ex.Click+=(s,e)=>F.Application.Exit();
            c.Items.AddRange(new F.ToolStripItem[]{en,new F.ToolStripSeparator(),st,new F.ToolStripSeparator(),ex}); T.ContextMenuStrip=c;
            F.Timer t = new F.Timer{Interval=100};
            t.Tick += (s,e) => {
                if(!en.Checked)return; try { foreach(var d in A.U.A()) { string n=A.U.N(d); if(L.ContainsKey(n)) { float x=L[n]/100f; float v; A.U.V(d).GM(out v); if(v>x+0.005f) { S.Guid g=S.Guid.Empty; A.U.V(d).SM(x,ref g); } } } } catch {}
            };
            t.Start(); F.Application.Run(); T.Visible=false;
        }
    }
}
