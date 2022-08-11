//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.03.2019
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Dialogs
{
    public partial class GroupEditorForm<T> : Form
        where T : IIdentifier<Guid>
    {
        private readonly IDirectoryRepository<T> repository;
        private readonly T? editableDir;
        private readonly Guid? parent_id;
        private Guid newId;

        protected GroupEditorForm(IDirectoryRepository<T> repository)
        {
            InitializeComponent();
            this.repository = repository;
        }

        public GroupEditorForm(IDirectoryRepository<T> repository, T editableDir) : this(repository)
        {
            this.editableDir = editableDir;
        }

        public GroupEditorForm(IDirectoryRepository<T> repository, Guid? parent_id) : this(repository)
        {
            this.parent_id = parent_id;
        }

        public T? ShowFolderDialog()
        {
            if (ShowDialog() == DialogResult.OK)
            {
                if (parent_id == null)
                {
                    return editableDir;
                }
                else
                {
                    return repository.GetById(newId);
                }
            }

            return default;
        }

        private void DoCommit()
        {
            try
            {
                if (editableDir is IDirectory dir)
                {
                    dir.code = textCode.Text;
                    dir.item_name = textName.Text;
                    repository.Update(editableDir);
                    return;
                }

                if (parent_id != null)
                {
                    newId = repository.AddFolder(parent_id, textCode.Text, textName.Text);
                    return;
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
