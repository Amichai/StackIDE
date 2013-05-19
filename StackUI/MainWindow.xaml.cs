using AvalonDock.Layout;
using StackIDE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StackUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        Stack st = new Stack();
        public MainWindow() {
            //TODO: This needs to be implemented with VS like tabs
            InitializeComponent();
            st.Push("3");
            st.Push("5");
            this.stackControl.ItemsSource = st.TokenStack;
            this.operationsControl.ItemsSource = st.OperationStack;
            this.allFunctions.ItemsSource = st.GetFunctions();
            this.textbox.Focus();
        }

        private void push() {
            if (this.textbox.Text == "") return;
            if (this.textbox.Text.First() == '\'') {
                this.st.Push(this.textbox.Text.Substring(1), false);
            } else {
                this.st.Push(this.textbox.Text);
            }
            this.textbox.Text = "";
            this.stackControl.ItemsSource = null;
            this.stackControl.ItemsSource = st.TokenStack;
            this.operationsControl.ItemsSource = null;
            this.operationsControl.ItemsSource = st.OperationStack;
        }



        private void Button_Click_1(object sender, RoutedEventArgs e) {
            push();
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e) {
            switch (e.Key) {
                case Key.Enter:
                    push();
                    break;
                    
            }
        }

        private int selectorIndex = 0;

        private void textbox_KeyUp_1(object sender, KeyEventArgs e) {
            switch (e.Key) {
                case Key.Up:
                    if (selectorIndex >= st.TokenStack.Count()) return;
                    if (selectorIndex < 0) selectorIndex++;
                    this.textbox.Text = st.TokenStack.ElementAt(selectorIndex++);
                    break;
                case Key.Down:
                    if (selectorIndex < 0) return;
                    if (selectorIndex >= st.TokenStack.Count()) selectorIndex--;
                    this.textbox.Text = st.TokenStack.ElementAt(selectorIndex--);
                    break;

            }
        }
    }
}
