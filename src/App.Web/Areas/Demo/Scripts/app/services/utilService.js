(function () {
    'use strict';

    angular.module('webapp').factory('utilService', utilService);
    utilService.$inject = ['$location'];
    function utilService($location) {

        //Services
        var service = {
            isUndefinedOrNull: isUndefinedOrNull,
            newGuid: newGuid,
            newRef: newRef,
            routing: routing
        };
        return service;

        //Support function 
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
              .toString(16)
              .substring(1);
        }


        function isUndefinedOrNull(val) {
            return angular.isUndefined(val) || val === null;
        }
        function newGuid() {

            return s4() + s4() + "-" + s4() + "-" + s4() + "-" +
              s4() + "-" + s4() + s4() + s4();
        }
        function newRef() {
            return s4() + s4();
        }


        function routing(path) {
            $location.url(path);
        }

    }


})();