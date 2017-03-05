'use strict';

module.exports = function(n, lang) {
	if (typeof n !== 'number') return undefined;
	
	String.prototype.reverse = function() {
		return this.split('').reverse().join('');
	};

	lang = lang || 'ru'; 

	var nStr = (n + '');

	if (parseInt(nStr) !== n) {
		throw new Error('Incorrect number (not integer, more that MAXINT or smth else)');
	}

	var locale = require('./locale/' + lang),
		parts = nStr.reverse().match(/.{1,3}/g)
			.reverse().map(function(item) {
				return item.reverse();
			});

	return locale(parts);
};