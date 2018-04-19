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
        private string _fileName;

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

        public Section this[string name]
        {
            get { return _sections[name]; }
            set { _sections[name] = value; }
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public SectionCollection Sections
        {
            get { return _sections; }
            set {
                if(_sections == null) throw new ArgumentNullException("Sections不能为null");
                _sections = value;
            }
        }

        public List<Comment> Comments
        {
            get { return _comments; }
            set {
                if(_comments == null) throw new ArgumentNullException("Comments不能为null");
                _comments = value;
            }
        }

        public Parameter GetParameter(string section, string key)
        {
            var sec = _sections[section];
            return sec?[key];
        }

        public string Read(string section, string key)
        {
            var par = GetParameter(section, key);
            return par?.Value;
        }

        public bool Write(string section, string key, string value)
        {
            var par = GetParameter(section, key);
            if (par == null) return false;
            par.Value = value;
            return true;
        }

        public void WriteNew(string section, string key, string value)
        {
            var sec = _sections[section];
            if(sec == null) sec = _sections.Add(new Section(section));
            var par = sec[key];
            if (par == null) par = sec.Parameters.Add(new Parameter(key, value));
            par.Value = value;
        }

        public int ParseFromFile(string filePath)
        {
            return ParseFromFile(filePath, Encoding.UTF8);
        }

        public virtual int ParseFromFile(string filePath, Encoding encoding)
        {
            var info = new FileInfo(filePath);
            if (!info.Exists) throw new IOException(string.Format("未能找到文件:{0}", info.Name));
            if (info.Extension != ".ini") throw new ArgumentException("文件必须是扩展名为ini的配置文件");
            _fileName = info.FullName;
            var lines = File.ReadAllLines(filePath, encoding);

            Section section = null;
            var buffer = new StringBuilder();
            for(int i = 0; i < lines.Length; ++i)
            {
                var line = lines[i];
                var lineId = i + 1;
                string key = null; int keyP = 0;
                string value = null; //int valueP = 0;
                Comment comment = null;
                bool newSection = false;
                int tp = 0;//第一个非空格字符位置
                
                var sr = new StringReader(line); char c;
                for(int p = 0; p < line.Length; p++)
                {
                    var ci = sr.Read();
                    c = (char)ci;
                    if ((c == ' ' || c == '\t') && buffer.Length == 0) continue;//跳过前导空白字符
                    if (c == ';') {
                        comment = new Comment(sr.ReadToEnd(), lineId, p);
                        break;
                    }
                    //if (newSection) throw new FileParseException("[Section]后不允许存在非注释内容", line, lineId, p);
                    if (c == '=') {
                        if(key == null && !newSection) {
                            key = buffer.Pull();
                            if (key.Length == 0) throw new FileParseException("Key不能为空", line, lineId, p);
                            keyP = tp;
                            continue;
                        }
                    } else if (c == '[') {
                        if(key == null) {
                            if (buffer.Length != 0) throw new FileParseException("[Section]前不允许存在字符且必须独占一行", line, lineId, p);
                            continue;
                        }
                    } else if (c == ']') {
                        if(key == null) {
                            if (buffer.Length == 0) throw new FileParseException("[Section]名称不能为空", line, lineId, p);
                            if (section != null) _sections[section.Name] = section;
                            section = _sections.GetNewSection(buffer.Pull().Trim(), lineId, p);
                            newSection = true;
                            continue;
                        }
                    }
                    if(buffer.Length == 0) tp = p;
                    buffer.Append((char)c);
                }
                Parameter parameter = null;
                if(key != null) {
                    key = key.Trim();
                    value = buffer.ToString().Trim();
                    if (section == null) throw new FileParseException(string.Format("键值对{0}={1}不属于任何Section", key, value), line, lineId, keyP);
                    parameter = section.Parameters.GetNewParameter(key, value, lineId, keyP);
                }
                if(comment != null) {
                    if (parameter != null) parameter.Comment = comment;
                    else if (section != null) section.Comments.Add(comment);
                    else Comments.Add(comment);
                }
                if(parameter != null) {
                    section.Parameters[parameter.Key] = parameter;
                }
                buffer.Clear();
            }
            if (section != null) _sections[section.Name] = section;

            return lines.Length;
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public virtual string ToString(bool withComment)
        {
            var sb = new StringBuilder();
            var nl = Environment.NewLine;
            if(withComment) {
                foreach (var comment in _comments) {
                    sb.Append(comment.ToString());
                    sb.Append(nl);
                }
            }
            foreach(var section in _sections) {
                sb.Append(section.ToString(withComment));
                sb.Append(nl);
            }

            return sb.ToString();
        }

        public string Save()
        {
            return Save(_fileName);
        }

        public string Save(string fileName)
        {
            return Save(fileName, true);
        }

        public string Save(string fileName, bool withComment)
        {
            return Save(fileName, withComment, Encoding.UTF8);
        }

        public string Save(string fileName, bool withComment, Encoding encoding)
        {
            if (fileName == null) throw new ArgumentNullException("文件路径不能为空");
            if (!fileName.EndsWith(".ini")) fileName += ".ini";
            var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            var s = ToString(withComment);
            var bytes = encoding.GetBytes(s);
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Flush();
            fileStream.Close();
            return new FileInfo(fileName).FullName;
        }
    }

    static class StringBuilderEx
    {
        public static string Pull(this StringBuilder sb)
        {
            var s = sb.ToString();
            sb.Clear();
            return s;
        }
    }

    class FileParseException : Exception
    {
        public int Line { get; set; }

        public int Offset { get; set; }

        public string Content { get; set; }

        public FileParseException(string message) : base(message)
        {
        }

        public FileParseException(string message, string content, int line, int offset) : this(message)
        {
            Content = content;
            Line = line;
            Offset = offset;
        }

        public override string Message
        {
            get
            {
                var messgae = string.Format(@"{3}{0}{3}第 {4} 行 第 {5} 个字符{3}{1}{3}{2}^", base.Message, Content, new string(' ', Offset), Environment.NewLine, Line, Offset + 1);
                return messgae;
            }
        }
    }
}
