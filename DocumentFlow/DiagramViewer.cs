//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.01.2020
// Time: 23:30
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using Dapper;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Diagram.Controls;
using Syncfusion.Windows.Forms.Edit.Enums;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Entities;

namespace DocumentFlow
{
    public partial class DiagramViewer : ToolWindow, IPage
    {
        private readonly IContainerPage containerPage;
        private readonly Data.Entities.Transition transition;

        public DiagramViewer(IContainerPage container, Guid transitionId)
        {
            InitializeComponent();

            containerPage = container;

            using (var conn = Db.OpenConnection())
            {
                transition = conn.QuerySingle<Data.Entities.Transition>("select * from transition where id = :id", new { id = transitionId });
            }

            editControl1.ApplyConfiguration(KnownLanguages.XML);

            Text = transition.name;

            diagram1.EventSink.NodeCollectionChanged += EventSink_NodeCollectionChanged;
            System.Windows.Forms.Application.Idle += Application_Idle;

            PrepareModelDiagram(model1);
            if (transition.diagram_model == null)
            {
                CreateDiagram();
            }
            else
            {
                using (var stream = new MemoryStream())
                {
                    stream.Write(transition.diagram_model, 0, transition.diagram_model.Length);
                    stream.Position = 0;
                    diagram1.LoadBinary(stream);
                }
            }
        }

        #region IPage implementation

        IContainerPage IPage.Container => containerPage;

        Guid IPage.Id => transition.Id;

        Guid IPage.InfoId => transition.Id;

        void IPage.Rebuild() { }

        #endregion

        private bool Save()
        {
            using (var stream = new MemoryStream())
            {
                diagram1.SaveBinary(stream);

                transition.diagram_model = stream.ToArray();
                using (var conn = Db.OpenConnection())
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            conn.Execute("update transition set diagram_model = :diagram_model where id = :id", transition, transaction);
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            ExceptionHelper.MesssageBox(e);
                        }
                    }
                }
            }

            return false;
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
                //if (this.WindowState != FormWindowState.Minimized)
                //    this.pasteToolStripButton.Enabled = diagram1.CanPaste;
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
                //if (this.WindowState != FormWindowState.Minimized)
                //    this.pasteToolStripButton.Enabled = false;
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
                    label.Position = Position.TopCenter;
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
            for (var p = Position.TopLeft; p < Position.Custom; p++)
            {
                ConnectionPoint point = new ConnectionPoint()
                {
                    Position = p
                };

                rect.Ports.Add(point);
            }
        }

        private RoundRect CreateRoundRect(int x, int y, Status status)
        {
            RoundRect rect = new RoundRect(x, y, 30, 16, MeasureUnits.Millimeter);

            rect.PropertyBag.Add("StatusId", status.id);

            diagram1.Model.AppendChild(rect);

            if (!rect.Labels.IsEmpty)
                rect.Labels[0].Text = status.note;

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

        private RoundRect CreateRoundRect(IDictionary<int, RoundRect> nodes, Status status, ref int x, ref int y)
        {
            RoundRect rect;
            if (nodes.ContainsKey(status.id))
                rect = nodes[status.id];
            else
            {
                rect = CreateRoundRect(x, y, status);
                nodes.Add(status.id, rect);

                x += 20;
                if (x + 30 >= 210)
                {
                    x = 40;
                    y += 20;
                }
            }

            return rect;
        }

        private void CreateExtremeShape(IDictionary<int, RoundRect> nodes, int? status, int x, int y, bool reverse)
        {
            if (!status.HasValue)
                return;

            if (nodes.ContainsKey(status.Value))
            {
                RoundRect rect = nodes[status.Value];

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

            Dictionary<int, RoundRect> nodes = new Dictionary<int, RoundRect>();
            using (var conn = Db.OpenConnection())
            {
                int x = 40;
                int y = 20;

                string sql = "select * from changing_status cs join status s_f on (s_f.id = cs.from_status_id) join status s_t on (s_t.id = cs.to_status_id) where transition_id = :id order by order_index";
                var list = conn.Query<ChangingStatus, Status, Status, ChangingStatus>(sql, (changing_status, status_from, status_to) =>
                {
                    changing_status.FromStatus = status_from;
                    changing_status.ToStatus = status_to;
                    return changing_status;
                }, new { id = transition.Id });

                foreach (var cs in list)
                {
                    RoundRect left = CreateRoundRect(nodes, cs.FromStatus, ref x, ref y);
                    RoundRect right = CreateRoundRect(nodes, cs.ToStatus, ref x, ref y);
                    CreateConnector(left, right, cs.name);
                }

                CreateExtremeShape(nodes, transition.starting_id, 20, 20, false);
                CreateExtremeShape(nodes, transition.finishing_id, x, y, true);
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
            paletteGroupBar1.BorderColor = ColorTranslator.FromHtml("#626262");
            foreach (Syncfusion.Windows.Forms.Tools.GroupBarItem item in paletteGroupBar1.GroupBarItems)
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

        private void buttonSave_Click(object sender, EventArgs e) => Save();

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

        private void buttonUndo_Click(object sender, EventArgs e) => diagram1.Model.HistoryManager.Undo();

        private void buttonRedo_Click(object sender, EventArgs e) => diagram1.Model.HistoryManager.Redo();

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
