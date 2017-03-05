'use strict';

var numbers = {
	0: 'ноль',
	1: { masculine: 'один', feminine: 'одна' },
	2: { masculine: 'два', feminine: 'две' },
	3: 'три',
	4: 'четыре',
	5: 'пять',
	6: 'шесть',
	7: 'семь',
	8: 'восемь',
	9: 'девять',
	10: 'десять',
	11: 'одиннадцать',
	12: 'двенадцать',
	13: 'тринадцать',
	14: 'четырнадцать',
	15: 'пятнадцать',
	16: 'шестнадцать',
	17: 'семнадцать',
	18: 'восемнадцать',
	19: 'девятнадцать',
	20: 'двадцать',
	30: 'тридцать',
	40: 'сорок',
	50: 'пятьдесят',
	60: 'шестьдесят',
	70: 'семьдесят',
	80: 'восемьдесят',
	90: 'девяносто',
	100: 'сто',
	200: 'двести',
	300: 'триста',
	400: 'четыреста',
	500: 'пятьсот',
	600: 'шестьсот',
	700: 'семьсот',
	800: 'восемьсот',
	900: 'девятьсот'
};

var exponent = {
	6: 'миллион',
	9: 'миллиард',
	12: 'триллион',
	15: 'квадриллион'
};

module.exports = function(parts) {
	function parsePart(n, isThousand) {
		var one = n % 10,
			ten = n % 100,
			hundred = n % 1000;

		var result = [];

		if (hundred >= 100) {
			result.push(numbers[hundred - ten]);
		}

		if (ten >= 10 && ten < 20) {
			result.push(numbers[ten]);
		} else {
			if (ten > 10) {
				result.push(numbers[ten - one]);
			}

			if (one > 0) {
				if (typeof numbers[one] == 'object') {
					result.push(numbers[one][isThousand ? 'feminine' : 'masculine']);
				} else {
					result.push(numbers[one]);
				}
			}
		}

		return result.join(' ');
	}

	function parseThousand(n) {
		switch (n % 10) {
			case 1: 
				return 'тысяча';
			case 2:
			case 3:
			case 4:
				return 'тысячи';
			default:
				return 'тысяч';
		}
	}

	function parseExponent(n, exp) {
		var ending;

		switch (n % 10) {
			case 1:
				ending = '';
				break;
			case 2:
			case 3:
			case 4:
				ending = 'а';
				break;
			default:
				ending = 'ов';
				break;
		}

		return exponent[exp] + ending;
	}

	if (parts.length == 1 && parts[0] == '0') return numbers[0];

	return parts.reduce(function(prev, cur, index, arr) {
		var ending,
			part = arr.length - index;

		cur = parseInt(cur, 10);

		switch (part) {
			case 1: 
				ending = '';
				break;
			case 2:
				ending = parseThousand(cur);
				break;
			default:
				ending = parseExponent(cur, (part - 1) * 3);
				break;
		}

		return [prev, parsePart(cur, part == 2), ending].join(' ').trim();
	}, '');
};