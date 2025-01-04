using Microsoft.Win32;
using Newtonsoft.Json;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Курсовая
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<AddWords> list;
        List<AddWords> select_list;
        List<AddWords> usedwords_list;
        List<Category> category_list;
        
        AddWords using_word;
        int Right_Answers;
        int Wrong_Answers;

        bool no_click = true;
        public MainWindow()
        {
            InitializeComponent();

            usedwords_list = new List<AddWords>();
            
            //Category_ComboBox.Items.Add("Фрукты");
            //Category_ComboBox.Items.Add("Животные");
            //Category_ComboBox.Items.Add("Мебель");
            //Category_ComboBox.SelectedIndex = 0;
            
            category_list = new List<Category>();


            //select_list = list.Where(x => x.Category == "Фрукты").ToList();

        }

        private void AddWord_button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.ShowDialog();

            if (window1.list_new.Count > 0)
            {
                //list = window1.list;
                if (list == null) list = window1.list_new;
                else
                {
                    for (int i = 0; i < window1.list_new.Count; i++)
                    if (list.Find(x => x.EnglishWord == window1.list_new[i].EnglishWord) == null) list.Add(window1.list_new[i]);
                }
                
                SelectCategoryFromList();
            }

        }

        private void SelectCategoryFromList()
        {
            string category_name = "";
            if (Category_ComboBox.SelectedItem != null) category_name = Category_ComboBox.SelectedItem.ToString();
            if (list != null) select_list = list.Where(x => x.Category.Name == category_name).ToList();

            Category cat = category_list.Find(x => x.Name == category_name);
            if (cat != null)
            {
                Caterory_border.Background = new SolidColorBrush(cat.Color);
            }
        }

        private void Category_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectCategoryFromList();
        }

        private void Load_button_Click(object sender, RoutedEventArgs e)
        {
            // создание диалога
            OpenFileDialog dlg = new OpenFileDialog();

            // настройка параметров диалога
            dlg.FileName = "Dictionary"; // Имя файла по умолчанию
            dlg.DefaultExt = ".json"; // Расширение файла по умолчанию
            dlg.Filter = "Text documents (*.json)|*.json"; // Фильтр файлов по расширению

            // вызов диалога
            bool? result = dlg.ShowDialog(); // Сохраняем результат вызова диалога

            // проверка, был ли выбран файл
            if (result == true)
            {
                //string t = File.ReadAllText(dlg.FileName); // Добавляем имя файла в список
                //string pathJsonFile = Environment.CurrentDirectory + "\\Dictionary.json";

                if (System.IO.File.Exists(dlg.FileName))
                {
                    var jsonData = System.IO.File.ReadAllText(dlg.FileName);
                    list = JsonConvert.DeserializeObject<List<AddWords>>(jsonData);
                    //var cat_list = list.Select(x => x.Category).GroupBy(x => x.Name).ToList();
                    var cat_lists = list.GroupBy(item => new { item.Category.Name, item.Category.Color }).Select(group => group.Key).ToList();
                    //var cat_lists2 = list.Select(x => new { x.Category }).GroupBy(gr => gr.Category.Name).Select(dd=>dd.Key).ToList();
                    foreach (var cat in cat_lists)
                    {
                        category_list.Add(new Category() { Name = cat.Name, Color = cat.Color });
                        Category_ComboBox.Items.Add(cat.Name);
                    }
                    Category_ComboBox.SelectedIndex = 0;

                    MessageBox.Show("Файл прочитан.");
                }
                else { list = new List<AddWords>(); category_list = new List<Category>(); }
            }

            SelectCategoryFromList();
        }

        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            //создание диалога
            SaveFileDialog dlg = new SaveFileDialog();
            //настройка параметров диалога
            dlg.FileName = "Dictionary"; // Default file name
            dlg.DefaultExt = ".json"; // Default file extension
            dlg.Filter = "Text documents (.json)|*.json"; // Filter files by extension

            dlg.ShowDialog();
            //получение выбранного имени файла

            string output = JsonConvert.SerializeObject(list, Formatting.Indented);
            System.IO.File.WriteAllText(dlg.FileName, output);
            
        }

        private void Start_button_Click(object sender, RoutedEventArgs e)
        {
            using_word = null;
            usedwords_list = new List<AddWords>();

            Right_Answers = 0;
            Wrong_Answers = 0;
            rightwords_label.Content = Right_Answers.ToString();
            wrongwords_label.Content = Wrong_Answers.ToString();

            NextWord_button_Click(null, null);
        }

        private void translationvar1_label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            InLabel(translationvar1_label);

        }

        private void translationvar2_label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            InLabel(translationvar2_label);
        }

        private void translationvar3_label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            
            InLabel(translationvar3_label);
        }

        private void InLabel(Label lbl)
        {
            if (no_click) return;

            if (lbl.Content == using_word.RussianWord)
            {
                MessageBox.Show("Правильно!");
                lbl.Background = Brushes.Green; // {  Color.FromRgb(87, 222, 51) };
                Right_Answers++;
            }
            else
            {
                MessageBox.Show("Неправильно!");
                lbl.Background = Brushes.Red;
                Wrong_Answers++;

                if (translationvar1_label.Content == using_word.RussianWord) translationvar1_label.BorderBrush = Brushes.Green;
                if (translationvar2_label.Content == using_word.RussianWord) translationvar2_label.BorderBrush = Brushes.Green;
                if (translationvar3_label.Content == using_word.RussianWord) translationvar3_label.BorderBrush = Brushes.Green;

            }
            rightwords_label.Content = Right_Answers.ToString();
            wrongwords_label.Content = Wrong_Answers.ToString();
            no_click = true;
        }

        private void NextWord_button_Click(object sender, RoutedEventArgs e)
        {
            if (select_list == null || select_list.Count < 3)
            {
                MessageBox.Show("Сначала загрузите список!");
                return;
            }

            
            no_click = false;

            if (using_word != null) usedwords_list.Add(using_word);
            
            translationvar1_label.Background = Brushes.Transparent;
            translationvar2_label.Background = Brushes.Transparent;
            translationvar3_label.Background = Brushes.Transparent;
            translationvar1_label.BorderBrush = Brushes.Transparent;
            translationvar2_label.BorderBrush = Brushes.Transparent;
            translationvar3_label.BorderBrush = Brushes.Transparent;

            if (usedwords_list.Count == select_list.Count)
            {
                if (Wrong_Answers > 0)
                {
                    MessageBox.Show("Вы прошли все слова в этой категории, но у вас есть ошибки :_(" +Environment.NewLine+ "Начните новую категорию или потренируйтесь ещё! ");
                }
                else
                {
                    MessageBox.Show("Вы прошли все слова в этой категории без ошибок! Молодцы! :)" + Environment.NewLine + "Можете начать новую категорию!");
                }
                return;
            }

            Random rnd = new Random();
            int index;

            while (true)
            {
                index = rnd.Next(0, select_list.Count);
                if (usedwords_list.Count > 0)
                { if (usedwords_list.Find(x => x.EnglishWord == select_list[index].EnglishWord) == null) break; }
                else break;
            }

            using_word = select_list[index];

            englishword_label.Content = select_list[index].EnglishWord;
            string russianWord1, russianWord2, russianWord3;
            russianWord1 = select_list[index].RussianWord;

            while (true)
            {
                index = rnd.Next(0, select_list.Count);
                if (select_list[index].RussianWord != russianWord1)
                {
                    russianWord2 = select_list[index].RussianWord;
                    break;
                }
            }

            while (true)
            {
                index = rnd.Next(0, select_list.Count);
                if (select_list[index].RussianWord != russianWord1 && select_list[index].RussianWord != russianWord2)
                {
                    russianWord3 = select_list[index].RussianWord;
                    break;
                }
            }

            int r_index = rnd.Next(0, 3);
            switch (r_index)
            {
                case 0:
                    translationvar1_label.Content = russianWord1;

                    translationvar2_label.Content = russianWord2;
                    translationvar3_label.Content = russianWord3;
                    break;
                case 1:
                    translationvar2_label.Content = russianWord1;

                    translationvar1_label.Content = russianWord2;
                    translationvar3_label.Content = russianWord3;
                    break;
                case 2:
                    translationvar3_label.Content = russianWord1;

                    translationvar1_label.Content = russianWord2;
                    translationvar2_label.Content = russianWord3;
                    break;
            }
        }
    }
}
