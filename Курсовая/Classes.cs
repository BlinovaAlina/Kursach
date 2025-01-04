using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace Курсовая
{


    public interface IWord
    {
        string EnglishWord { get; set; }
        string RussianWord { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
        public Color Color { get; set; }
    }

    public class AddWords : IWord
    {
        public Category Category;
        public string EnglishWord { get; set; }
        public string RussianWord { get; set; }
    }

    public class FruitsWords : AddWords
    {
        public FruitsWords() 
        {
            Category = new Category() { Name = "Фрукты", Color = Brushes.Yellow.Color };
        }
    }
    public class AnimalsWords : AddWords
    {
        public AnimalsWords()
        {
            Category = new Category() { Name = "Животные", Color = Brushes.Orange.Color };
        }
    }
    public class FurnitureWords : AddWords
    {
        public FurnitureWords()
        {
            Category = new Category() { Name = "Мебель", Color = Brushes.Blue.Color };
        }
    }




}
