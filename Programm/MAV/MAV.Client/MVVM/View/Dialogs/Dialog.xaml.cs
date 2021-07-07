using ModernWpf.Controls;

namespace MAV.Client.MVVM.View
{
    public partial class Dialog : ContentDialog
    {
        public Dialog()
        {
            InitializeComponent();
        }

        public string FirstLineContent 
        { 
            get { return FirstLine.Text; } 
            set { FirstLine.Text = value; } 
        }

        public string SecondLineText 
        { 
            get { return SecondLine.Text; } 
            set { SecondLine.Text = value; } 
        }
    }
}
