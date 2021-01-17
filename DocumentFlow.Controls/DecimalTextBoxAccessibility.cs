//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.03.2019
// Time: 22:51
//-----------------------------------------------------------------------

using System.Drawing;
using System.Windows.Forms;

namespace DocumentFlow.Controls
{
    public class DecimalTextBoxAccessibility : Control.ControlAccessibleObject
    {
        /// <summary>
        /// DecimalTextBox
        /// </summary>
        private readonly DecimalTextBox m_DecimalTextBox;

        /// <summary>
        /// Bounds of the Control
        /// </summary>
        /// <returns>The accessible object bounds.</returns>
        public override Rectangle Bounds => m_DecimalTextBox.RectangleToScreen(m_DecimalTextBox.ClientRectangle);

        /// <summary>
        /// Gets the role for the RibbonControlAdv. This is used by accessibility programs.
        /// </summary>
        public override AccessibleRole Role => AccessibleRole.Text;

        /// <summary>
        /// Gets or sets the accessible object name
        /// </summary>
        /// <returns>The accessible object name.</returns>
        public override string Name => m_DecimalTextBox.Name;

        /// <summary>
        /// Gets the description of the RibbonControlAdvAccessibility
        /// </summary>
        /// <returns> A string describing the RibbonControlAdvAccessibility.</returns>
        public override string Description => "DecimalTextBox," + m_DecimalTextBox.Style.ToString() + "," + m_DecimalTextBox.Text.ToString() + "," + m_DecimalTextBox.Bounds.X.ToString() + "," + m_DecimalTextBox.Bounds.Y.ToString() + "," + m_DecimalTextBox.Bounds.Width.ToString() + "," + m_DecimalTextBox.Bounds.Height.ToString() + "," + m_DecimalTextBox.Name.ToString() + "," + m_DecimalTextBox.ContainsFocus.ToString() + "," + m_DecimalTextBox.Enabled.ToString();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="DecimalTextBox">The RibbonControlAdv instance.</param>
        public DecimalTextBoxAccessibility(DecimalTextBox DecimalTextBox)
            : base(DecimalTextBox)
        {
            m_DecimalTextBox = DecimalTextBox;
        }

        /// <summary>
        /// Retrieves the child object at the specified screen coordinates.
        /// </summary>
        /// <param name="x">The horizontal screen coordinate.</param>
        /// <param name="y">The vertical screen coordinate.</param>
        /// <returns>An RibbonControlAdvAccessibility that represents the child object at the given screen coordinates.
        /// This method returns the calling object if the object itself is at the location specified.
        /// Returns null if no object is at the tested location.</returns>
        public override AccessibleObject HitTest(int x, int y)
        {
            Control childAtPoint = m_DecimalTextBox.GetChildAtPoint(m_DecimalTextBox.PointToClient(new Point(x, y)));
            if (childAtPoint != null)
            {
                return childAtPoint.AccessibilityObject;
            }

            return base.HitTest(x, y);
        }
    }
}
