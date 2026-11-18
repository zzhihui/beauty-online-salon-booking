(function () {
    "use strict";

    function loginController($scope, requestService, authenticationService) {
        $scope.login = function () {
            requestService.loginCustomer($scope.email, $scope.password).then(function (response) {
                
                authenticationService.saveCredential(response.data);
                window.history.back();
                //window.location.href = 'Demo#!/';

            }).catch(function (err) {
                $scope.error = "Failed to Login. Please try again?";
            });
        }
    }

    angular.module("webapp").controller("loginController", loginController);

    loginController.$inject = ['$scope', 'requestService', 'authenticationService'];
})();