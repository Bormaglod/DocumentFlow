//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.10.2023
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data;
using DocumentFlow.Data.Interfaces.Repository;
using DocumentFlow.Messages;
using DocumentFlow.ViewModels;

using Humanizer;

using Syncfusion.DataSource;
using Syncfusion.DataSource.Extensions;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.Input.Enums;
using Syncfusion.WinForms.ListView.Events;

using System.Collections.ObjectModel;

namespace DocumentFlow.Controls.PageContents;

public partial class EmailPage : UserControl, IEmailPage
{
    private readonly IEmailLogRepository repository;
    private Guid? currentGroup;
    private Guid? currentContractor;

    public EmailPage(IEmailLogRepository repository)
    {
        InitializeComponent();

        Text = "Почта";

        this.repository = repository;

        listCompany.DisplayMember = nameof(EmailLog.ContractorName);
        listCompany.ValueMember = nameof(EmailLog.ContractorId);
    }

    public void NotifyPageClosing()
    {
    }

    public void RefreshPage()
    {
        var log = repository.GetEmails(currentGroup, currentContractor);
        gridContent.DataSource = new ObservableCollection<EmailLog>(log);

        if (currentGroup == null && currentContractor == null)
        {
            var contractors = log.DistinctBy(x => x.ContractorId);
            listCompany.DataSource = new ObservableCollection<EmailLog>(contractors);

            listCompany.View.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = nameof(EmailLog.ContractorGroup)
            });
        }
    }

    private void GridContent_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
    {
        switch (e.Column.MappingName)
        {
            case nameof(EmailLog.DateTimeSending):
                e.Column.Width = 180;
                e.Column.CellStyle.HorizontalAlignment = HorizontalAlignment.Left;
                if (e.Column is GridDateTimeColumn dateColumn)
                {
                    dateColumn.Pattern = DateTimePattern.FullDateTime;
                }

                break;
            case nameof(EmailLog.ToAddress):
                e.Column.Width = 220;
                break;
            case nameof(EmailLog.ContractorName):
                e.Column.Width = 250;
                break;
            case nameof(EmailLog.Document):
                e.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                break;
        }
    }

    private void ListCompany_SelectionChanging(object sender, ItemSelectionChangingEventArgs e)
    {
        if (e.AddedItems.Count == 1)
        {
            if (e.AddedItems[0] is GroupResult group)
            {
                var email = group.Items.OfType<EmailLog>().First();
                currentGroup = email.ContractorGroupId;
                currentContractor = null;
            }
            else if (e.AddedItems[0] is EmailLog emailLog)
            {
                currentGroup = null;
                currentContractor = emailLog.ContractorId;
            }
            else
            {
                currentGroup = null;
                currentContractor = null;
            }

            RefreshPage();
        }
    }

    private void ListCompany_GroupCollapsing(object sender, GroupExpandCollapseChangingEventArgs e)
    {
        e.Cancel = true;
    }

    private void ButtonContractor_Click(object sender, EventArgs e)
    {
        if (gridContent.SelectedItem is EmailLog log)
        {
            WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IContractorEditor), log.ContractorId));
        }
    }

    private void ButtonDocument_Click(object sender, EventArgs e)
    {
        if (gridContent.SelectedItem is EmailLog log && log.Code != null && log.DocumentId != null)
        {
            var type = Type.GetType($"DocumentFlow.ViewModels.I{log.Code.Pascalize()}Editor");
            if (type != null)
            {
                WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(type, log.DocumentId.Value));
            }
        }
    }
}
