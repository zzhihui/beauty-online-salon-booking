(function () {
    "use strict";

    angular.module("webapp").controller("registerController", registerController);

    registerController.$inject = ['$scope', 'requestService', 'authenticationService'];
    function registerController($scope, requestService, authenticationService) {
        $scope.register = function () {
            requestService.registerCustomer($scope.customer).then(function (response) {
                
                authenticationService.saveCredential(response.data);
                window.history.back();

            }).catch(function (err) {
                console.log(err);
                $scope.error = err["data"];
            });
        }
    }
})();