(function () {
    "use strict";

    angular.module("webapp").controller("navController", navController);

    navController.$inject = ['$scope', '$rootScope', 'utilService', 'authenticationService'];
    function navController($scope, $rootScope, utilService, authenticationService) {
        
        $scope.$on('order-updated', function () {
            if (utilService.isUndefinedOrNull($rootScope.myOrder)) {
                $scope.countNumber = "";
            }
            else {
                if ($rootScope.myOrder.orderItems.length == 0) {
                    $scope.countNumber = "";
                } else {
                    $scope.countNumber = $rootScope.myOrder.orderItems.length.toString();
                }
            }
        });
        $scope.$on('loading', function () {
            //Do nothing...
        });

        $scope.$on('user-change', function () {

            if (authenticationService.isAuthenticated()) {
                $scope.userloggedin = true;
                $scope.fullName = $rootScope.globals.currentUser.firstName + ' ' + $rootScope.globals.currentUser.lastName;
            } else {
                $scope.userloggedin = false;
                $scope.fullName = "";
            }
        });
    }
})();