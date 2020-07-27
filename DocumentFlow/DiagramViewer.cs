//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.01.2020
// Time: 23:30
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Printing;
    using System.Drawing.Text;
    using System.Data;
    using System.IO;
    using System.Linq;
    using NHibernate;
    using Syncfusion.Windows.Forms.Diagram;
    using Syncfusion.Windows.Forms.Diagram.Controls;
    using Syncfusion.Windows.Forms.Edit.Enums;
    using Syncfusion.Windows.Forms.Tools;
    using DocumentFlow.Core;
    using DocumentFlow.Data.Core;
    using Entities = Data.Entities;

    public partial class DiagramViewer : System.Windows.Forms.UserControl, IPage
    {
        private Entities.Transition transition;

        public DiagramViewer(Guid transitionId)
        {
            InitializeComponent();
            NewSession();

            transition = Session.Get<Entities.Transition>(transitionId);

            editControl1.ApplyConfiguration(KnownLanguages.XML);

            Text = transition.Name;

            diagram1.EventSink.NodeCollectionChanged += EventSink_NodeCollectionChanged;
            System.Windows.Forms.Application.Idle += Application_Idle;

            PrepareModelDiagram(model1);
            if (transition.DiagramModel == null)
            {
                CreateDiagram();
            }
            else
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    stream.Write(transition.DiagramModel, 0, transition.DiagramModel.Length);
                    stream.Position = 0;
                    diagram1.LoadBinary(stream);
                }
            }
        }

        protected ISession Session { get; private set; }

        Guid IPage.Id => transition.Id;

        Guid IPage.ContentId => transition.Id;

        void IPage.OnClosed() { }

        protected void NewSession() => Session = Db.OpenSession();

        private bool Save()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                diagram1.SaveBinary(stream);

                transition.DiagramModel = stream.ToArray();
                using (var transaction = Session.BeginTransaction())
                {
                    try
                    {
                        Session.SaveOrUpdate(transition);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ExceptionHelper.MesssageBox(ex);
                        NewSession();
                        transition = Session.Merge(transition);
                    }
                }
            }

            return false;
        }

        private void Close()
        {
            ((TabPageAdv)Parent).Close();
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            if (diagram1 != null && diagram1.Model != null)
            {
                buttonFill.Enabled = true;
                buttonRefresh.Enabled = true;
                buttonSave.Enabled = true;
                buttonSaveAndClose.Enabled = true;
                buttonCut.Enabled = diagram1.CanCut;
                buttonCopy.Enabled = diagram1.CanCopy;
                /*if (this.WindowState != FormWindowState.Minimized)
                    this.pasteToolStripButton.Enabled = diagram1.CanPaste;*/
                buttonUndo.Enabled = diagram1.Model.HistoryManager.CanUndo;
                buttonRedo.Enabled = diagram1.Model.HistoryManager.CanRedo;
                buttonDelete.Enabled = (diagram1.View.SelectionList.Count > 0);

            }
            else
            {
                buttonFill.Enabled = false;
                buttonRefresh.Enabled = false;
                buttonSave.Enabled = false;
                buttonSaveAndClose.Enabled = false;
                buttonCut.Enabled = false;
                buttonCopy.Enabled = false;
                /*if (this.WindowState != FormWindowState.Minimized)
                    this.pasteToolStripButton.Enabled = false;*/
                buttonUndo.Enabled = false;
                buttonRedo.Enabled = false;
                buttonDelete.Enabled = false;
            }
        }

        private void EventSink_NodeCollectionChanged(CollectionExEventArgs evtArgs)
        {
            if (evtArgs.ChangeType == CollectionExChangeType.Insert)
            {
                foreach (var node in evtArgs.Elements.OfType<FilledPath>())
                {
                    node.FillStyle.Color = Color.FromArgb(199, 224, 244);
                    node.FillStyle.ForeColor = Color.White;
                    node.FillStyle.Type = FillStyleType.LinearGradient;
                    node.LineStyle.LineColor = Color.FromArgb(0, 120, 212);

                    node.EnableCentralPort = true;
                    node.DrawPorts = false;

                    CreatePorts(node);
                    CreateLabel(node, "");
                }

                foreach (var line in evtArgs.Elements.OfType<LineBase>())
                {
                    line.LineStyle.LineColor = Color.FromArgb(0, 120, 212);
                    line.HeadDecorator.DecoratorShape = DecoratorShape.Filled45Arrow;
                    
                    line.HeadDecorator.FillStyle.Color = Color.MidnightBlue;
                    line.HeadDecorator.LineStyle.LineColor = Color.FromArgb(0, 120, 212);
                    line.HeadDecorator.Size = new SizeF(6, 6);

                    line.TailDecorator.FillStyle.Color = Color.MidnightBlue;
                    line.TailDecorator.LineStyle.LineColor = Color.FromArgb(0, 120, 212);
                    line.TailDecorator.Size = new SizeF(6, 6);

                    Label label = CreateLabel(line, "");
                    label.AdjustRotateAngle = true;
                    label.Position = Syncfusion.Windows.Forms.Diagram.Position.TopCenter;
                    label.FontStyle.PointSize = 8;
                }
            }
        }

        private Label CreateLabel(PathNode node, string labelText)
        {
            Label label = new Label(node, labelText);
            label.FontStyle.Family = "Segoe UI";
            label.FontStyle.PointSize = 10;
            label.FontColorStyle.Color = Color.FromArgb(68, 68, 68);

            node.Labels.Add(label);

            return label;
        }

        private void CreatePorts(FilledPath rect)
        {
            for (var p = Syncfusion.Windows.Forms.Diagram.Position.TopLeft; p < Syncfusion.Windows.Forms.Diagram.Position.Custom; p++)
            {
                ConnectionPoint point = new ConnectionPoint()
                {
                    Position = p
                };

                rect.Ports.Add(point);
            }
        }

        private RoundRect CreateRoundRect(int x, int y, Entities.Status status)
        {
            RoundRect rect = new RoundRect(x, y, 30, 16, MeasureUnits.Millimeter);

            rect.PropertyBag.Add("StatusId", status.Id);

            diagram1.Model.AppendChild(rect);

            if (!rect.Labels.IsEmpty)
                rect.Labels[0].Text = status.Note;

            return rect;
        }

        private void CreateConnector(Node left, Node right, string labelText)
        {
            LineConnector line = new LineConnector(left.PinPoint, right.PinPoint);
            left.CentralPort.TryConnect(line.TailEndPoint);
            right.CentralPort.TryConnect(line.HeadEndPoint);

            diagram1.Model.AppendChild(line);

            if (!line.Labels.IsEmpty && !string.IsNullOrEmpty(labelText))
                line.Labels[0].Text = labelText;
        }

        private RoundRect CreateRoundRect(IDictionary<long, RoundRect> nodes, Entities.Status status, ref int x, ref int y)
        {
            RoundRect rect;
            if (nodes.ContainsKey(status.Id))
                rect = nodes[status.Id];
            else
            {
                rect = CreateRoundRect(x, y, status);
                nodes.Add(status.Id, rect);

                x += 20;
                if (x + 30 >= 210)
                {
                    x = 40;
                    y += 20;
                }
            }

            return rect;
        }

        private void CreateExtremeShape(IDictionary<long, RoundRect> nodes, Entities.Status status, int x, int y, bool reverse)
        {
            if (status == null)
                return;

            if (nodes.ContainsKey(status.Id))
            {
                RoundRect rect = nodes[status.Id];

                Ellipse node = new Ellipse(x, y, 10, 10, MeasureUnits.Millimeter);
                node.FillStyle.Color = Color.FromArgb(0, 120, 212);
                diagram1.Model.AppendChild(node);

                if (reverse)
                    CreateConnector(rect, node, string.Empty);
                else
                    CreateConnector(node, rect, string.Empty);
            }
        }

        private void CreateDiagram()
        {
            diagram1.BeginUpdate();

            Dictionary<long, RoundRect> nodes = new Dictionary<long, RoundRect>();
            using (var transaction = Session.BeginTransaction())
            {
                int x = 40;
                int y = 20;
                foreach (var cs in from Entities.ChangingStatus cs in Session.QueryOver<Entities.ChangingStatus>().Where(p => p.Transition == transition).OrderBy(p => p.FromStatus).Asc.List() select cs)
                {
                    RoundRect left = CreateRoundRect(nodes, cs.FromStatus, ref x, ref y);
                    RoundRect right = CreateRoundRect(nodes, cs.ToStatus, ref x, ref y);
                    CreateConnector(left, right, cs.Name);
                }

                CreateExtremeShape(nodes, transition.Starting, 20, 20, false);
                CreateExtremeShape(nodes, transition.Finishing, x, y, true);
            }

            diagram1.EndUpdate();
        }

        private void PrepareModelDiagram(Model model)
        {
            paletteGroupBar1.LoadPalette(@".\Diagram Palette\Basic Shapes.edp");
            paletteGroupBar1.LoadPalette(@".\Diagram Palette\Flowchart Symbols.edp");
            paletteGroupBar1.LoadPalette(@".\Diagram Palette\SwimLane Symbols.edp");

            GroupBarAppearance();

            model.RenderingStyle.SmoothingMode = SmoothingMode.HighQuality;
            model.RenderingStyle.InterpolationMode = InterpolationMode.HighQualityBicubic;
            model.RenderingStyle.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        }

        private void GroupBarAppearance()
        {
            this.paletteGroupBar1.BorderColor = ColorTranslator.FromHtml("#626262");
            foreach (GroupBarItem item in paletteGroupBar1.GroupBarItems)
            {
                 if (item.Client is PaletteGroupView)
                {
                    PaletteGroupView view = item.Client as PaletteGroupView;
                    view.Font = new Font("Segoe UI", 9, System.Drawing.FontStyle.Regular);
                    view.ForeColor = Color.Black;
                    view.FlowView = true;
                    view.ShowToolTips = true;
                    view.ShowFlowViewItemText = true;
                    view.SelectedItemColor = Color.FromArgb(255, 219, 118);
                    view.HighlightItemColor = Color.FromArgb(255, 227, 149);
                    view.SelectingItemColor = Color.FromArgb(255, 238, 184);
                    view.SelectedHighlightItemColor = Color.FromArgb(255, 218, 115);
                    view.FlowViewItemTextLength = 80;
                    view.BackColor = Color.White;
                    view.TextWrap = true;
                    view.FlatLook = true;
                    view.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                Close();
            }
        }

        private void buttonFill_Click(object sender, EventArgs e)
        {
            diagram1.Model.Clear();
            
            Model model = new Model();
            PrepareModelDiagram(model);
            
            diagram1.Model = model;
            CreateDiagram();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {

        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            diagram1.Model.HistoryManager.Undo();
        }

        private void buttonRedo_Click(object sender, EventArgs e)
        {
            diagram1.Model.HistoryManager.Redo();
        }

        private void buttonCut_Click(object sender, EventArgs e)
        {
            diagram1.Controller.Cut();
            buttonPaste.Enabled = true;
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            diagram1.Controller.Copy();
            buttonPaste.Enabled = true;
        }

        private void buttonPaste_Click(object sender, EventArgs e)
        {
            diagram1.Controller.Paste();
            buttonPaste.Enabled = false;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            diagram1.Model.BeginUpdate();
            diagram1.Controller.Delete();
            diagram1.Model.EndUpdate();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (diagram1 != null)
            {
                PrintDocument printDoc = diagram1.CreatePrintDocument();
                System.Windows.Forms.PrintDialog printDlg = new System.Windows.Forms.PrintDialog
                {
                    Document = printDoc,
                    AllowSomePages = true
                };

                if (printDlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    printDoc.PrinterSettings = printDlg.PrinterSettings;
                    printDoc.Print();
                }
            }
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            if (diagram1 != null)
            {
                PrintDocument printDoc = diagram1.CreatePrintDocument();
                System.Windows.Forms.PrintPreviewDialog printPreviewDlg = new System.Windows.Forms.PrintPreviewDialog();

                printDoc.PrinterSettings.FromPage = 0;
                printDoc.PrinterSettings.ToPage = 0;
                printDoc.PrinterSettings.PrintRange = PrintRange.AllPages;

                printPreviewDlg.Document = printDoc;
                printPreviewDlg.ShowDialog(this);
            }
        }
    }
}
