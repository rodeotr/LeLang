using LangDataAccessLibrary;
using LangDataAccessLibrary.ServerDBModels;
using LangDataAccessLibrary.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace SubProgWPF.Utils
{
    public class LoadAppResources
    {

        public static RegexTypes setRegexTypes(ResourceDictionary dictionary)
        {
            RegexTypes regexTypes = new RegexTypes()
            {
                WordPattern = dictionary["IndividiualWordPattern"].ToString(),
                TimePattern = dictionary["SRT_TimePattern"].ToString()
            };

            return regexTypes;
        }
        public static RegexTypes loadRegexTypes()
        {
            //new System.Windows.Application();

            XmlDocument doc = new XmlDocument();

            doc.Load(@"ResourceDictionary\WordParsing.xml");
            
            //ResourceDictionary dict = new ResourceDictionary
            //{
            //    Source = new Uri("/SubProgWPF;component/ResourceDictionary/WordParsing.xaml",
            //                     UriKind.RelativeOrAbsolute)
            //};

            RegexTypes regexTypes = new RegexTypes()
            {
                WordPattern = doc.GetElementsByTagName("IndividiualWordPattern")[0].InnerText,
                TimePattern = doc.GetElementsByTagName("SRT_TimePattern")[0].InnerText

                
                //WordPattern = dict["IndividiualWordPattern"].ToString(),
                //TimePattern = dict["SRT_TimePattern"].ToString()
            };

            return regexTypes;
        }



    }
}
