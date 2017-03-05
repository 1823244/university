using System;
using System.Data;
using System.Windows.Forms;

namespace Project
{
	public partial class MainForm : Form
	{
		bool accepted = false;
		string word = "";
		DataTable lexems = new DataTable();

		public MainForm()
		{
			InitializeComponent();
			lexems.Columns.Add("type");
			lexems.Columns.Add("value");

			richTextBox1.Text = "var aa, bb\n" +
				"begin\n" +
				"\taa := 1 + 2 + 3;\n" +
				"\tbb := 5 + aa;\n" +
				"\tif (aa > bb and bb > 10 or aa < 5) then\n" +
				"\t\taa := 1\n" +
				"\telse\n" +
				"\t\taa := 10;\n" +
				"\twrite(\"%d\", aa);\n" +
				"\tac := ab + 2 + 3\n" +
				"end";
		}

		bool check_reserved(string s)
		{
			return (s == "var" || s == "begin" || s == "end" || s == "read" || s == "write" || s == "if" || s == "then"
				|| s == "else" || s == "not" || s == "and" || s == "or");
		}

		bool add_lexem(int s)
		{
			if (word == " " || word == "\n" || word == "\t")
			{
				word = "";
				return true;
			}

			switch (s)
			{
				case 2:
					if (check_reserved(word))
						lexems.Rows.Add(word, word); 
					else
						lexems.Rows.Add("id", word);
					break;
				case 3:
					lexems.Rows.Add("num", word);
					break;
				case 5:
					word = word.Replace("'+'", "").Replace("\"+\"", "").Replace("\"+'", "").Replace("'+\"", "");
					word = '\'' + word.Substring(1, word.Length - 2).Replace("'", "''") + '\'';
					lexems.Rows.Add("string", word);
					break;
				case 8:
					if (word == "+")
						lexems.Rows.Add("plus", word);
					else
						lexems.Rows.Add("minus", word);
					break;
				case 9:
					lexems.Rows.Add("bracket", word);
					break;
				case 10:
					lexems.Rows.Add("b-bracket", word);
					break;
				case 11:
					lexems.Rows.Add("comma", word);
					break;
				case 12:
					lexems.Rows.Add("s-colon", word);
					break;
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
					lexems.Rows.Add("eq", word);
					break;
				case 19:
					lexems.Rows.Add("assign", word);
					break;
				case 21:
					lexems.Rows.Add("comment", word);
					break;
				default:
					return false;
			}

			word = "";
			return true;
		}

		private bool accept(string lexem)
		{
			word = "";
			int s = 0;
			int i = 0;

			while (i < lexem.Length)
			{
				switch (s)
				{
					case 0:
						if (!s0(lexem[i], ref s)) goto fail;
						break;
					case 1:
						if (!s1(lexem[i], ref s)) goto fail;
						break;
					case 2:
						if (!s2(lexem[i], ref s)) goto fail;
						break;
					case 3:
						if (!s3(lexem[i], ref s)) goto fail;
						break;
					case 4:
						if (!s4(lexem[i], ref s)) goto fail;
						break;
					case 5:
						if (!s5(lexem[i], ref s)) goto fail;
						break;
					case 6:
						if (!s6(lexem[i], ref s)) goto fail;
						break;
					case 7:
						if (!s7(lexem[i], ref s)) goto fail;
						break;
					case 8:
						if (!s8(lexem[i], ref s)) goto fail;
						break;
					case 9:
						if (!s9(lexem[i], ref s)) goto fail;
						break;
					case 10:
						if (!s10(lexem[i], ref s)) goto fail;
						break;
					case 11:
						if (!s11(lexem[i], ref s)) goto fail;
						break;
					case 12:
						if (!s12(lexem[i], ref s)) goto fail;
						break;
					case 13:
						if (!s13(lexem[i], ref s)) goto fail;
						break;
					case 14:
						if (!s14(lexem[i], ref s)) goto fail;
						break;
					case 15:
						if (!s15(lexem[i], ref s)) goto fail;
						break;
					case 16:
						if (!s16(lexem[i], ref s)) goto fail;
						break;
					case 17:
						if (!s17(lexem[i], ref s)) goto fail;
						break;
					case 18:
						if (!s18(lexem[i], ref s)) goto fail;
						break;
					case 19:
						if (!s19(lexem[i], ref s)) goto fail;
						break;
					case 20:
						if (!s20(lexem[i], ref s)) goto fail;
						break;
					case 21:
						if (!s21(lexem[i], ref s)) goto fail;
						break;
				}

				bool saved = s != 0;
				if (accepted)
				{
					add_lexem(s);
					accepted = false;
					s = 0;
					i--;
				}
				else
				{
					if (saved) word += lexem[i];
				}

				i++;
			}


			return add_lexem(s);

		fail:
			word += lexem.Substring(i).Split(' ', '\n')[0];
			return false;
		}

		bool s0(char c, ref int s)
		{
			word = "";
			switch (c)
			{
				case ' ':
				case '\n':
				case '\t':
					s = 0;
					break;
				case '\'':
					s = 4;
					break;
				case '"':
					s = 7;
					break;
				case '+':
				case '-':
					s = 8;
					break;
				case '(':
					s = 9;
					break;
				case ')':
					s = 10;
					break;
				case ',':
					s = 11;
					break;
				case ';':
					s = 12;
					break;
				case '=':
					s = 13;
					break;
				case '<':
					s = 14;
					break;
				case '>':
					s = 15;
					break;
				case ':':
					s = 18;
					break;
				case '{':
					s = 20;
					break;
				default:
					if (Char.IsLetter(c)) s = 1;
					else if (Char.IsDigit(c)) s = 3;
					else return false;
					break;
			}

			return true;
		}

		bool s1(char c, ref int s)
		{
			if (Char.IsLetter(c) || Char.IsDigit(c))
				s = 2;
			else
				return false;

			return true;
		}

		bool s2(char c, ref int s)
		{
			if (Char.IsLetter(c) || Char.IsDigit(c))
				s = 2;
			else
				accepted = true;

			return true;
		}

		bool s3(char c, ref int s) 
		{
			if (Char.IsDigit(c))
				s = 3;
			else
				accepted = true;

			return true;
		}

		bool s4(char c, ref int s)
		{
			if (c != '\'')
				s = 4;
			else
				s = 5;

			return true;
		}

		bool s5(char c, ref int s)
		{
			if (c == '+')
				s = 6;
			else
				accepted = true;
			
			return true;
		}

		bool s6(char c, ref int s)
		{
			if (c == '\'')
				s = 4;
			else if (c == '"')
				s = 7;
			else
				return false;

			return true;
		}

		bool s7(char c, ref int s)
		{
			if (c != '"')
				s = 7;
			else
				s = 5;

			return true;
		}

		bool s8(char c, ref int s)
		{
			accepted = true;

			return true;
		}

		bool s9(char c, ref int s)
		{
			accepted = true;

			return true;
		}

		bool s10(char c, ref int s)
		{
			accepted = true;

			return true;
		}

		bool s11(char c, ref int s)
		{
			accepted = true;

			return true;
		}

		bool s12(char c, ref int s)
		{
			accepted = true;

			return true;
		}

		bool s13(char c, ref int s)
		{
			accepted = true;

			return true;
		}

		bool s14(char c, ref int s)
		{
			if (c == '>' || c == '=')
				s = 16;
			else 
				accepted = true;
			
			return true;
		}

		bool s15(char c, ref int s)
		{
			if (c == '=')
				s = 17;
			else 
				accepted = true;
			
			return true;
		}

		bool s16(char c, ref int s)
		{
			accepted = true;

			return true;
		}

		bool s17(char c, ref int s)
		{
			accepted = true;

			return true;
		}

		bool s18(char c, ref int s)
		{
			if (c == '=')
				s = 19;
			else
				return false;

			return true;
		}

		bool s19(char c, ref int s)
		{
			accepted = true;

			return true;
		}

		bool s20(char c, ref int s)
		{
			if (c == '}')
				s = 21;
			else if (c == '{')
				return false;

			return true;
		}

		bool s21(char c, ref int s)
		{
			accepted = true;

			return true;
		}

		private void ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (richTextBox1.Text == "")
			{
				richTextBox2.Text = "";
				richTextBox3.Text = "";
				return;
			}

			lexems.Clear();

			if (!accept(richTextBox1.Text.ToLower()))
			{
				int index = richTextBox1.Text.IndexOf(word, StringComparison.CurrentCultureIgnoreCase);
				richTextBox3.Text = "Ошибка: " + word.Replace('\n', ' ') + "; Строка " + (richTextBox1.GetLineFromCharIndex(index) + 1).ToString() + ".";
				
				return;
			}
			else
			{
				richTextBox3.Text = "Лексических ошибок нет!";

				lexems.Rows.Add("-|", "-|");

				var p = new Parser();
				string code = p.Translate(lexems);

				richTextBox2.Text = code;
			}
		}
	}
}
