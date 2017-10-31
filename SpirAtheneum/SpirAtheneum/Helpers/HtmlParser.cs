using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpirAtheneum.Helpers
{
    class HtmlParser
    {
        /// <summary>
        /// Formats the Html String to plain text and replaces tags with "\n"
        /// </summary>
        /// <param name="html">The Html String</param>
        /// <returns></returns>
        public string GetFormattedTextFromHtml(string html)
        {
            //cut everything out that's before <body tag
            if (html.Contains("<body"))
            {
                html = SelectContiguousHtmlTag("body", html);
            }

            //find all tags
            //alt regex: \<([\w]+?)\>
            string regex = @"\<(.*?)\>";

            MatchCollection tags = Regex.Matches(html, regex);
            //for if we decide to use the delimiter way instead. USE Delimiters if body will include more than the below tags
            //List<string> delimiters = new List<string>();

            foreach (Match match in tags)
            {
                //delimiters.Add(match.Value);

                //if using delimter, do not need this if statement
                if (match.Value.Contains("<h2") || match.Value.Contains("<h3") || match.Value.Contains("<h4") || match.Value.Contains("<h5"))
                {
                    html = html.Replace(match.Value, "\n\n\n");
                }
                else if (match.Value.Contains("<p") || match.Value.Contains("<li"))
                {
                    html = html.Replace(match.Value, "\n\n");
                }
                else
                {
                    html = html.Replace(match.Value, "");
                }
                //
            }

            ////create array of paragraphs split by html tags we found
            //string[] split = html.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            //foreach (string s in split)
            //{
            //    html = String.Join("\n\n", split);
            //}

            return html;
        }

        private string SelectContiguousHtmlTag(string word, string text)
        {
            List<char> letters = new List<char>();
            List<char> textList = new List<char>();
            var list = text.ToCharArray();

            foreach (var l in list)
            {
                textList.Add(l);
            }

            //doesn't even have the tag we're looking for
            if (!text.Contains("<" + word))
            { return String.Empty; }

            //find index for '<' and '>' of first tag
            int ltIndex = textList.IndexOf('<');
            int gtIndex = textList.FindIndex(e => e == '>');

            //get the text without the first tag
            string newText = text.Substring(gtIndex + 1);

            //construct the tag. Everything between '<' and '>' May include attributes
            for (int i = ltIndex; i <= gtIndex; i++)
            {
                letters.Add(textList.ElementAt(i));
            }

            //tag as a string
            string tag = String.Join("", letters);

            //if not an actual tag. Probably didn't find a '>'
            if (String.IsNullOrEmpty(tag))
            {
                return SelectContiguousHtmlTag(word, newText);
            }

            //look for a space, which indicates attributes so: <body class=...
            int firstSpaceIndex = letters.FindIndex(e => e == ' ');

            //if tag does not have attributes.
            if (firstSpaceIndex != -1)
            {
                //remove all after space (all tag's attributes)
                tag = tag.Remove(firstSpaceIndex);
            }
            else
            {
                //remove only '>'
                List<char> tagList = new List<char>();

                var tList = tag.ToCharArray();

                foreach (var l in tList)
                {
                    tagList.Add(l);
                }

                tag = tag.Remove(tagList.FindIndex(e => e == '>'));
            }

            if (tag == "<" + word)
            {
                return text.Substring(ltIndex); ;
            }

            return SelectContiguousHtmlTag(word, newText);


        }
    }
}
