(function () {
    'use strict';

    angular.module('webapp').factory('shortText', shortText);
    function shortText() {
        return function (input) {
            if (angular.isUndefined(val) || val === null)
                return '';
            else
                return '';
                
        };
    }

})();