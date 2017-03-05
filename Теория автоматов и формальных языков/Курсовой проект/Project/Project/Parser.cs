using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Project
{
	class Parser
	{
		int expsCount = 0;
		int labelsCounter = 0;
		int index = 0;
		int errorCode = 0;
		Stack<string> exps = new Stack<string>();
		string curExp = "";
		DataTable lexems;

		public string Translate(DataTable lexem_table)
		{
			lexems = lexem_table;
			if (lexems.RCount() == 0)
				return "";

			index = 0;
			var outString = A();

			if (outString != null && errorCode == 0 && index < lexems.RCount() && lexems.Type(index) == "-|")
				return outString;
			else
				return GetLastError();
		}

		// A -> aBbCc
		/*
		 * A — <программа>
		 * B — <переменные>
		 * C — <операторы>
		 * a — var
		 * b — begin
		 * c — end
		*/
		string A()
		{
			if (lexems.Type(index++) == "var")
			{
				string outString = B();
				if (outString == null)
					return null;

				outString = "#include <stdio.h>\n\nint main() {\n\tint " + outString;
				
				if (lexems.Type(index++) == "begin")
				{
					string opString = C();
					if (opString == null)
						return null;

					if (lexems.Type(index++) == "end")
					{
						string variables = "";

						expsCount--;
						for (int i = 0; i <= expsCount; i++)
						{
							variables += ", exp" + i.ToString();
						}

						variables += ";\n";

						return outString + variables + opString + "\n\treturn 0;\n}";
					}

					errorCode = 3;
					return null;
				}

				errorCode = 2;
				return null;
			}

			errorCode = 1;
			return null;
		}

		// B —> dJ
 		/* 
		 * B — <переменные>
		 * J — <доп. нетерминал>
		 * d — id
		 */
		string B()
		{
			if (lexems.Type(index) == "id")
				return lexems.Value(index++) + J();

			errorCode = 4;
			return null;
		}

		// J -> ,B
		// J -> ε
		/*
		 * J — <доп. нетерминал>
		 * B — <переменные>
		 */
		string J()
		{
			if (lexems.Type(index) == "comma")
			{
				var outString = lexems.Value(index++) + ' ';
				var b = B();
				if (b == null)
					return null;
				
				return outString + b;
			}

			return ""; 
		}

		// C -> DL
		/*
		 * C — <операторы>
		 * D — <оператор>
		 * L — <доп. нетерминал>
		 */
		string C()
		{
			var d = D();
			if (d == null)
				return null;

			var l = L();
			if (l == null)
				return null;
			
			return d + l;
		}

		// L -> ;DL
		// L -> ε
		/*
		 * D — <оператор>
		 * L — <доп. нетерминал>
		 */
		string L()
		{
			if (lexems.Type(index) == "s-colon")
			{
				index++;

				var d = D();
				if (d == null)
					return null;

				var l = L();
				if (l == null)
					return null;

				return d + l;
			}

			return ""; 
		}

		// D -> e(p, d)
		// D -> f(p, d)
		// D -> dgE
		// D -> hFiDjD
		/*
		 * D — <оператор>
		 * E — <выражение>
		 * d — id
		 * e — read
		 * f — write
		 * g — assign
		 * h — if
		 * i — then
		 * j — else
		 * p — string
		 */
		string D()
		{
			var outString = "";
			if (lexems.Type(index) == "read" || lexems.Type(index) == "write")
			{
				var type = lexems.Type(index);
				var amp = "";
				if (type == "read")
				{
					amp = "&";
					outString = "\n\tscanf";
				}
				else
					outString = "\n\tprintf";

				if (lexems.Type(++index) == "bracket")
				{
					outString += lexems.Value(index++);
					if (lexems.Type(index) == "string")
					{
						var tmp = lexems.Value(index++).Replace("''", "'").Remove(0, 1);
						if (tmp != "")
							outString += "\"" + tmp.Remove(tmp.Length - 1) + "\"";
						else
							outString += "\"\"";

						if (lexems.Type(index) == "comma")
						{
							outString += lexems.Value(index++);
							if (lexems.Type(index) == "id")
							{
								outString += " " + amp + lexems.Value(index++);
								if (lexems.Type(index++) == "b-bracket")
								{
									return outString + ");\n";
								}

								errorCode = 9;
								return null;
							}

							errorCode = 4;
							return null;
						}

						errorCode = 8;
						return null;
					}

					errorCode = 7;
					return null;
				}

				errorCode = 6;
				return null;
			}

			switch (lexems.Type(index))
			{
				case "id":
					outString = lexems.Value(index++);
					if (lexems.Type(index++) == "assign")
					{
						createCurExp();
						var e = E();
						if (e == null)
							return null;

						if (e.Contains('\n'))
						{
							var res = "\n\t" + curExp + " = " + e + ";\n\t" + outString + " = " + curExp + ";\n";
							exps.Push(curExp);
							curExp = "";
							return res;
						}
						exps.Push(curExp);
						curExp = "";
						return "\n\t" + outString + " = " + e + ";\n";
					}

					errorCode = 10;
					return null;

				case "if":
					outString = "\n\t" + lexems.Value(index++);
					var condition = F();
					if (condition == null)
						return null;

					outString = condition + outString + " (" + curExp + ")";
					if (lexems.Type(index++) == "then")
					{
						var d1 = D();
						if (d1 == null)
							return null; 

						if (lexems.Type(index++) == "else")
						{
							var d2 = D();
							if (d2 == null)
								return null;

							outString += " goto label" + ++labelsCounter + ";\n";
							outString += d2 + "\n\tgoto label" + ++labelsCounter + ";\n\n";
							outString += "label" + (labelsCounter - 1) + ":\n" + d1 + "\n";
							return outString + "label" + labelsCounter + ":\n";
						}

						errorCode = 12;
						return null;
					}

					errorCode = 11;
					return null;

				default:
					errorCode = 5;
					return null;
			}
		}

		// E -> dK
		// E -> kK
		/*
		 * E — <выражение>
		 * K — <доп. нетерминал>
		 * d — id
		 * k — num
		 */
		string E()
		{
			var outString = "";
			switch (lexems.Type(index))
			{
				case "id":
				case "num":
					outString = lexems.Value(index++);
					break;
				default:
					errorCode = 13;
					return null;
			}

			var k = K();
			if (k == null)
				return null;

			if (k != "")
			{
				return outString + ";\n\t" + curExp + " = " + curExp + k;
			}

			return outString;
		}

		// K -> +E
		// K -> -E
		// K -> ε
		/*
		 * K — <доп. нетерминал>
		 * E — <выражение>
		 * + — plus
		 * - — minus
		 */
		string K()
		{
			switch (lexems.Type(index))
			{
				case "plus":
				case "minus":
					var res = " " + lexems.Value(index++) + " ";
					var e = E();
					if (e == null)
						return null;
					return res + e;

				default:
					return "";
			}
		}

		// F -> GM
		/*
		 * F — <условие>
		 * G — <терм>
		 * M — <доп. нетерминал>
		 */
		string F()
		{
			return GM();
		}

		// M -> lGM
		// M -> ε
		/*
		 * G — <терм>
		 * M — <доп. нетерминал>
		 * l — or
		 */
		string M()
		{
			if (lexems.Type(index) == "or")
			{
				index++;
				return GM();
			}

			return "";
		}

		// Дополнительная функция для обработки пары нетерминалов GM,
		// → которые встречаются в правилах F и M.
		string GM()
		{
			var g = G();
			if (g == null)
				return null;

			var saved = curExp;
			var m = M();
			if (m == null)
				return null;

			if (m != "")
			{
				var saved2 = curExp;

				newCurExp();
				var outString = g + m + "\n\t" + curExp + " = " + saved + " || " + saved2 + ";\n";

				exps.Push(saved);
				exps.Push(saved2);

				return outString;
			}

			return g;
		}

		// G -> HN
		/*
		 * G — <терм>
		 * H — <множитель>
		 * N — <доп. нетерминал>
		 */
		string G()
		{
			return HN();
		}

		// N -> mHN
		// N -> ε
		/*
		 * H — <множитель>
		 * N — <доп. нетерминал>
		 * m — and
		 */
		string N()
		{
			if (lexems.Type(index) == "and")
			{
				index++;
				return HN();
			}

			return "";
		}

		// Дополнительная функция для обработки пары нетерминалов HN,
		// → которые встречаются в правилах G и N.
		string HN()
		{
			var h = H();
			if (h == null)
				return null;

			var saved = curExp;
			var n = N();
			if (n == null)
				return null;

			if (n != "")
			{
				var saved2 = curExp;

				newCurExp();
				var outString = h + n + "\n\t" + curExp + " = " + saved + " && " + saved2 + ";\n";

				exps.Push(saved);
				exps.Push(saved2);

				return outString;
			}

			return h;
		}

		// H -> n(F)
		// H -> (F)
		// H -> I
		/*
		 * F — <условие>
		 * H — <множитель>
		 * I — <отношение>
		 * n — not
		 */
		string H()
		{
			var outString = "";

			if (lexems.Type(index) == "not")
			{
				outString = "!";
				if (lexems.Type(++index) == "bracket")
				{
					index++;
					var f = F();
					if (f == null)
						return null;

					if (lexems.Type(index++) == "b-bracket")
						return f + "\n\t" + curExp + " = " + outString + curExp + ";\n";

					errorCode = 9;
					return null;
				}

				errorCode = 6;
				return null;
			}
			else if (lexems.Type(index) == "bracket")
			{
				index++;
				var f = F();
				if (f == null)
					return null;

				outString += f;
				if (lexems.Type(index++) == "b-bracket")
					return outString;

				errorCode = 9;
				return null;
			}
			else
				return I();
		}

		// I -> EoE
		/*
		 * E — <выражение>
		 * I — <отношение>
		 * o — eq
		 */
		string I()
		{
			var outString = "";

			newCurExp();
			var e = E();

			if (e == null)
				return null;

			if (lexems.Type(index) == "eq")
			{
				var eq = " " + lexems.Value(index++) + " ";
				if (eq == " = ") eq = " == ";
				if (eq == " <> ") eq = " != ";

				string saved = "";
					
				if (e.Contains('\n'))
				{
					saved = curExp;
					newCurExp();
				}

				var e2 = E();
				if (e2 == null)
					return null;

				string saved2 = "";

				if (e2.Contains('\n'))
				{
					saved2 = curExp;
					newCurExp();
				}

				if (saved != "")
					outString = "\n\t" + saved + " = " + e + ";\n";

				if (saved2 != "")
					outString += "\n\t" + saved2 + " = " + e2 + ";";

				outString += "\n\t" + curExp + " = ";

				if (saved != "")
				{
					outString += saved;
					exps.Push(saved);
				}
				else
					outString += e;

				outString += eq;

				if (saved2 != "")
				{
					outString += saved2 + ";\n";
					exps.Push(saved2);
				}
				else
					outString += e2 + ";\n";
						
				return outString;
			}

			errorCode = 14;
			return null;
		}

		void newCurExp()
		{
			if (exps.IsEmpty())
			{
				curExp = "exp" + expsCount++;
			}
			else
			{
				curExp = exps.Pop();
			}
		}

		void createCurExp()
		{
			if (curExp == "")
			{
				newCurExp();
			}
		}

		string GetLastError()
		{
			switch (errorCode)
			{
				case 0:
					return "Неожиданный конец программы!";
				case 1:
					return "Программа должна начинаться с ключевого слова 'var'!";
				case 2:
					return "Ожидалось ключевое слово 'begin'" + beforeLexem(2);
				case 3:
					return "Ожидалось ключевое слово 'end' или ';'" + beforeLexem(2);
				case 4:
					return "Ожидался идентификатор" + beforeLexem();
				case 5:
					return "Ожидался оператор" + beforeLexem();
				case 6:
					return "Ожидался символ '('" + beforeLexem();
				case 7:
					return "Ожидалась строка" + beforeLexem();
				case 8:
					return "Ожидался символ ','" + beforeLexem();
				case 9:
					return "Ожидался символ ')'" + beforeLexem(2);
				case 10:
					return "Ожидался оператор ':='" + beforeLexem(2);
				case 11:
					return "Ожидалось ключевое слово 'then'" + beforeLexem(2);
				case 12:
					return "Ожидалось ключевое слово 'else'" + beforeLexem(2);
				case 13:
					return "Ожидался идентификатор или число" + beforeLexem(1);
				case 14:
					return "Ожидался операция отношения" + beforeLexem(1);
				default:
					return "Неизвестная ошибка.";
			}
		}

		string beforeLexem(int shift = 1)
		{
			return " после лексемы «" + lexems.Value(index - shift) + "» (" + lexems.Type(index - shift) + ").";
		}
	}
}