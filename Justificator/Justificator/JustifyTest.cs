using System;
using NUnit.Framework;

namespace Justificator
{
    public class JustifyTest
    {
        [Test]
        public void MakeStrings_Test()
        {
            var form = new Form1();
            var testString1 = "123 45 5 6 6789 1234567"; 
            var testString2 = "1 1 1 1 1 1 1 1 1 1 1 1";
            var result1 = form.MakeStrings(7, testString1, false);
            var result2 = form.MakeStrings(6, testString2, false);
            Assert.AreEqual(4, result1.Count);
            Assert.AreEqual(4, result2.Count);
        }

        [Test]
        public void MakeStrings_Test_Negative()
        {
            var form = new Form1();
            var testString = "Иван Петр петрович , 4";
            Assert.Throws<ArgumentException>(() => form.MakeStrings(4, testString, false));
        }

        [Test]
        public void MakeStringsWithBigData_Test()
        {
            var form = new Form1();
            var testString = "«Общество» – Микрофинансовая  компания «Лайм-Займ» (Общество с ограниченной ответственностью)  ИНН 7724889891 ОГРН 1137746831606. «Кредитный отчет»  -документ,  который содержит  информацию,  входящую в  состав кредитной истории,  и который  Бюро предоставляет  по запросу  Партнера в  соответствии с  настоящим согласием. «Бюро кредитных   историй»     –   Бюро кредитных     историй    «Эквифакс Кредит   Сервисиз»  129090, г.Москва,  Каланчевская ул., д.16,   стр. 1(2 этаж, офис 2.09), и(или)  Акционерное общество  «Национальное бюро кредитных историй»  121069,  г.Москва,  Скатертный пер., д. 20,   стр. 1   и(или)  Закрытое акционерное общество «Объединенное Кредитное Бюро»   г.Москва,   1 - ая   Тверская - Ямская,  2,   стр.1    либо иное юридическое лицо, оказывающее   услуги по   формированию,  обработке и хранению кредитных историй, а также по предоставлению кредитных отчетов   и сопутствующих услуг,   с которым   у Общества   имеется заключенный договор.";
            var list = form.MakeStrings(60, testString, false);

            foreach (var item in list)
            {
                Assert.That(item.Length <= 60);
            }
        }

        [Test]
        public void Justify_Test()
        {
            var form = new Form1();
            var testString = "Иван Вася";
            var testString2 = "<12 12 12 , ,2 3,";

            var result = form.Justify(10, testString);
            var result2 = form.Justify(25, testString2);

            Assert.AreEqual(10, result.Length);
            Assert.AreEqual(25, result2.Length);

        }

        [Test]
        public void Justify_Test_Negative()
        {
            var form = new Form1();
            var testString = "«Общество» – Микрофинансовая  компания «Лайм-Займ» (Общество с ограниченной ответственностью)  ИНН 7724889891 ОГРН 1137746831606. «Кредитный отчет»  -документ,  который содержит  информацию,  входящую в  состав кредитной истории,  и который  Бюро предоставляет  по запросу  Партнера в  соответствии с  настоящим согласием. «Бюро кредитных   историй»     –   Бюро кредитных     историй    «Эквифакс Кредит   Сервисиз»  129090, г.Москва,  Каланчевская ул., д.16,   стр. 1(2 этаж, офис 2.09), и(или)  Акционерное общество  «Национальное бюро кредитных историй»  121069,  г.Москва,  Скатертный пер., д. 20,   стр. 1   и(или)  Закрытое акционерное общество «Объединенное Кредитное Бюро»   г.Москва,   1 - ая   Тверская - Ямская,  2,   стр.1    либо иное юридическое лицо, оказывающее   услуги по   формированию,  обработке и хранению кредитных историй, а также по предоставлению кредитных отчетов   и сопутствующих услуг,   с которым   у Общества   имеется заключенный договор.";
            Assert.Throws<ArgumentException>(() => form.Justify(10, testString));
        }
    }
}
