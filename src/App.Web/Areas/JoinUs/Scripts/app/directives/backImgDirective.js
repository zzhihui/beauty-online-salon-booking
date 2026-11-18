(function () {
    'use strict';

    angular.module('webapp').directive('backImg', function () {
        return function (scope, element, attrs) {
            var url = attrs.backImg;
            element.css({
                'background-image': 'url(' + url + ')',
                'background-size': '300px 150px',
                'background-position': 'center',
                'background-repeat': 'no-repeat'
            });
        };
    });
})();