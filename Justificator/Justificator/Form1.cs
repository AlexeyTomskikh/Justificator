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
            var text = textBox1.Text;
            var lines = MakeStrings(100, text);


            var sb = new StringBuilder();

            foreach (var item in lines)
            {
                if (lines.IndexOf(item) != lines.Count - 1)
                {
                    var result = Justify(100, item);
                    sb.AppendLine(result);
                }
                else
                {
                    sb.AppendLine(item);
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

            var wordCount = data.Count - 1;
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

        public List<string> MakeStrings(int length, string sourcestext)
        {
            var wordArray = sourcestext.Split(new[] { "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries);

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
    }
}
