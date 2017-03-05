'use strict';

var webdriverio = require('webdriverio');
var expect = require('expect.js');
var path = require('path');

var html5Support = true;
var file = path.join('C:', 'Users', 'Игорь', 'Downloads', 'student.zip');

describe('Mars vacation program', function(){
    beforeEach(function() {
        browser.url('/');

        // b = browser.execute(function() {
        //     return window.navigator.appVersion;
        // });

        html5Support = browser.execute(function() {
            var i = document.createElement('input');
            i.setAttribute('type', 'date');
           
            return i.type !== 'text';
        }).value;
    });

    it('should be putin lover from russia', function() {
        browser.click('#first-name')
            .keys('Igor')
            .click('#last-name')
            .keys('Adamenko')
            .click('#date')
            .keys('23.06.1994')
            .selectByValue('select', 'russia')
            .click('#putin')
            .click('#about')
            .keys('Smth about me')
            .chooseFile('#photo', file)
            .click('input[type=submit]')

        var result = browser.execute(function() {
            return document.querySelector('tbody td:first-child').innerHTML;
        });

        expect(result.value).to.eql('Igor');
    });

    it('should be tea lover from china', function() {
        browser.click('#first-name')
            .keys('Igor')
            .click('#last-name')
            .keys('Adamenko')
            .click('#date')
            .keys('23.06.1994')
            .selectByValue('select', 'china')
            .click('input[value=white]')
            .click('input[value=green]')
            .click('input[value="pu erh"]')
            .click('#about')
            .keys('Smth about me')
            .chooseFile('#photo', file)
            .click('input[type=submit]')

        var result = browser.execute(function() {
            return document.querySelector('tbody td:first-child').innerHTML;
        });

        expect(result.value).to.eql('Igor');
    });

    it('should be man from usa', function() {
        browser.click('#first-name')
            .keys('Igor')
            .click('#last-name')
            .keys('Adamenko')
            .click('#date')
            .keys('23.06.1994')
            .selectByValue('select', 'usa')
            .click('#zip')
            .keys('RX783')
            .click('#about')
            .keys('Smth about me')
            .chooseFile('#photo', file)
            .click('input[type=submit]')

        var result = browser.execute(function() {
            return document.querySelector('tbody td:first-child').innerHTML;
        });

        expect(result.value).to.eql('Igor');
    });

    afterEach(function() {
        if (this.currentTest.state !== "passed") {
            browser.saveScreenshot();
        }
    });
});
