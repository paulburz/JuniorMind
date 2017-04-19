﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OrderWordsByNumberOfApparitionsInText
{
    [TestClass]
    public class IndexWordsTest
    {
        [TestMethod]
        public void Test_For_Search_For_Word_Function_And_Add_Word()
        {
            string text = "this words should be indexed";

            var expected = new Word[] { new Word("words"),
                        new Word("should"),new Word("be"),new Word("indexed"),new Word("this") };

            var actual = new IndexWords().IndexWordsInText(text);

            Assert.AreEqual(0, CompareWords(expected, actual));
        }
        [TestMethod]
        public void Test_For_Search_And_For_Index_Words_By_Most_Common()
        {
            string text = "this this this this words words should should should be indexed";

            var expected = new Word[] { new Word("be", 1), new Word("indexed", 1), new Word("words", 2),
                        new Word("should",3),new Word("this",4) };

            var actual = new IndexWords().IndexWordsInText(text);

                Assert.AreEqual(0, CompareWords(expected, actual) + CompareWordsByNumberOfApparitions(expected, actual));
        }

        private int CompareWords(Word[] expected, Word[] actual)
        {
            int ok = 0;
            for (int i = 0; i < expected.Length; i++)
            {
                if (IsEqualTo(expected[i], actual[i]) == false)
                {
                    ok = 1;
                    break;
                }
            }
            return ok;
        }
        private int CompareWordsByNumberOfApparitions(Word[] expected, Word[] actual)
        {
            int ok = 0;
            for (int i = 0; i < expected.Length; i++)
            {
                if (IsNumberOfApparitionsEqualTo(expected[i], actual[i]) == false)
                {
                    ok = 1;
                    break;
                }
            }
            return ok;
        }
        bool IsEqualTo(Word firstWord, Word secondWord)
        {
            return firstWord.EqualWords(secondWord);
        }
        bool IsNumberOfApparitionsEqualTo(Word firstWord, Word secondWord)
        {
            return firstWord.EqualApparitions(secondWord);
        }
    }
}