using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace WpfApp1
{
    //объяснению не подлежит - значит, что вещь очевидная:)
    public partial class MainWindow : Window
    {
        OpenFileDialog open; // диалоговое окно открытия файла
        SaveFileDialog save; // окно сохранения файла
        MessageBoxResult result; // просто переменная
        bool flag; // просто флаг
        string name; // имя файла, если ты открыл файл, а не создал новый
        public MainWindow()
        {
            InitializeComponent();
            flag = false; //объяснению не подлежит
            while (true) //просто бесконечный цикл
            {
                result = MessageBox.Show("открыть файл?", "", MessageBoxButton.YesNo); //объяснению не подлежит
                if (result == MessageBoxResult.Yes) //объяснению не подлежит
                {
                    open = new OpenFileDialog() { Filter = "Text files(*.txt) | *.txt" }; //объяснению не подлежит
                    if (open.ShowDialog() == true) //объяснению не подлежит
                    {
                        name = open.FileName; // записываю имя открытого файла
                        text.Text = File.ReadAllText(name); //открываю
                        flag = true; //объяснению не подлежит
                    }
                    break;//объяснению не подлежит
                }
                else//объяснению не подлежит
                {
                    result = MessageBox.Show("Создать новый файл?", "", MessageBoxButton.YesNo);//объяснению не подлежит
                    if (result == MessageBoxResult.Yes)//объяснению не подлежит
                    {
                        break;//объяснению не подлежит
                    }
                }
            }
            font.SelectedIndex = 0; //шрифт по умолчанию
        }
        private void font_SelectionChanged(object sender, SelectionChangedEventArgs e) //объяснению не подлежит
        {
            switch (font.SelectedIndex) 
            {
                case 0:
                    text.FontFamily = new FontFamily("Segoe UI");
                    break;
                case 1:
                    text.FontFamily = new FontFamily("Arial");
                    break;
                case 2:
                    text.FontFamily = new FontFamily("Courier New");
                    break;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            result = MessageBox.Show("Сохранить?", "", MessageBoxButton.YesNoCancel); //объяснению не подлежит
            if (result == MessageBoxResult.Yes)
            {
                save = new SaveFileDialog() { Filter = "Text files(*.txt) | *.txt", FileName = "Новый текстовый документ" };//объяснению не подлежит
                if (!flag) // а вот теперь этот флаг используется для проверки: открыл ли ты или создал файл. Ну понятно, чтобы лишний раз не мучиться
                {
                    if (save.ShowDialog() == true)
                    {
                        File.WriteAllText(save.FileName, text.Text);
                        result = MessageBox.Show("Зашифровать?", "", MessageBoxButton.YesNo); //объяснению не подлежит
                        if (result == MessageBoxResult.Yes)
                        {
                            File.Encrypt(save.FileName);
                        }
                        
                    }
                    else e.Cancel = true;
                }
                else
                {
                    File.WriteAllText(save.FileName, text.Text);
                    result = MessageBox.Show("Зашифровать?", "", MessageBoxButton.YesNo); //объяснению не подлежит
                    if (result == MessageBoxResult.Yes)
                    {
                        File.Encrypt(save.FileName); // шифрую
                    }
                }
            }
            else if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
