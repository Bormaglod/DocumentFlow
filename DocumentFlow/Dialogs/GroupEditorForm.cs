//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.03.2019
//
// Версия 2022.8.18
//  - не удалось создать группу в таблице "Продукция" (и в других,
//    допускающих создание групп). Окно для создания закрывается, ни
//    ошибок, ни сообщений не выдается. Данная проблема возникает только
//    в корневом каталоге. Исправлено.
//  - если попытаться изменить группу, открывается окно поля которого
//    должны быть заполнены значениями группы, но они пустые. Исправлено.
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Dialogs
{
    public partial class GroupEditorForm<T> : Form
        where T : IIdentifier<Guid>
    {
        private readonly IDirectoryRepository<T> repository;
        private T? editableDir;
        private readonly Guid? parent_id;

        protected GroupEditorForm(IDirectoryRepository<T> repository)
        {
            InitializeComponent();
            this.repository = repository;
        }

        public GroupEditorForm(IDirectoryRepository<T> repository, T editableDir) : this(repository)
        {
            this.editableDir = editableDir;

            if (editableDir is IDirectory dir)
            {
                textCode.Text = dir.Code;
                textName.Text = dir.ItemName;
            }
        }

        public GroupEditorForm(IDirectoryRepository<T> repository, Guid? parent_id) : this(repository)
        {
            this.parent_id = parent_id;
        }

        public T? ShowFolderDialog()
        {
            if (ShowDialog() == DialogResult.OK)
            {
                return editableDir;
            }

            return default;
        }

        private void DoCommit()
        {
            try
            {
                if (editableDir is IDirectory dir)
                {
                    dir.Code = textCode.Text;
                    dir.ItemName = textName.Text;
                    repository.Update(editableDir);
                }
                else
                {
                    var newId = repository.AddFolder(parent_id, textCode.Text, textName.Text);
                    editableDir = repository.GetById(newId);
                }
            }
            catch (Exception e)
            {
                DialogResult = DialogResult.None;
                ExceptionHelper.MesssageBox(e);
            }
        }

        private void ButtonOk_Click(object sender, EventArgs e) => DoCommit();
    }
}
