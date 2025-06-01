using System.Drawing;
using System.Windows.Forms;

namespace MazeGenerator
{
    partial class MainForm
    {
        private NoArrowComboBox mazeSizeComboBox;
        private Button newGameButton;
        private Label statusLabel;
        private Label instructionsLabel;

        private void InitializeComponent()
        {
            this.mazeSizeComboBox = new NoArrowComboBox();
            this.newGameButton = new Button();
            this.statusLabel = new Label();
            this.instructionsLabel = new Label();
            this.SuspendLayout();

            // mazeSizeComboBox
            this.mazeSizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.mazeSizeComboBox.FormattingEnabled = true;
            this.mazeSizeComboBox.Items.AddRange(new object[] {
                "Small (11x11)",
                "Medium (15x15)",
                "Large (21x21)",
                "Extra Large (31x31)"
            });
            this.mazeSizeComboBox.Location = new Point(10, 10);
            this.mazeSizeComboBox.Name = "mazeSizeComboBox";
            this.mazeSizeComboBox.Size = new Size(120, 24);
            this.mazeSizeComboBox.SelectedIndex = 1;
            this.mazeSizeComboBox.SelectedIndexChanged += MazeSizeComboBox_SelectedIndexChanged;

            // newGameButton
            this.newGameButton.Location = new Point(140, 10);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new Size(100, 24);
            this.newGameButton.Text = "New Game";
            this.newGameButton.UseVisualStyleBackColor = true;
            this.newGameButton.Click += NewGameButton_Click;

            // statusLabel
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new Point(10, 40);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new Size(200, 17);
            this.statusLabel.Text = "Navigate to the red square to win!";

            // instructionsLabel
            this.instructionsLabel.AutoSize = true;
            this.instructionsLabel.Location = new Point(10, 60);
            this.instructionsLabel.Name = "instructionsLabel";
            this.instructionsLabel.Size = new Size(200, 17);
            this.instructionsLabel.Text = "Use arrow keys to move";

            // MainForm
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(400, 400);
            this.Controls.Add(this.mazeSizeComboBox);
            this.Controls.Add(this.newGameButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.instructionsLabel);
            this.Name = "MainForm";
            this.Text = "Maze Generator";
            this.Paint += MainForm_Paint;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
    public class NoArrowComboBox : ComboBox
    {
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down ||
                keyData == Keys.Left || keyData == Keys.Right)
            {
                return true; // Treat arrow keys as normal input, not selection change
            }
            return base.IsInputKey(keyData);
        }
    }
}
