using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace Initialization
{
    class Initialization : IEnumerable<Section>
    {

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        public static string Read(string filePath, string section, string key)
        {
            var buffer = new StringBuilder(255);
            var r = GetPrivateProfileString(section, key, "", buffer, 255, filePath);
            return buffer.ToString();
        }

        public static long Write(string filePath, string section, string key, string value)
        {
            var r = WritePrivateProfileString(section, key, value, filePath);
            return r;
        }

        protected SectionCollection _sections;
        protected List<Comment> _comments;

        public Initialization()
        {
            _sections = new SectionCollection();
            _comments = new List<Comment>();
        }

        public Initialization(string filePath) : this(filePath, Encoding.UTF8)
        {
        }

        public Initialization(string filePath, Encoding encoding) : this()
        {
            ParseFromFile(filePath, encoding);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Sections.GetEnumerator();
        }

        public IEnumerator<Section> GetEnumerator()
        {
            return Sections.GetEnumerator();
        }

        public string FileName { get; set; }

        public SectionCollection Sections
        {
            get { return _sections; }
            set { _sections = value; }
        }

        public List<Comment> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        protected void ParseFromFile(string filePath, Encoding encoding)
        {
            var info = new FileInfo(filePath);
            if (!info.Exists) throw new IOException(string.Format("未能找到文件:{0}", info.Name));
            if (info.Extension != ".ini") throw new ArgumentException("文件必须是扩展名为ini的配置文件");
            var lines = File.ReadAllLines(filePath);

            Section section = null;
            var buffer = new StringBuilder();
            for(int i = 0; i < lines.Length; ++i)
            {
                string key = null; int keyP;
                string name = null; int nameP;
                string comment = null; int commentP;
                int tp;//第一个非空格字符位置
                var line = lines[i];
                var sr = new StringReader(line);
                int c;
                for(int p = 0; p < line.Length; p++)
                {
                    c = sr.Read();
                    if (c == ' ' && buffer.Length == 0) continue;//跳过前导空格
                    if (c == ';')
                    {
                        if (key != null)
                        {
                            name = buffer.ToString();
                            buffer.Clear();
                        }
                        comment = sr.ReadToEnd(); commentP = p;
                        break;
                    }
                    else if (c == '[')
                    {
                        
                    }
                    else if (c == '=')
                    {

                    }
                    if(buffer.Length == 0) tp = 0;

                    buffer.Append((char)c);
                }
                if(key != null) name = buffer.ToString();
                buffer.Clear();
            }
        }
    }

    class InitializationFileParseException : Exception, IPositionable
    {
        public int Line { get; set; }

        public int Offset { get; set; }

        public string Content { get; set; }

        public InitializationFileParseException(string content, int line, int offset)
        {
            Content = content;
            Line = line;
            Offset = offset;
        }
    }
}
