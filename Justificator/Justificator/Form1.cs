using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Justificator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sourceText = textBox1.Text;

            int param = 100;
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                param = Convert.ToInt32(textBox3.Text);
            }

            List<string> nonAlignLines = new List<string>();
            if (!checkBox1.Checked)
            {
                // формируем список параграфов
                var paragraphList = MakeParagraph(param, sourceText);

                // готовим список строк (не выровненный) на основе параграфов
                nonAlignLines = new List<string>();
                foreach (var item in paragraphList)
                {
                    var oneParagraf = MakeStrings(param, item, checkBox1.Checked);
                    nonAlignLines.AddRange(oneParagraf);
                }
            }
            else
            {
                nonAlignLines = MakeStrings(param, sourceText, checkBox1.Checked);
            }
            

            // выравниваем каждую строку
            var sb = new StringBuilder();
            foreach (var line in nonAlignLines)
            {
                if (!line.EndsWith("\r\n"))
                {
                    var result1 = Justify(param, line);
                    sb.AppendLine(result1);
                }
                else
                {
                    sb.AppendLine(line);
                }
            }

            textBox2.Text = sb.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public string Justify(int length, string sourceString)
        {
            var data = sourceString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            int totalWordLenght = 0;
            foreach (var word in data)
            {
                totalWordLenght += word.Length;
            }

            if (totalWordLenght > length)
            {
                throw new ArgumentException("Переданная строка превышает максимально заданную длину");
            }

            var leftOverSpace = length - totalWordLenght;

            var wordCount = data.Count;
            var spaceperWord = leftOverSpace / wordCount;
            var remainder = leftOverSpace % wordCount;

            var sb = new StringBuilder();

            foreach (var word in data)
            {
                sb.Append(word);

                if (word != data[data.Count - 1])
                {
                    for (int i = 0; i < spaceperWord; i++)
                    {
                        sb.Append(" ");
                    }

                    if (remainder > 0)
                    {
                        sb.Append(" ");
                        remainder -= 1;
                    }
                }
            }

            return sb.ToString();
        }

        // Формирует список параграфов
        public List<string> MakeParagraph(int length, string sourceText)
        {
            var list = sourceText.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            for(int i = 0; i < list.Length; i++)
            {
                list[i] = list[i] + "\r\n";
            }

            

            return list.ToList();
        }

        public List<string> MakeStrings(int length, string sourcestext, bool withoutParagraph)
        {
            string[] splitOptions = withoutParagraph ? new[] { " ", "\r\n" } : new[] { " " };

            var wordArray = sourcestext.Split(splitOptions, StringSplitOptions.RemoveEmptyEntries);

            var sb = new StringBuilder();
            var list = new List<string>();
            foreach (var word in wordArray)
            {
                if (word.Length > length)
                {
                    throw new ArgumentException($"Заданная длина строки меньше чем длина слова {word}");
                }

                if (sb.Length + word.Length < length)
                {
                    var ind = Array.IndexOf(wordArray, word);
                    if (ind == wordArray.Length - 1)
                    {
                        sb.Append(word);
                    }
                    else
                    {
                        sb.Append(word + " ");
                    }
                }
                else
                {
                    list.Add(sb.ToString());
                    sb.Clear();
                    sb.Append(word + " ");
                }
            }

            list.Add(sb.ToString());

            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].EndsWith(" "))
                {
                    list[i] = list[i].TrimEnd(' ');
                }
            }
            
            return list;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
