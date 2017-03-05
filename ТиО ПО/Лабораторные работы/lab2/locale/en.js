'use strict';

var numbers = {
	0: 'zero',
	1: 'one',
	2: 'two',
	3: 'three',
	4: 'four',
	5: 'five',
	6: 'six',
	7: 'seven',
	8: 'eight',
	9: 'nine',
	10: 'ten',
	11: 'eleven',
	12: 'twelve',
	13: 'thirteen',
	14: 'fourteen',
	15: 'fifteen',
	16: 'sixteen',
	17: 'seventeen',
	18: 'eighteen',
	19: 'nineteen',
	20: 'twenty',
	30: 'thirty',
	40: 'forty',
	50: 'fifty',
	60: 'sixty',
	70: 'seventy',
	80: 'eighty',
	90: 'ninety'
};

var exponent = {
	2: 'hundred',
	3: 'thousand',
	6: 'million',
	9: 'thousand million',
	12: 'billion',
	15: 'thousand billion'
};

module.exports = function(parts) {
	function parsePart(n) {
		var one = n % 10,
			ten = n % 100,
			hundred = Math.floor(n / 100);

		var result = [];

		if (hundred > 0) {
			result.push(numbers[hundred]);
			result.push(exponent[2]);
		}

		if (ten >= 10 && ten < 20) {
			result.push(numbers[ten]);
		} else {
			if (ten > 10) {
				var part = numbers[ten - one];

				if (one > 0) {
					part += '-' + numbers[one];
				}

				result.push(part);
			} else {
				if (one > 0) {
					result.push(numbers[one]);
				}
			}
		}

		return result.join(' ');
	}

	if (parts.length == 1 && parts[0] == '0') return numbers[0];

	return parts.reduce(function(prev, cur, index, arr) {
		var ending,
			part = arr.length - index;

		cur = parseInt(cur, 10);

		if (part == 1) {
			ending = '';
		} else {
			ending = exponent[(part - 1) * 3];
		}
 
		return [prev, parsePart(cur), ending].join(' ').trim();
	}, '');
};