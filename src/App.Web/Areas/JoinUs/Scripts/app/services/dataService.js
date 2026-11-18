(function () {
    'use strict';

    angular.module('webapp').factory('dataService', dataService);
    dataService.$inject = ['$http', '$location', '$q', '$rootScope'];
    function dataService($http, $location, $q, $rootScope) {

        //Service Functions
        var service = {

            getNewOrder: getNewOrder,
            calculateTotal: calculateTotal
        };
        return service;

        //Support function
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
              .toString(16)
              .substring(1);
        }
        function newGuid() {

            return s4() + s4() + "-" + s4() + "-" + s4() + "-" +
              s4() + "-" + s4() + s4() + s4();
        }


        //Public functions 
        function getNewOrder() {
            var order = {
                id: 0,
                guid: newGuid()
            };
            return angular.copy(order);
        }

        function calculateTotal(order) {
            var totalValue = 0;
            if (angular.isUndefined(order) || order == null)
                return totalValue;

            angular.forEach(order.orderItems, function (item) {
                totalValue += item.price;
            });

            order.orderOriginalTotal = totalValue;
            order.orderTotal = totalValue;

            return totalValue;
        }



    }
})();