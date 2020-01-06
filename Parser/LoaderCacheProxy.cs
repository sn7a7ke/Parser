using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using Parser.Interfaces;

namespace Parser
{
    public class LoaderCacheProxy : ILoader
    {
        private readonly ILoader _loader;
        private readonly char[] _invalidChars;
        private HtmlDocument _document;

        public string FileName { get; private set; }

        public string Dir { get; set; }

        public string FullPath => Dir + FileName;

        public List<string> Urls { get; private set; }

        public int Appeals { get; private set; }

        public int Downloads { get; private set; }

        public LoaderCacheProxy(ILoader loader)
        {
            _invalidChars = Path.GetInvalidFileNameChars().Concat(new List<char> { '.' }).ToArray();
            Dir = AppDomain.CurrentDomain.BaseDirectory + "Cache\\";
            FillCache();
            _loader = loader ?? throw new ArgumentNullException(nameof(loader));
        }


        #region ILoader
        public HtmlDocument Document
        {
            get
            {
                if (_document == null)
                {
                    if (_loader.Document.RemainderOffset > 100)
                        Save(_loader.Document);
                    return _loader.Document;
                }
                else
                    return _document;
            }
        }

        public void GetPage(IUrl url, string pendingXPath = null)
        {
            Appeals++;
            _document = null;
            FileName = Converting(url.Get());
            if (Urls.Contains(FileName) && File.Exists(FullPath))
            {
                _document = Load();
                if (_document != null)
                    return;
            }
            Downloads++;
            _loader.GetPage(url, pendingXPath);
        }

        public bool WaitEnabledElement(string xPath, int seconds = 60) => _document != null || _loader.WaitEnabledElement(xPath, seconds);
        #endregion


        public void FillCache() => Urls = Directory.GetFiles(Dir).Select(f => Path.GetFileName(f)).ToList();

        public void ClearCache() => Urls.Clear();

        private string Converting(string url) => string.Join("_", url.Split(_invalidChars));

        private void Save(HtmlDocument doc)
        {
            try
            {
                doc.Save(FullPath);
                Urls.Add(FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private HtmlDocument Load()
        {
            HtmlDocument doc;
            try
            {
                doc = new HtmlDocument();
                doc.Load(FullPath);
                return doc;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Urls.Remove(FileName);
                try
                {
                    File.Delete(FullPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return null;
        }
    }
}
