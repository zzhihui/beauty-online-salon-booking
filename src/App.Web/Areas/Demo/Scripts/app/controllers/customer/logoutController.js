(function () {
    "use strict";

    angular.module("webapp").controller("logoutController", logoutController);

    logoutController.$inject = ['$scope', 'authenticationService', '$location'];
    function logoutController($scope, authenticationService, $location) {
        authenticationService.clearCredential();
        $location.url('/');

    }

})();