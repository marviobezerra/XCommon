using System;
using System.IO;
using System.Text;

namespace XCommon.Util
{
	public class StringBuilderIndented
	{
		private const byte IndentSize = 4;

		private byte _indent;
		private bool _indentPending = true;

		private readonly StringBuilder _stringBuilder = new StringBuilder();

		public StringBuilderIndented()
		{
		}

		public StringBuilderIndented(StringBuilderIndented from)
		{
			_indent = from._indent;
		}

		public virtual int Length => _stringBuilder.Length;

		public virtual StringBuilderIndented Append(object o)
		{
			DoIndent();

			_stringBuilder.Append(o);

			return this;
		}

		public virtual StringBuilderIndented AppendLine()
		{
			AppendLine(string.Empty);

			return this;
		}

		public virtual StringBuilderIndented AppendLine(object o)
		{
			var value = o.ToString();

			if (value != string.Empty)
			{
				DoIndent();
			}

			_stringBuilder.AppendLine(value);

			_indentPending = true;

			return this;
		}

		public virtual StringBuilderIndented AppendLines(object o)
		{
			using (var reader = new StringReader(o.ToString()))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					AppendLine(line);
				}
			}

			return this;
		}

		public virtual StringBuilderIndented Clear()
		{
			_stringBuilder.Clear();

			return this;
		}

		public virtual StringBuilderIndented IncrementIndent()
		{
			_indent++;
			return this;
		}

		public virtual StringBuilderIndented DecrementIndent()
		{
			if (_indent > 0)
			{
				_indent--;
			}
			return this;
		}

		public virtual IDisposable Indent() => new Indenter(this);

		public override string ToString() => _stringBuilder.ToString();

		private void DoIndent()
		{
			if (_indentPending && (_indent > 0))
			{
				_stringBuilder.Append(new string(' ', _indent * IndentSize));
			}

			_indentPending = false;
		}

		private sealed class Indenter : IDisposable
		{
			private readonly StringBuilderIndented _stringBuilder;

			public Indenter(StringBuilderIndented stringBuilder)
			{
				_stringBuilder = stringBuilder;

				_stringBuilder.IncrementIndent();
			}

			public void Dispose() => _stringBuilder.DecrementIndent();
		}
	}
}
