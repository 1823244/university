'use strict';
/*eslint-env mocha */

var	expect = require('expect.js'),
	$ = require('./');

describe('Wrong input', function() {
	it('should returns undefined', function() {
		expect($(null)).to.be(undefined);
		expect($('1')).to.be(undefined);
		expect($('')).to.be(undefined);
	});

	it('should not work', function() {
		expect($).withArgs(1012736126318631763183).to.throwError();
	});
});

describe('Russian locale', function() {
	it('should work fine', function() {
		expect($(0)).to.be('ноль');
		expect($(10)).to.be('десять');
		expect($(15)).to.be('пятнадцать');
		expect($(20)).to.be('двадцать');
		expect($(42)).to.be('сорок два');
		expect($(100)).to.be('сто');
		expect($(101)).to.be('сто один');
		expect($(379)).to.be('триста семьдесят девять');
		expect($(1000)).to.be('одна тысяча');
		expect($(1234)).to.be('одна тысяча двести тридцать четыре');
		expect($(987654)).to.be('девятьсот восемьдесят семь тысяч шестьсот пятьдесят четыре');
		expect($(1010000)).to.be('один миллион десять тысяч');
		expect($(5003000)).to.be('пять миллионов три тысячи');
		expect($(102031002)).to.be('сто два миллиона тридцать одна тысяча два');
	});
});

describe('English locale', function() {
	it('should work fine', function() {
		expect($(0, 'en')).to.be('zero');
		expect($(10, 'en')).to.be('ten');
		expect($(15, 'en')).to.be('fifteen');
		expect($(20, 'en')).to.be('twenty');
		expect($(42, 'en')).to.be('forty-two');
		expect($(100, 'en')).to.be('one hundred');
		expect($(101, 'en')).to.be('one hundred one');
		expect($(379, 'en')).to.be('three hundred seventy-nine');
		expect($(1000, 'en')).to.be('one thousand');
		expect($(1234, 'en')).to.be('one thousand two hundred thirty-four');
		expect($(987654, 'en')).to.be('nine hundred eighty-seven thousand six hundred fifty-four');
		expect($(1010000, 'en')).to.be('one million ten thousand');
	});
});
