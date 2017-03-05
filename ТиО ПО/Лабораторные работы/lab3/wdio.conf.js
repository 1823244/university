var selenium = require('selenium-standalone');
var seleniumServer;

exports.config = {

    // host: 'ondemand.saucelabs.com',
    host: '127.0.0.1',
    // port: 80,
    port: 4444,

    specs: [
        'test/*.spec.js'
    ],

    capabilities: [{
        browserName: 'phantomjs'
    }, {
        browserName: 'chrome'
    }],

    // user: 'igoradamenko',

    // key: 'b35a9dea-2da1-4ed7-9af2-7a41378ab317',

    // logLevel: 'silent',
    coloredLogs: true,
    screenshotPath: 'shots',
    baseUrl: 'http://localhost:8080',
    waitforTimeout: 10000,
    framework: 'mocha',

    reporters: ['dot', 'allure'],
    reporterOptions: {
        outputDir: './allure-results'
    },

    mochaOpts: {
        ui: 'bdd'
    },

    onPrepare: function() {
        return new Promise((resolve, reject) => {
            selenium.start((err, process) => {
                if(err) {
                    return reject(err);
                }
                seleniumServer = process;
                resolve(process);
            })
        });
    },
    onComplete: function() {
        seleniumServer.kill();
    }

};
