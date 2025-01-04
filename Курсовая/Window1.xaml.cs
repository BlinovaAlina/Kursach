using Newtonsoft.Json;
using System;
using System.IO;
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
using System.Windows.Shapes;

namespace Курсовая
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        public List<AddWords> list_new;

        public Window1()
        {
            InitializeComponent();

            Category_ComboBox.Items.Add("Фрукты");
            Category_ComboBox.Items.Add("Животные");
            Category_ComboBox.Items.Add("Мебель");
            Category_ComboBox.SelectedIndex = 0;

            EnglishWord_textbox.Text = "";
            RussianWord_textbox.Text = "";
            
            list_new = new List<AddWords>();

            string nekaya_stroka = "";
        }

        private void AddMore_Click(object sender, RoutedEventArgs e)
        {
            AddWord(true);
        }

        private void AddAndClose_button_Click(object sender, RoutedEventArgs e)
        {
            AddWord(false);
        }

        private void AddWord(bool flag=false)
        {
            if (EnglishWord_textbox.Text == "" || RussianWord_textbox.Text == "")
            {
                if (!flag) Close();
                else
                {
                    MessageBox.Show("Пустые значения !");
                    return;
                }
            }

            AddWords addWords = new AddWords();

            switch (Category_ComboBox.SelectedItem.ToString())
            {
                case "Фрукты":
                    addWords = new FruitsWords();
                    break;
                case "Животные":
                    addWords = new AnimalsWords();
                    break;
                case "Мебель":
                    addWords = new FurnitureWords();
                    break;
            }

            //addWords.Category = Category_ComboBox.SelectedItem.ToString();
            
            addWords.EnglishWord = EnglishWord_textbox.Text;
            addWords.RussianWord = RussianWord_textbox.Text;

            //string pathJsonFile = Environment.CurrentDirectory + "\\Dictionary.json";

            //if (File.Exists(pathJsonFile))
            //{
            //    var jsonData = File.ReadAllText(pathJsonFile);
                //list_new = JsonConvert.DeserializeObject<List<AddWords>>(jsonData);
            //}

            if (list_new.Find(x => x.EnglishWord == addWords.EnglishWord) == null) list_new.Add(addWords);

            //string output = JsonConvert.SerializeObject(list_new, Formatting.Indented);
            //File.WriteAllText(pathJsonFile, output);

            if (flag)
            {
                EnglishWord_textbox.Text = "";
                RussianWord_textbox.Text = "";
            }
            else
            {
                Close();
            }

        }
    }
}
