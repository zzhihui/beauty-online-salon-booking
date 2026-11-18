(function () {
    "use strict";

    angular.module("webapp").controller("meController", meController);

    meController.$inject = ['$scope', '$rootScope', 'requestService', 'authenticationService'];

    function meController($scope, $rootScope, requestService, authenticationService) {
        
        //Get customer detail from controller api.
        if ($rootScope.globals.currentUser != null) {

            var user = $rootScope.globals.currentUser;

            //var res = str.split(" ");

            $scope.master = {
                credit: '99',
                firstName: user.firstName,
                lastName: user.lastName,
                email: user.email,
                address: user.address,
                phoneNumber: user.phoneNumber
            };

            //requestService.getCustomerDetails($rootScope.globals.currentUser.id).then(function (response) {

            //    var user = response.data;
            //    console.log($rootScope.globals.currentUser);
            //    alert(user.contactName);
            //    $scope.master = {
            //        credit: '99',
            //        firstName: user.contactName,
            //        lastName: user.contactName,
            //        email: user.email,
            //        address: user.address,
            //        phoneNumber: user.phoneNumber
            //    };
            //}).catch(function (err) {
            //    $scope.error = "Failed to Login. Please try again?";
            //});
        }

        $scope.passwordCheckboxChanged = function() {
            if (!$scope.customer.changePassword) {
                $scope.customer.currentPassword = "";
                $scope.customer.newPassword = "";
                $scope.customer.confirmNewPassword = "";
            }
        }

        $scope.Update = function (customer) {
            $scope.master = angular.copy(customer);

            //Save to database
            //if (!$scope.customer.changePassword) {

            customer.id = $rootScope.globals.currentUser.id;
            customer.email = $rootScope.globals.currentUser.email;

            requestService.updateCustomerDetails(customer).then(function (response) {
                console.log(response.data);

                authenticationService.saveCredential(response.data);
                $rootScope.$broadcast('user-change');

                $scope.resultMessage = "Your details have been saved.";
                $scope.error = "";
                //var user = response.data;
                //console.log($rootScope.globals.currentUser);
                //alert(user.contactName);
                //$scope.master = {
                //    credit: '99',
                //    firstName: user.contactName,
                //    lastName: user.contactName,
                //    email: user.email,
                //    address: user.address,
                //    phoneNumber: user.phoneNumber
                //};
            }).catch(function (err) {
                console.log(err);
                $scope.error = "Error update profile. Please try again?";
                $scope.resultMessage = "";
            });
            //}
        };

        $scope.Reset = function (form) {
            if (form) {
                form.$setPristine();
                form.$setUntouched();
            }
            $scope.customer = angular.copy($scope.master);
            $scope.resultMessage = "";
        };

        $scope.Reset();
    }
})();