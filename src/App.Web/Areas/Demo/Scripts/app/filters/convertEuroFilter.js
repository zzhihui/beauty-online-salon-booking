(function () {
    'use strict';

    angular.module('webapp').factory('convertEuro', convertEuro);
    function convertEuro() {
        return function (input) {
            return '€' + input;
        };
    }

})();